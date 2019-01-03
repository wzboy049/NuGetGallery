using System;

namespace NuGetGallery.Features
{
    public class FeatureFlagOptions
    {
        /// <summary>
        /// How frequently the feature flags should be refreshed.
        /// </summary>
        public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// The maximum refresh staleness allowed by the <see cref="IFeatureFlagRefreshService"/>.
        /// If the threshold is reached, the returned feature flags will be labeled as stale.
        /// </summary>
        public TimeSpan? MaximumStaleness { get; set; } = TimeSpan.FromHours(1);
    }
}
