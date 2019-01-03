using System;
using Microsoft.Extensions.Logging;

namespace NuGetGallery.Features
{
    public class FeatureFlagClient : IFeatureFlagClient
    {
        private readonly IFeatureFlagRefreshService _flags;
        private readonly ILogger<FeatureFlagClient> _logger;

        public FeatureFlagClient(IFeatureFlagRefreshService flags, ILogger<FeatureFlagClient> logger)
        {
            _flags = flags ?? throw new ArgumentNullException(nameof(flags));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool IsEnabled(string feature, bool @default)
        {
            var result = IsEnabled(feature);

            switch (result)
            {
                case IsEnabledResult.Enabled:
                    return true;

                case IsEnabledResult.Disabled:
                    return false;

                case IsEnabledResult.Unknown:
                default:
                    return @default;

            }
        }

        public IsEnabledResult IsEnabled(string feature)
        {
            var latest = _flags.GetLatestFlags();
            if (latest.Status != LatestFlagsStatus.Ok)
            {
                _logger.LogWarning(
                    "Couldn't determine status of feature {Feature} as the latest flags have status {LatestFlagsStatus}",
                    feature,
                    latest.Status);

                return IsEnabledResult.Unknown;
            }

            if (!latest.Flags.Features.TryGetValue(feature, out var featureStatus))
            {
                _logger.LogWarning(
                    "Couldn't determine status of feature {Feature} as it isn't in the latest flags",
                    feature);

                return IsEnabledResult.Unknown;
            }

            switch (featureStatus)
            {
                case FeatureStatus.Enabled:
                    return IsEnabledResult.Enabled;

                case FeatureStatus.Disabled:
                    return IsEnabledResult.Disabled;

                default:
                    _logger.LogWarning(
                        "Unknown feature status {FeatureStatus} for feature {Feature}",
                        feature,
                        featureStatus);

                    return IsEnabledResult.Unknown;
            }
        }
    }
}
