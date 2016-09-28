// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureView.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Controls
{
	using System;

	using Xamarin.Forms;

	/// <summary>
	/// Swipe view.
	/// </summary>
	public class GestureView : View
	{
		#region Public Events

		/// <summary>
		/// Occurs when swipe left.
		/// </summary>
		public event EventHandler SwipeLeft;

		/// <summary>
		/// Occurs when swipe right.
		/// </summary>
		public event EventHandler SwipeRight;

		/// <summary>
		/// Occurs when t.
		/// </summary>
		public event EventHandler Touch;

		#endregion

		#region Public Methods

		/// <summary>
		/// Notifies the swipe left.
		/// </summary>
		public void NotifySwipeLeft()
		{
			if (SwipeLeft != null) 
			{
				SwipeLeft (this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Notifies the swipe right.
		/// </summary>
		public void NotifySwipeRight()
		{
			if (SwipeRight != null) 
			{
				SwipeRight (this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Notifies the touch.
		/// </summary>
		/// <returns>The touch.</returns>
		public void NotifyTouch()
		{
			if (Touch != null)
			{
				Touch(this, EventArgs.Empty);
			}
		}

		#endregion
	}
}