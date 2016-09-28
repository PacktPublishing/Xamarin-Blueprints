// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureViewIOS.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.iOS.Renderers.GestureView
{
	using UIKit;
	using Foundation;

	using FileStorage.Controls;

	/// <summary>
	/// Gesture viewi os.
	/// </summary>
	[Register("GestureViewiOS")]
	public sealed class GestureViewiOS : UIView
	{
		#region Private Properties

		/// <summary>
		/// The main view.
		/// </summary>
		private UIView _mainView;

		/// <summary>
		/// The swipe left gesture recognizer.
		/// </summary>
		private UISwipeGestureRecognizer _swipeLeftGestureRecognizer;

		/// <summary>
		/// The swipe right gesture recognizer.
		/// </summary>
		private UISwipeGestureRecognizer _swipeRightGestureRecognizer;

		/// <summary>
		/// The tap gesture recognizer.
		/// </summary>
		private UITapGestureRecognizer _tapGestureRecognizer;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.iOS.Renderers.GestureView.GestureViewiOS"/> class.
		/// </summary>
		public GestureViewiOS()
		{
			_mainView = new UIView() { TranslatesAutoresizingMaskIntoConstraints = false };
			_mainView.BackgroundColor = UIColor.Clear;

			Add(_mainView);

			// set layout constraints for main view
			AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[mainView]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("mainView", _mainView)));
			AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[mainView]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary("mainView", _mainView)));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Inits the gestures.
		/// </summary>
		/// <returns>The gestures.</returns>
		/// <param name="swipeView">Swipe view.</param>
		public void InitGestures(GestureView swipeView)
		{
			_swipeLeftGestureRecognizer = new UISwipeGestureRecognizer(swipeView.NotifySwipeLeft);
			_swipeLeftGestureRecognizer.Direction = UISwipeGestureRecognizerDirection.Left;
			_swipeRightGestureRecognizer = new UISwipeGestureRecognizer(swipeView.NotifySwipeRight);
			_swipeRightGestureRecognizer.Direction = UISwipeGestureRecognizerDirection.Right;
			_tapGestureRecognizer = new UITapGestureRecognizer(swipeView.NotifyTouch);
			_tapGestureRecognizer.NumberOfTapsRequired = 1;

			_mainView.AddGestureRecognizer(_swipeLeftGestureRecognizer);
			_mainView.AddGestureRecognizer(_swipeRightGestureRecognizer);
			_mainView.AddGestureRecognizer(_tapGestureRecognizer);
		}

		#endregion
	}
}