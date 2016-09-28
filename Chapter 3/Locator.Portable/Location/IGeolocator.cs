// --------------------------------------------------------------------------------------------------
//  <copyright file="IGeolocator.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.Portable.Location
{
	using System.Reactive.Subjects;

	/// <summary>
	/// Geolocator.
	/// </summary>
	public interface IGeolocator
	{
		#region Properties

		/// <summary>
		/// Gets or sets the positions.
		/// </summary>
		/// <value>The positions.</value>
		Subject<IPosition> Positions { get; set; } 

		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start();

		/// <summary>
		/// Stop this instance.
		/// </summary>
		void Stop();

		#endregion
	}
}