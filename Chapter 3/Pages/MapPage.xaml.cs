// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Pages
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Xamarin.Forms;
	using Xamarin.Forms.Maps;

	using Locator.UI;
	using Locator.Portable.ViewModels;
	using Locator.Portable.Location;

	/// <summary>
	/// Map page.
	/// </summary>
	public partial class MapPage : ContentPage, INavigableXamarinFormsPage
	{
		#region Private Properties

		/// <summary>
		/// The view model.
		/// </summary>
		private MapPageViewModel _viewModel;

		/// <summary>
		/// The location update subscriptions.
		/// </summary>
		private IDisposable _locationUpdateSubscriptions;

		/// <summary>
		/// The closest subscriptions.
		/// </summary>
		private IDisposable _closestSubscriptions;

		/// <summary>
		/// The geocoder.
		/// </summary>
		private Geocoder geocoder;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.Pages.MapPage"/> class.
		/// </summary>
		public MapPage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.Pages.MapPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public MapPage (MapPageViewModel model)
		{
			_viewModel = model;
			BindingContext = model;
			InitializeComponent ();

			Appearing += HandleAppearing;
			Disappearing += HandleDisappearing;

			geocoder = new Geocoder ();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the disappearing.
		/// </summary>
		/// <returns>The disappearing.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleDisappearing (object sender, EventArgs e)
		{
			_viewModel.OnDisppear ();

			if (_locationUpdateSubscriptions != null) 
			{
				_locationUpdateSubscriptions.Dispose ();
			}

			if (_closestSubscriptions != null) 
			{
				_closestSubscriptions.Dispose ();
			}
		}

		/// <summary>
		/// Handles the appearing.
		/// </summary>
		/// <returns>The appearing.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleAppearing (object sender, EventArgs e)
		{
			_viewModel.OnAppear ();

			_locationUpdateSubscriptions = _viewModel.LocationUpdates.Subscribe (LocationChanged);
			_closestSubscriptions = _viewModel.ClosestUpdates.Subscribe (ClosestChanged);
		}

		/// <summary>
		/// Locations the changed.
		/// </summary>
		/// <returns>The changed.</returns>
		/// <param name="position">Position.</param>
		private void LocationChanged (IPosition position)
		{
			try 
			{
				var formsPosition = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);

				geocoder.GetAddressesForPositionAsync(formsPosition)
				        .ContinueWith(_ =>
						{
							var mostRecent = _.Result.FirstOrDefault();
							if (mostRecent != null)
							{
								_viewModel.Address = mostRecent;
							}
						})
				        .ConfigureAwait(false);

				MapView.MoveToRegion(MapSpan.FromCenterAndRadius(formsPosition, Distance.FromMiles(0.3)));
			}
			catch (Exception e) 
			{
				System.Diagnostics.Debug.WriteLine ("MapPage: Error with moving map region - " + e);
			}
		}

		/// <summary>
		/// Closests the changed.
		/// </summary>
		/// <returns>The changed.</returns>
		/// <param name="position">Position.</param>
		private void ClosestChanged (IPosition position)
		{
			try 
			{
				var pin = new Pin()
				{
					Type = PinType.Place,
					Position = new Xamarin.Forms.Maps.Position (position.Latitude, position.Longitude),
					Label = "Closest Location",
					Address = position.Address
				};

				MapView.Pins.Add(pin);

				MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)
				                                                 , Distance.FromMiles(0.3)));
			}
			catch (Exception e) 
			{
				System.Diagnostics.Debug.WriteLine ("MapPage: Error with moving pin - " + e);
			}
		}

		#endregion

		#region INavigableXamarinFormsPage interface

		/// <summary>
		/// Called when page is navigated to.
		/// </summary>
		/// <returns>The navigated to.</returns>
		/// <param name="navigationParameters">Navigation parameters.</param>
		public void OnNavigatedTo(IDictionary<string, object> navigationParameters)
		{
			this.Show(navigationParameters);
		}

		#endregion
	}
}