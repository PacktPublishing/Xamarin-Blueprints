// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewportContract.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.Repositories.GeocodingWebServiceController.Contracts
{
    /// <summary>
    /// Viewport contract.
    /// </summary>
	public sealed class ViewportContract
    {
        #region Public Properties

		/// <summary>
		/// Gets or sets the northeast.
		/// </summary>
		/// <value>The northeast.</value>
		public NortheastContract northeast { get; set; }

		/// <summary>
		/// Gets or sets the southwest.
		/// </summary>
		/// <value>The southwest.</value>
		public SouthwestContract southwest { get; set; }

        #endregion
    }
}