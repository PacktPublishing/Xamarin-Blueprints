// --------------------------------------------------------------------------------------------------
//  <copyright file="CameraView.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Controls
{
	using System;

	using Xamarin.Forms;

	using Camera.Portable.Enums;

	/// <summary>
	/// Orientation page.
	/// </summary>
	public class OrientationPage : ContentPage
	{
		#region Static Properties

		/// <summary>
		/// The page orientation.
		/// </summary>
		public static Orientation PageOrientation;

		/// <summary>
		/// Occurs when orientation handler.
		/// </summary>
		public static event EventHandler<Orientation> OrientationHandler;

		/// <summary>
		/// Occurs when touch handler.
		/// </summary>
		public static event EventHandler<Point> TouchHandler;

		#endregion

		#region Static Methods

		/// <summary>
		/// Notifies the orientation change.
		/// </summary>
		/// <param name="orientation">Orientation.</param>
		public static void NotifyOrientationChange(Orientation orientation)
		{
			if (OrientationHandler != null)
			{
				OrientationHandler (null, orientation);
			}
		}

		/// <summary>
		/// Notifies the touch.
		/// </summary>
		/// <param name="touchPoint">Touch point.</param>
		public static void NotifyTouch(Point touchPoint)
		{
			if (TouchHandler != null)
			{
				TouchHandler(null, touchPoint);
			}
		}

		#endregion
	}
}