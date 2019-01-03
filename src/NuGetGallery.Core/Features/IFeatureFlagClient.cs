namespace NuGetGallery.Features
{
    public interface IFeatureFlagClient
    {
        /// <summary>
        /// Get whether a feature is enabled. This method does not throw.
        /// </summary>
        /// <param name="feature">The unique identifier for this feature.</param>
        /// <param name="default">The value to return if the status of the feature is unknown.</param>
        /// <returns>Whether the feature is enabled.</returns>
        bool IsEnabled(string feature, bool @default);

        /// <summary>
        /// Get status of a feature flag. This method does not throw.
        /// </summary>
        /// <param name="feature">The unique identifier for this feature.</param>
        /// <returns>This feature's status.</returns>
        IsEnabledResult IsEnabled(string feature);
    }

    public enum IsEnabledResult
    {
        /// <summary>
        /// The feature's latest status is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The feature is disabled.
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// The feature is enabled.
        /// </summary>
        Enabled = 2,
    }
}
