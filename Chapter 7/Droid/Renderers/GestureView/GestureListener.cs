// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureListener.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Droid.Renderers.GestureView
{
	using System;

	using Android.Views;

	using FileStorage.Controls;

	/// <summary>
	/// Gesture listener.
	/// </summary>
	public class GestureListener : GestureDetector.SimpleOnGestureListener
	{
		#region Private Properties

		/// <summary>
		/// The SWIPE_THRESHOLD.
		/// </summary>
		private const int SWIPE_THRESHOLD = 50;

		/// <summary>
		/// The SWIPE_VELOCITY_THRESHOLD.
		/// </summary>
		private const int SWIPE_VELOCITY_THRESHOLD = 50;

		/// <summary>
		/// The swipe view.
		/// </summary>
		private GestureView _swipeView;

		#endregion

		#region Constructors

		/// <summary>
		/// Inits the core swipe view.
		/// </summary>
		/// <param name="swipeView">Swipe view.</param>
		public void InitCoreSwipeView(GestureView swipeView)
		{
			_swipeView = swipeView;
		}

		#endregion

		#region Public Overrides

		/// <summary>
		/// Raises the long press event.
		/// </summary>
		/// <param name="e">E.</param>
		public override void OnLongPress (MotionEvent e)
		{
			base.OnLongPress (e);
		}

		/// <summary>
		/// Raises the double tap event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnDoubleTap (MotionEvent e)
		{
			return base.OnDoubleTap (e);
		}

		/// <summary>
		/// Raises the double tap event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnDoubleTapEvent (MotionEvent e)
		{
			return base.OnDoubleTapEvent (e);
		}

		/// <summary>
		/// Raises the single tap up event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnSingleTapUp (MotionEvent e)
		{
			_swipeView.NotifyTouch();
			return base.OnSingleTapUp (e);
		}

		/// <summary>
		/// Raises the down event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnDown (MotionEvent e)
		{
			return base.OnDown (e);
		}

		/// <summary>
		/// Raises the fling event.
		/// </summary>
		/// <param name="e1">E1.</param>
		/// <param name="e2">E2.</param>
		/// <param name="velocityX">Velocity x.</param>
		/// <param name="velocityY">Velocity y.</param>
		public override bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			try
			{
			float diffY = e2.GetY() - e1.GetY();
			float diffX = e2.GetX() - e1.GetX();

			if (Math.Abs(diffX) > Math.Abs(diffY)) 
			{
				if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
				{
					if (_swipeView != null)
					{	
						if (diffX > 0) 
						{
							_swipeView.NotifySwipeRight ();
						}
						else 
						{
							_swipeView.NotifySwipeLeft ();
						}
					}
				}
			} 
			}
			catch (Exception) 
			{
			}

			return base.OnFling (e1, e2, velocityX, velocityY);
		}

		/// <summary>
		/// Raises the scroll event.
		/// </summary>
		/// <param name="e1">E1.</param>
		/// <param name="e2">E2.</param>
		/// <param name="distanceX">Distance x.</param>
		/// <param name="distanceY">Distance y.</param>
		public override bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			return base.OnScroll (e1, e2, distanceX, distanceY);
		}

		/// <summary>
		/// Raises the show press event.
		/// </summary>
		/// <param name="e">E.</param>
		public override void OnShowPress (MotionEvent e)
		{
			base.OnShowPress (e);
		}

		/// <summary>
		/// Raises the single tap confirmed event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnSingleTapConfirmed (MotionEvent e)
		{
			return base.OnSingleTapConfirmed (e);
		}

		#endregion
	}
}