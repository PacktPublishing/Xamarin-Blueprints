// --------------------------------------------------------------------------------------------------
//  <copyright file="Position.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.Portable.Location
{
	/// <summary>
	/// The position interface.
	/// </summary>
	public class Position : IPosition
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double Latitude {get; set;}

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double Longitude {get; set;}

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address {get; set;}

		#endregion
	}
}