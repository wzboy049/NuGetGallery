using System.Threading.Tasks;

namespace NuGetGallery.Features
{
    public interface IFeatureFlagStorageService
    {
        Task<FeatureFlags> GetAsync();
    }
}
