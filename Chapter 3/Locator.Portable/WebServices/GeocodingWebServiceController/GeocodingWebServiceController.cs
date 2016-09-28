// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeocodingWebServiceController.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.WebServices.GeocodingWebServiceController
{
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Reactive.Linq;
	using System.Threading;

	using Newtonsoft.Json;

	using Locator.Portable.Repositories.GeocodingWebServiceController.Contracts;
	using Locator.Portable.Resources;

	/// <summary>
	/// Geocoding web service controller.
	/// </summary>
	public sealed class GeocodingWebServiceController : IGeocodingWebServiceController
	{
		#region Fields

		/// <summary>
		/// The client handler.
		/// </summary>
		private readonly HttpClientHandler _clientHandler;

		#endregion

		#region Constructors

		public GeocodingWebServiceController(HttpClientHandler clientHandler)
		{
			_clientHandler = clientHandler;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the geocode from address async.
		/// </summary>
		/// <returns>The geocode from address async.</returns>
		/// <param name="address">Address.</param>
		/// <param name="city">City.</param>
		/// <param name="state">State.</param>
		public IObservable<GeocodingContract> GetGeocodeFromAddressAsync(string address, string city, string state)
		{
			var authClient = new HttpClient(_clientHandler);

			var message = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(ApiConfig.GoogleMapsUrl, address, city, state)));

			return Observable.FromAsync(() => authClient.SendAsync(message, new CancellationToken(false)))
				.SelectMany(async response =>
					{
						if (response.StatusCode != HttpStatusCode.OK)
						{
							throw new Exception("Respone error");
						}

						return await response.Content.ReadAsStringAsync();
					})
				.Select(json => JsonConvert.DeserializeObject<GeocodingContract>(json));
		}

		#endregion
	}
}