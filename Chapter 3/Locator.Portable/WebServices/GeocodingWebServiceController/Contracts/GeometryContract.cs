// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryContract.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.Repositories.GeocodingWebServiceController.Contracts
{
	using System.Collections.Generic;

    /// <summary>
    /// Geometry contract.
    /// </summary>
	public sealed class GeometryContract
    {
        #region Public Properties

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public LocationContract location { get; set; }

		/// <summary>
		/// Gets or sets the type of the location.
		/// </summary>
		/// <value>The type of the location.</value>
		public string location_type { get; set; }

		/// <summary>
		/// Gets or sets the viewport.
		/// </summary>
		/// <value>The viewport.</value>
		public ViewportContract viewport { get; set; }

        #endregion
    }
}