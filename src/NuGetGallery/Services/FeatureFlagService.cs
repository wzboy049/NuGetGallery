// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Services.FeatureFlags;

namespace NuGetGallery
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private const string GalleryPrefix = "NuGetGallery.";

        private readonly IFeatureFlagClient _featureFlagClient;
        private readonly IFlightClient _flightClient;

        public FeatureFlagService(IFeatureFlagClient featureFlagClient, IFlightClient flightClient)
        {
            _featureFlagClient = featureFlagClient ?? throw new ArgumentNullException(nameof(featureFlagClient));
            _flightClient = flightClient ?? throw new ArgumentNullException(nameof(flightClient));
        }

        // TODO...
    }
}