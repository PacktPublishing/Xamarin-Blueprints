// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapPageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.ViewModels
{
	using System;
	using System.Reactive.Subjects;
	using System.Reactive.Linq;
	using System.Linq;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Windows.Input;

	using Locator.Portable.UI;
	using Locator.Portable.Location;
	using Locator.Portable.WebServices.GeocodingWebServiceController;

	/// <summary>
	/// Map page view model.
	/// </summary>
	public class MapPageViewModel : ViewModelBase
	{
		#region Constants

		/// <summary>
		/// The addresses.
		/// </summary>
		private IDictionary<int, string[]> _addresses = new Dictionary<int, string[]>()
		{
			{0, new string[] { "120 Rosamond Rd", "Melbourne", "Victoria" }},
			{1, new string[] { "367 George Street", "Sydney", "New South Wales" }},
			{2, new string[] { "790 Hay St", "Perth", "Western Australi" }},
			{3, new string[] { "77-90 Rundle Mall", "Adelaide", "South Australia" }},
			{4, new string[] { "233 Queen Street", "Brisbane", "Queensland" }},
		};

		#endregion

		#region Subjects

		/// <summary>
		/// Gets or sets the location updates.
		/// </summary>
		/// <value>The location updates.</value>
		public Subject<IPosition> LocationUpdates { get; set; } 

		/// <summary>
		/// Gets or sets the closest updates.
		/// </summary>
		/// <value>The closest updates.</value>
		public Subject<IPosition> ClosestUpdates { get; set; } 

		#endregion

		#region Private Properties
	
		/// <summary>
		/// The positions.
		/// </summary>
		private IList<IPosition> _positions;

		/// <summary>
		/// The current position.
		/// </summary>
		private IPosition _currentPosition;

		/// <summary>
		/// The subscriptions.
		/// </summary>
		private IDisposable _subscriptions;

		/// <summary>
		/// The geolocator.
		/// </summary>
		private readonly IGeolocator _geolocator;

		/// <summary>
		/// The geocoding web service controller.
		/// </summary>
		private readonly IGeocodingWebServiceController _geocodingWebServiceController;

		/// <summary>
		/// The address.
		/// </summary>
		private string _address;

		/// <summary>
		/// The closest address.
		/// </summary>
		private string _closestAddress;

		/// <summary>
		/// The geolocation button title.
		/// </summary>
		private string _geolocationButtonTitle = "Start";

		/// <summary>
		/// The geolocation updating.
		/// </summary>
		private bool _geolocationUpdating;

		/// <summary>
		/// The geocodes complete.
		/// </summary>
		private int _geocodesComplete = 0;

		/// <summary>
		/// The nearest address command.
		/// </summary>
		private ICommand _nearestAddressCommand;

		/// <summary>
		/// The geolocation command.
		/// </summary>
		private ICommand _geolocationCommand;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address
		{
			get
			{
				return _address;
			}

			set
			{
				if (value.Equals(_address))
				{
					return;
				}

				_address = value;
				OnPropertyChanged("Address");
			}
		}

		/// <summary>
		/// Gets or sets the closest address.
		/// </summary>
		/// <value>The closest address.</value>
		public string ClosestAddress
		{
			get
			{
				return _closestAddress;
			}

			set
			{
				if (value.Equals(_closestAddress))
				{
					return;
				}

				_closestAddress = value;
				OnPropertyChanged("ClosestAddress");
			}
		}

		/// <summary>
		/// Gets or sets the geolocation button title.
		/// </summary>
		/// <value>The geolocation button title.</value>
		public string GeolocationButtonTitle
		{
			get
			{
				return _geolocationButtonTitle;
			}

			set
			{
				if (value.Equals(_geolocationButtonTitle))
				{
					return;
				}

				_geolocationButtonTitle = value;
				OnPropertyChanged("GeolocationButtonTitle");
			}
		}

		/// <summary>
		/// Gets or sets the nearest address command.
		/// </summary>
		/// <value>The nearest address command.</value>
		public ICommand NearestAddressCommand
		{
			get
			{
				return _nearestAddressCommand;
			}

			set
			{
				if (value.Equals(_nearestAddressCommand))
				{
					return;
				}

				_nearestAddressCommand = value;
				OnPropertyChanged("NearestAddressCommand");
			}
		}

		/// <summary>
		/// Gets or sets the geolocation command.
		/// </summary>
		/// <value>The geolocation command.</value>
		public ICommand GeolocationCommand
		{
			get
			{
				return _geolocationCommand;
			}

			set
			{
				if (value.Equals(_geolocationCommand))
				{
					return;
				}

				_geolocationCommand = value;
				OnPropertyChanged("GeolocationCommand");
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.Portable.ViewModels.MapPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="geolocator">Geolocator.</param>
		/// <param name="commandFactory">Command factory.</param>
		/// <param name="geocodingWebServiceController">Geocoding repository.</param>
		public MapPageViewModel (INavigationService navigation, IGeolocator geolocator, Func<Action, ICommand> commandFactory, 
			IGeocodingWebServiceController geocodingWebServiceController) : base (navigation)
		{
			_geolocator = geolocator;
			_geocodingWebServiceController = geocodingWebServiceController;

			_nearestAddressCommand = commandFactory(() => FindNearestSite());
			_geolocationCommand = commandFactory(() =>
			{
				if (_geolocationUpdating)
				{
					geolocator.Stop();
				}
				else
				{
					geolocator.Start();
				}

				GeolocationButtonTitle = _geolocationUpdating ? "Start" : "Stop";
				_geolocationUpdating = !_geolocationUpdating;
			});

			_positions = new List<IPosition> ();

			LocationUpdates = new Subject<IPosition> ();
			ClosestUpdates = new Subject<IPosition> ();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Degreeses to radians.
		/// </summary>
		/// <returns>The to radians.</returns>
		/// <param name="deg">Deg.</param>
		private double DegreesToRadians(double deg) 
		{
			return deg * Math.PI / 180;
		}

		/// <summary>
		/// Pythagorases the equirectangular.
		/// </summary>
		/// <returns>The equirectangular.</returns>
		/// <param name="lat1">Lat1.</param>
		/// <param name="lon1">Lon1.</param>
		/// <param name="lat2">Lat2.</param>
		/// <param name="lon2">Lon2.</param>
		private double PythagorasEquirectangular(double lat1, double lon1, double lat2, double lon2)
		{
			lat1 = DegreesToRadians(lat1);
			lat2 = DegreesToRadians(lat2);
			lon1 = DegreesToRadians(lon1);
			lon2 = DegreesToRadians(lon2);

			// within a 5km radius
			var radius = 5;
			var x = (lon2 - lon1) * Math.Cos((lat1 + lat2) / 2);
			var y = (lat2 - lat1);
			var distance = Math.Sqrt(x * x + y * y) * radius;

			return distance;
		}

		/// <summary>
		/// Finds the nearest site.
		/// </summary>
		private void FindNearestSite()
		{
			if (_geolocationUpdating)
			{
				_geolocationUpdating = false;
				_geolocator.Stop();
				GeolocationButtonTitle = "Start";
			}

			double mindif = 99999;
			IPosition closest = null;
			var closestIndex = 0;
			var index = 0;

			if (_currentPosition != null)
			{
				foreach (var position in _positions)
				{
					var difference = PythagorasEquirectangular(_currentPosition.Latitude, _currentPosition.Longitude,
						position.Latitude, position.Longitude);

					if (difference < mindif)
					{
						closest = position;
						closestIndex = index;
						mindif = difference;
					}

					index++;
				}

				if (closest != null)
				{
					var array = _addresses[closestIndex];
					Address = string.Format("{0}, {1}, {2}", array[0], array[1], array[2]);
					ClosestUpdates.OnNext(closest);			
				}
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the geocode from address.
		/// </summary>
		/// <returns>The geocode from address.</returns>
		/// <param name="address">Address.</param>
		/// <param name="city">City.</param>
		/// <param name="state">State.</param>
		public async Task GetGeocodeFromAddress(string address, string city, string state)
		{
			var geoContract = await _geocodingWebServiceController.GetGeocodeFromAddressAsync(address, city, state);

			if (geoContract != null && geoContract.results != null && geoContract.results.Count > 0)
			{
				var result = geoContract.results.FirstOrDefault();

				if (result != null && result.geometry != null && result.geometry.location != null)
				{
					_geocodesComplete++;

					_positions.Add(new Position()
					{
						Latitude = result.geometry.location.lat,
						Longitude = result.geometry.location.lng,
						Address = string.Format("{0}, {1}, {2}", address, city, state)
					});

					// once all geocodes are found, find the closest
					if ((_geocodesComplete == _positions.Count) && _currentPosition != null)
					{
						FindNearestSite();
					}
				}
			}
		}

		/// <summary>
		/// Ons the appear.
		/// </summary>
		/// <returns>The appear.</returns>
		public void OnAppear()
		{
			_subscriptions = _geolocator.Positions.Subscribe (x => 
				{
					_currentPosition = x;
					LocationUpdates.OnNext(x);
				});
		}

		/// <summary>
		/// Ons the disppear.
		/// </summary>
		/// <returns>The disppear.</returns>
		public void OnDisppear()
		{
			_geolocator.Stop ();

			if (_subscriptions != null) 
			{
				_subscriptions.Dispose ();
			}
		}

		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="parameters">Parameters.</param>
		protected override async Task LoadAsync (IDictionary<string, object> parameters)
		{
			var index = 0;

			// all 5 tasks will run in parallel
			for (int i = 0; i < 5; i++)
			{
				var array = _addresses [index];
				index++;

				GetGeocodeFromAddress(array[0], array[1], array[2]).ConfigureAwait(false);
			}
		}

		#endregion
	}
}