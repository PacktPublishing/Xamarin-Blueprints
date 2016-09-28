// --------------------------------------------------------------------------------------------------
//  <copyright file="GeolocatorWinPhone.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.WinPhone.Location
{
    using System;
    using System.Reactive.Subjects;
    using System.Linq;

    using Windows.Devices.Geolocation;

    using Locator.Portable.Location;

    public class GeolocatorWinPhone : IGeolocator
    {
		/// <summary>
		/// Gets or sets the positions.
		/// </summary>
		/// <value>The positions.</value>
        public Subject<IPosition> Positions { get; set; }

		/// <summary>
		/// The windows geolocator.
		/// </summary>
        private Geolocator _geolocator;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.WinPhone.Location.GeolocatorIOS"/> class.
		/// </summary>
        public GeolocatorWinPhone()
        {
            Positions = new Subject<IPosition>();

            _geolocator = new Geolocator();
            _geolocator.DesiredAccuracyInMeters = 50;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        public async void Start()
        {
            try
            {
                var geoposition = await _geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                _geolocator.MovementThreshold = 1;
                _geolocator.DesiredAccuracy = PositionAccuracy.High;
                _geolocator.PositionChanged += GeolocatorPositionChanged;

                // push a new position into the sequence
                Positions.OnNext(new Position()
                    {
                        Latitude = geoposition.Coordinate.Latitude,
                        Longitude = geoposition.Coordinate.Longitude
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error retrieving geoposition - " + ex);
            }

        }

        /// <summary>
        /// Stop this instance.
        /// </summary>
        public void Stop()
        {
            // remove event handler
            _geolocator.PositionChanged -= GeolocatorPositionChanged;
        }

        /// <summary>
        /// Geolocator position changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GeolocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            // push a new position into the sequence
            Positions.OnNext(new Position()
            {
                Latitude = args.Position.Coordinate.Latitude,
                Longitude = args.Position.Coordinate.Longitude
            });
        }
    }
}