// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrollChange.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.UI
{
	/// <summary>
	/// Scroll change.
	/// </summary>
	public class ScrollChange
	{
		#region Properties

		/// <summary>
		/// Gets or sets the scroll y.
		/// </summary>
		/// <value>The scroll y.</value>
		public double ScrollY { get; set; }

		/// <summary>
		/// Gets or sets the scroll x.
		/// </summary>
		/// <value>The scroll x.</value>
		public double ScrollX { get; set; }

		/// <summary>
		/// Gets or sets the duration of the scroll.
		/// </summary>
		/// <value>The duration of the scroll.</value>
		public double ScrollDuration { get; set; }

		#endregion
	}
}