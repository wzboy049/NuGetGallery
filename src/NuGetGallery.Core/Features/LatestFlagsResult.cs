using System;

namespace NuGetGallery.Features
{
    /// <summary>
    /// The latest known feature flags' state.
    /// </summary>
    public class LatestFlagsResult
    {
        public static readonly LatestFlagsResult Uninitialized = new LatestFlagsResult(LatestFlagsStatus.Uninitialized, flags: null);

        private LatestFlagsResult(LatestFlagsStatus status, FeatureFlags flags)
        {
            Flags = flags;
            Status = status;
        }

        public static LatestFlagsResult Stale(FeatureFlags flags)
        {
            if (flags == null) throw new ArgumentNullException(nameof(flags));

            return new LatestFlagsResult(LatestFlagsStatus.Stale, flags);
        }

        public static LatestFlagsResult Ok(FeatureFlags flags)
        {
            if (flags == null) throw new ArgumentNullException(nameof(flags));

            return new LatestFlagsResult(LatestFlagsStatus.Ok, flags);
        }

        /// <summary>
        /// The status of the feature flags.
        /// </summary>
        public LatestFlagsStatus Status { get; }

        /// <summary>
        /// The latest known feature flags. Null if the status is unitialized.
        /// </summary>
        public FeatureFlags Flags { get; }
    }

    /// <summary>
    /// The status of the feature flags. Each service and job refresh their
    /// cache of the feature flags.
    /// </summary>
    public enum LatestFlagsStatus
    {
        /// <summary>
        /// The feature flags' state are unknown as they have never been loaded.
        /// </summary>
        Uninitialized = 0,

        /// <summary>
        /// The feature flags are no longer fresh and may be incorrect.
        /// </summary>
        Stale = 1,

        /// <summary>
        /// The feature flags are ready for use.
        /// </summary>
        Ok = 2,
    }
}
