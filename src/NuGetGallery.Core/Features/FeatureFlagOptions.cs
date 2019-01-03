using System;

namespace NuGetGallery.Features
{
    public class FeatureFlagOptions
    {
        /// <summary>
        /// The connection string for the Azure Blob Storage account that stores the feature flags.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The Azure Blob Storage container that stores the feature flags.
        /// </summary>
        public string Container { get; set; } = CoreConstants.Folders.FeatureFlagsContainerFolderName;

        /// <summary>
        /// The Azure Blob Storage blob path that stores the feature flags.
        /// </summary>
        public string Blob { get; set; } = CoreConstants.FeatureFlagsFileName;

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
