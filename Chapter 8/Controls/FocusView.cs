// --------------------------------------------------------------------------------------------------
//  <copyright file="FocusView.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Controls
{
	using System;
	using System.Threading.Tasks;

	using Xamarin.Forms;

	using Camera.Portable.Enums;

	/// <summary>
	///  This is the control used to embed into Xamarin Forms that will yield a custom rendered for tapping focus
	/// </summary>
	public sealed class FocusView : RelativeLayout
	{
		#region Constant Properties

		/// <summary>
		/// The image target bound.
		/// </summary>
		const int IMG_TARGET_BOUND = 100;

		#endregion

		#region Private Properties

		/// <summary>
		/// The is animating.
		/// </summary>
		private bool _isAnimating;

		/// <summary>
		/// The focal target.
		/// </summary>
		private readonly CustomImage _focalTarget;

		/// <summary>
		/// The p starting orientation.
		/// </summary>
		private Point _pStartingOrientation;

		/// <summary>
		/// The p flipped orientation.
		/// </summary>
		private Point _pFlippedOrientation;

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when focus.
		/// </summary>
		public event EventHandler<Point> TouchFocus;

		#endregion

		#region Public Properties

		/// <summary>
		/// The orientation.
		/// </summary>
		public Orientation Orientation;

		#endregion

		#region Private Methods

		/// <summary>
		/// Animates the focal target.
		/// </summary>
		/// <param name="touchPoint">Touch point.</param>
		private async Task AnimateFocalTarget(Point touchPoint)
		{
			_focalTarget.TintColorString = "#007F00";

			var storyboard = new Animation();

			var translationX = new Animation(callback: x => _focalTarget.TranslationX = x,
										  start: touchPoint.X,
										  end: touchPoint.X - (IMG_TARGET_BOUND / 2),
										  easing: Easing.Linear);

			var translationY = new Animation(callback: y => _focalTarget.TranslationY = y,
										  start: touchPoint.Y,
										  end: touchPoint.Y - (IMG_TARGET_BOUND / 2),
										  easing: Easing.Linear);

			var scaleFirst = new Animation(callback: o => _focalTarget.Scale = o,
										  start: 0.5,
										  end: 1,
										  easing: Easing.Linear);

			var fade = new Animation(callback: o => _focalTarget.Opacity = o,
										  start: 1,
										  end: 0.7f,
										  easing: Easing.Linear);

			var scaleSecond = new Animation(callback: o => _focalTarget.Scale = o,
										  start: 1,
										  end: 0.5f,
										  easing: Easing.Linear);

			storyboard.Add(0, 0.01, translationX);
			storyboard.Add(0, 0.01, translationY);
			storyboard.Add(0, 0.01, scaleFirst);
			storyboard.Add(0, 0.5, fade);
			storyboard.Add(0.5, 1, scaleSecond);

			var tcs = new TaskCompletionSource<bool>();
				
			storyboard.Commit(_focalTarget, "_focalTarget", length: 300, finished: async (arg1, arg2) =>
			{
				_focalTarget.TintOn = true;

				await Task.Delay(500);

				_focalTarget.TintColorString = "#FFFFFF";

				_isAnimating = false;

				tcs.TrySetResult(true);
			});

			await tcs.Task;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset()
		{
			switch (Orientation)
			{
				case Orientation.Portrait:
					NotifyFocus(_pStartingOrientation);
					break;
				case Orientation.LandscapeLeft:
				case Orientation.LandscapeRight:
					NotifyFocus(_pFlippedOrientation);
					break;
			}
		}

		/// <summary>
		/// Notifies the focus.
		/// </summary>
		/// <param name="touchPoint">Touch point.</param>
		public void NotifyFocus(Point touchPoint)
		{
			if (_isAnimating) 
			{
				return;
			}

			_focalTarget.Opacity = 0.0f;
			_focalTarget.TintOn = false;
			_isAnimating = true;

			Device.BeginInvokeOnMainThread(async () => await AnimateFocalTarget(touchPoint));

			TouchFocus?.Invoke(this, touchPoint);
		}

		/// <summary>
		/// Sets the focus points.
		/// </summary>
		/// <param name="pStart">P start.</param>
		/// <param name="pFlipped">P flipped.</param>
		public void SetFocusPoints(Point pStart, Point pFlipped)
		{
			_pStartingOrientation = pStart;
			_pFlippedOrientation = pFlipped;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.Controls.FocusView"/> class.
		/// </summary>
		public FocusView()
		{
			_focalTarget = new CustomImage()
			{
				Path = "photo_focus.png",
				BackgroundColor = Color.Transparent,
				TintColorString = "#FFFFFFF",
				Opacity = 0.0f,
				TintOn = false
			} ;

			Children.Add(_focalTarget,
				Constraint.RelativeToParent((parent) =>
					{
						return parent.X;
					} ),
				Constraint.RelativeToParent((parent) =>
					{
						return parent.Y;
					} ),
				Constraint.RelativeToParent((parent) =>
					{
						return IMG_TARGET_BOUND;
					} ),
				Constraint.RelativeToParent((parent) =>
					{
						return IMG_TARGET_BOUND;
					} ));

			BackgroundColor = Color.Transparent;
		}

		#endregion
	}
}