// --------------------------------------------------------------------------------------------------
//  <copyright file="IPosition.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.Portable.Location
{
	using System;

	/// <summary>
	/// The position interface.
	/// </summary>
	public interface IPosition
	{
		#region Properties

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		double Latitude {get; set;}

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		double Longitude {get; set;}

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		string Address {get; set;}

		#endregion
	}
}