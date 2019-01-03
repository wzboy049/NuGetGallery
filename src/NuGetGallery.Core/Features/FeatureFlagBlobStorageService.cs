using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuGetGallery.Features
{
    public class FeatureFlagBlobStorageService : IFeatureFlagStorageService
    {
        private readonly ICloudBlobClient _blobClient;
        private readonly FeatureFlagOptions _options;

        public FeatureFlagBlobStorageService(ICloudBlobClient blobClient, FeatureFlagOptions options)
        {
            _blobClient = blobClient ?? throw new ArgumentNullException(nameof(blobClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<FeatureFlags> GetAsync()
        {
            var container = _blobClient.GetContainerReference(_options.Container);
            var blob = container.GetBlobReference(_options.Blob);

            var json = await blob.DownloadTextAsync();

            return JsonConvert.DeserializeObject<FeatureFlags>(json);
        }
    }
}
