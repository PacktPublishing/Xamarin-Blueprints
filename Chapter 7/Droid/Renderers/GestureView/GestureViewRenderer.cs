// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureViewRenderer.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.ExportRenderer(typeof(FileStorage.Controls.GestureView), typeof(FileStorage.Droid.Renderers.GestureView.GestureViewRenderer))]

namespace FileStorage.Droid.Renderers.GestureView
{
	using Xamarin.Forms.Platform.Android;

	using Android.Views;
	using Android.Widget;

	using FileStorage.Controls;

	/// <summary>
	/// Gesture view renderer.
	/// </summary>
	public class GestureViewRenderer : ViewRenderer<GestureView, LinearLayout>
	{
		#region Private Properties

		/// <summary>
		/// The layout.
		/// </summary>
		private LinearLayout _layout;

		/// <summary>
		/// The listener.
		/// </summary>
		private readonly GestureListener _listener;

		/// <summary>
		/// The detector.
		/// </summary>
		private readonly GestureDetector _detector;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Droid.Renderers.GestureView.GestureViewRenderer"/> class.
		/// </summary>
		public GestureViewRenderer ()
		{
			_listener = new GestureListener ();
			_detector = new GestureDetector (_listener);

			_layout = new LinearLayout (Context);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called when the element changes.
		/// </summary>
		/// <returns>The element changed.</returns>
		/// <param name="e">E.</param>
		protected override void OnElementChanged (ElementChangedEventArgs<GestureView> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement == null) 
			{
				GenericMotion -= HandleGenericMotion;
				Touch -= HandleTouch;
			}

			if (e.OldElement == null) 
			{
				GenericMotion += HandleGenericMotion;
				Touch += HandleTouch;
			}

			if (Element != null) 
			{
				_listener.InitCoreSwipeView(Element);
			}

			SetNativeControl (_layout);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the touch.
		/// </summary>
		/// <returns>The touch.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleTouch (object sender, TouchEventArgs e)
		{
			_detector.OnTouchEvent (e.Event);
		}

		/// <summary>
		/// Handles the generic motion.
		/// </summary>
		/// <returns>The generic motion.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleGenericMotion (object sender, GenericMotionEventArgs e)
		{
			_detector.OnTouchEvent (e.Event);
		}

		#endregion
	}
}