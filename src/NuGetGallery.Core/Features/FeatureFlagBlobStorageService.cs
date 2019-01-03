using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuGetGallery.Features
{
    public class FeatureFlagBlobStorageService : IFeatureFlagStorageService
    {
        private readonly ICloudBlobClient _blobClient;

        public FeatureFlagBlobStorageService(ICloudBlobClient blobClient)
        {
            _blobClient = blobClient ?? throw new ArgumentNullException(nameof(blobClient));
        }

        public async Task<FeatureFlags> GetAsync()
        {
            var container = _blobClient.GetContainerReference("flags");
            var blob = container.GetBlobReference("flags.json");

            var json = await blob.DownloadTextAsync();

            return JsonConvert.DeserializeObject<FeatureFlags>(json);
        }
    }
}
