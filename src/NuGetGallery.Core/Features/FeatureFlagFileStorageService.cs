using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuGetGallery.Features
{
    public class FeatureFlagFileStorageService : IFeatureFlagStorageService
    {
        private readonly ICoreFileStorageService _storage;
        private readonly JsonSerializer _serializer;

        public FeatureFlagFileStorageService(ICoreFileStorageService storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _serializer = new JsonSerializer();
        }

        public async Task<FeatureFlags> GetAsync()
        {
            using (var stream = await _storage.GetFileAsync("flags", "flags.json"))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize<FeatureFlags>(reader);
            }
        }
    }
}
