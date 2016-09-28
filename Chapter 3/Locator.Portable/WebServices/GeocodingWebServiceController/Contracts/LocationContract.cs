// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryContract.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.Repositories.GeocodingWebServiceController.Contracts
{
    /// <summary>
    /// Geometry contract.
    /// </summary>
	public sealed class LocationContract
    {
        #region Public Properties

		/// <summary>
		/// Gets or sets the lat.
		/// </summary>
		/// <value>The lat.</value>
		public double lat { get; set; }

		/// <summary>
		/// Gets or sets the lng.
		/// </summary>
		/// <value>The lng.</value>
		public double lng { get; set; }

        #endregion
    }
}