// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeocodingContract.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.Repositories.GeocodingWebServiceController.Contracts
{
	using System.Collections.Generic;

    /// <summary>
	/// AccountContract
    /// </summary>
	public sealed class GeocodingContract
    {
        #region Public Properties

		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>The results.</value>
		public List<GeocodingResultContract> results { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public string status { get; set; }

        #endregion
    }
}