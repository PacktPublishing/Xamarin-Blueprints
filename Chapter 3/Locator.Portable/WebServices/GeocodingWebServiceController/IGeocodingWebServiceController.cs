// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeocodingWebServiceController.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.WebServices.GeocodingWebServiceController
{
	using System;

	using Locator.Portable.Repositories.GeocodingWebServiceController.Contracts;

	/// <summary>
	/// The geocoding web service controller interface.
	/// </summary>
	public interface IGeocodingWebServiceController
	{
		#region Methods

		/// <summary>
		/// Gets the geocode from address async.
		/// </summary>
		/// <returns>The geocode from address async.</returns>
		/// <param name="address">Address.</param>
		/// <param name="city">City.</param>
		/// <param name="state">State.</param>
		IObservable<GeocodingContract> GetGeocodeFromAddressAsync (string address, string city, string state);

		#endregion
	}
}