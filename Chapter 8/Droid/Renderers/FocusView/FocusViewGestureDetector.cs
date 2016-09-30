// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FocusViewGestureDetector.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Droid.Renderers
{
	using System;

	using Android.Views;

	/// <summary>
	/// Focus view gesture detector.
	/// </summary>
	public class FocusViewGestureDetector : GestureDetector.SimpleOnGestureListener
	{
		#region Events

		/// <summary>
		/// Occurs when touch.
		/// </summary>
		public event EventHandler<MotionEvent> Touch;

		#endregion

		#region Public Methods

		/// <summary>
		/// Ons the long press.
		/// </summary>
		/// <param name="e">E.</param>
		public override void OnLongPress(MotionEvent e)
		{
			base.OnLongPress(e);
		}

		/// <summary>
		/// Ons the double tap.
		/// </summary>
		/// <returns><c>true</c>, if double tap was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnDoubleTap(MotionEvent e)
		{
			return base.OnDoubleTap(e);
		}

		/// <summary>
		/// Ons the double tap event.
		/// </summary>
		/// <returns><c>true</c>, if double tap event was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnDoubleTapEvent(MotionEvent e)
		{
			return base.OnDoubleTapEvent(e);
		}

		/// <summary>
		/// Ons the single tap up.
		/// </summary>
		/// <returns><c>true</c>, if single tap up was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnSingleTapUp(MotionEvent e)
		{
			return base.OnSingleTapUp(e);
		}

		/// <summary>
		/// Ons down.
		/// </summary>
		/// <returns><c>true</c>, if down was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnDown(MotionEvent e)
		{
			if (Touch != null)
			{
				Touch(this, e);
			}

			return base.OnDown(e);
		}

		/// <summary>
		/// Ons the fling.
		/// </summary>
		/// <returns><c>true</c>, if fling was oned, <c>false</c> otherwise.</returns>
		/// <param name="e1">E1.</param>
		/// <param name="e2">E2.</param>
		/// <param name="velocityX">Velocity x.</param>
		/// <param name="velocityY">Velocity y.</param>
		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			return base.OnFling(e1, e2, velocityX, velocityY);
		}

		/// <summary>
		/// Ons the scroll.
		/// </summary>
		/// <returns><c>true</c>, if scroll was oned, <c>false</c> otherwise.</returns>
		/// <param name="e1">E1.</param>
		/// <param name="e2">E2.</param>
		/// <param name="distanceX">Distance x.</param>
		/// <param name="distanceY">Distance y.</param>
		public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			return base.OnScroll(e1, e2, distanceX, distanceY);
		}

		/// <summary>
		/// Ons the show press.
		/// </summary>
		/// <param name="e">E.</param>
		public override void OnShowPress(MotionEvent e)
		{
			base.OnShowPress(e);
		}

		/// <summary>
		/// Ons the single tap confirmed.
		/// </summary>
		/// <returns><c>true</c>, if single tap confirmed was oned, <c>false</c> otherwise.</returns>
		/// <param name="e">E.</param>
		public override bool OnSingleTapConfirmed(MotionEvent e)
		{
			return base.OnSingleTapConfirmed(e);
		}

		#endregion
	}
}