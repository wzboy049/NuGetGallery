using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuGetGallery.Features
{
    public class FeatureFlagFileStorageService : IFeatureFlagStorageService
    {
        private readonly ICoreFileStorageService _storage;
        private readonly FeatureFlagOptions _options;
        private readonly JsonSerializer _serializer;

        public FeatureFlagFileStorageService(ICoreFileStorageService storage, FeatureFlagOptions options)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _serializer = new JsonSerializer();
        }

        public async Task<FeatureFlags> GetAsync()
        {
            using (var stream = await _storage.GetFileAsync(_options.Container, _options.Blob))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize<FeatureFlags>(reader);
            }
        }
    }
}
