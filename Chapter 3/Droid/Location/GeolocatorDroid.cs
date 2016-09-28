// --------------------------------------------------------------------------------------------------
//  <copyright file="GeolocatorDroid.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.Droid.Location
{
	using System;
	using System.Reactive.Subjects;

	using Android.Content;
	using Android.App;
	using Android.OS;
	using Android.Locations;
	using Android.Provider;
	using Android.Runtime;

	using Object = Java.Lang.Object;

	using Xamarin.Forms;

	using Locator.Portable.Location;

	/// <summary>
	/// Geolocator droid.
	/// </summary>
	public class GeolocatorDroid : Object, IGeolocator, ILocationListener
	{
		#region Private Properties

		/// <summary>
		/// The provider.
		/// </summary>
		private string provider;

		/// <summary>
		/// The location manager.
		/// </summary>
		private LocationManager locationManager;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the positions.
		/// </summary>
		/// <value>The positions.</value>
		public Subject<IPosition> Positions { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Start()
		{
			if (locationManager.IsProviderEnabled(provider))
			{
				locationManager.RequestLocationUpdates(provider, 2000, 1, this);
			}
			else
			{
				Console.WriteLine(provider + " is not available. Does the device have location services enabled?");
			}
		}

		/// <summary>
		/// Stop this instance.
		/// </summary>
		public void Stop()
		{
			locationManager.RemoveUpdates(this);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.Droid.Location.GeolocatorDroid"/> class.
		/// </summary>
		public GeolocatorDroid()
		{
			Positions = new Subject<IPosition> ();
		
			locationManager = (LocationManager) Android.App.Application.Context.GetSystemService(Context.LocationService);
			provider = LocationManager.NetworkProvider;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Requests the location services.
		/// </summary>
		/// <returns>The location services.</returns>
		private void RequestLocationServices()
		{
			// Build the alert dialog
			AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);
			builder.SetTitle("Locator");
			builder.SetMessage("Would you like to enable Location Services and GPS");
			builder.SetPositiveButton("Yes", (sender, e) =>
			{
				// Show location settings when the user acknowledges the alert dialog
				Intent intent = new Intent(Settings.ActionLocationSourceSettings);
				Forms.Context.StartActivity(intent);
			});
			builder.SetNegativeButton("No", (sender, e) =>
			{
			});

			Dialog alertDialog = builder.Create();
			alertDialog.SetCanceledOnTouchOutside(false);
			alertDialog.Show();
		}

		#endregion

		#region ILocationListener interface

		/// <summary>
		/// Called when the location changed.
		/// </summary>
		/// <returns>The location changed.</returns>
		/// <param name="location">Location.</param>
		public void OnLocationChanged(Location location)
		{
			Positions.OnNext(new Position()
			{
				Latitude = location.Latitude,
				Longitude = location.Longitude
			});
		}

		/// <summary>
		/// Called when the provider becomes disabled.
		/// </summary>
		/// <returns>The provider disabled.</returns>
		/// <param name="provider">Provider.</param>
		public void OnProviderDisabled(string provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Called when the provider becomes enabled.
		/// </summary>
		/// <returns>The provider enabled.</returns>
		/// <param name="provider">Provider.</param>
		public void OnProviderEnabled(string provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Called when the status changed.
		/// </summary>
		/// <returns>The status changed.</returns>
		/// <param name="provider">Provider.</param>
		/// <param name="status">Status.</param>
		/// <param name="extras">Extras.</param>
		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}