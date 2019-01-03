using NuGet.Services.Entities;

namespace NuGetGallery.Features
{
    public interface IFlightClient
    {
        /// <summary>
        /// Get whether a flight is enabled for a user. This method does not throw.
        /// </summary>
        /// <param name="flight">The unique identifier for this flight.</param>
        /// <param name="user">The user whose status should be determined.</param>
        /// <param name="default">The value to return if the status of the flight is unknown.</param>
        /// <returns>Whether the flight is enabled for this user.</returns>
        bool Can(string flight, User user, bool @default);

        /// <summary>
        /// Get whether a flight is enabled for a user. This method does not throw.
        /// </summary>
        /// <param name="flight">The unique identifier for this flight.</param>
        /// <param name="user">The user whose status should be determined.</param>
        /// <returns>Whether the flight is enabled for this user.</returns>
        UserFlightResult Can(string flight, User user);
    }

    public enum UserFlightResult
    {
        /// <summary>
        /// The feature's latest status is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The flight is disabled for this user.
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// The flight is enabled for this user.
        /// </summary>
        Enabled = 2,
    }
}
