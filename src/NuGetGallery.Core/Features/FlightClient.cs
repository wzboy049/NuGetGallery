using System;
using System.Linq;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using NuGet.Services.Entities;

namespace NuGetGallery.Features
{
    public class FlightClient : IFlightClient
    {
        private readonly IFeatureFlagRefreshService _flags;
        private readonly ILogger<FlightClient> _logger;

        public FlightClient(IFeatureFlagRefreshService flags, ILogger<FlightClient> logger)
        {
            _flags = flags ?? throw new ArgumentNullException(nameof(flags));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool Can(string flightName, User user, bool @default)
        {
            var result = Can(flightName, user);

            switch (result)
            {
                case UserFlightResult.Enabled:
                    return true;

                case UserFlightResult.Disabled:
                    return false;

                case UserFlightResult.Unknown:
                default:
                    return @default;
            }
        }

        public UserFlightResult Can(string flightName, User user)
        {
            var latest = _flags.GetLatestFlags();
            if (latest.Status != LatestFlagsStatus.Ok)
            {
                _logger.LogWarning(
                    "Couldn't determine status of flight {Flight} as the latest flags have status {LatestFlagsStatus}",
                    flightName,
                    latest.Status);

                return UserFlightResult.Unknown;
            }

            if (!latest.Flags.Flights.TryGetValue(flightName, out var flight))
            {
                _logger.LogWarning(
                    "Couldn't determine status of flight {Flight} as it isn't in the latest feature flags",
                    flightName);

                return UserFlightResult.Unknown;
            }

            if (flight.All)
            {
                return UserFlightResult.Enabled;
            }

            if (flight.Accounts.Contains(user.Username))
            {
                return UserFlightResult.Enabled;
            }

            if (TryParseEmailDomain(user.EmailAddress, out var domain) && flight.Domains.Contains(domain))
            {
                return UserFlightResult.Enabled;
            }

            if (flight.SiteAdmin && user.IsInRole(CoreConstants.AdminRoleName))
            {
                return UserFlightResult.Enabled;
            }

            return UserFlightResult.Disabled;
        }

        private bool TryParseEmailDomain(string email, out string domain)
        {
            try
            {
                domain = (new MailAddress(email)).Host;

                return true;
            }
            catch (ArgumentNullException) { }
            catch (ArgumentException) { }
            catch (FormatException) { }

            domain = null;
            return false;
        }
    }
}
