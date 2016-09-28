// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureLayoutRenderer.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.ExportRenderer(typeof(FileStorage.Controls.GestureView), typeof(FileStorage.iOS.Renderers.GestureView.GestureLayoutRenderer))]

namespace FileStorage.iOS.Renderers.GestureView
{
	using System;
	using System.Reflection;

	using Foundation;
	using UIKit;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.iOS;

	using FileStorage.Controls;

	/// <summary>
	/// Gesture layout renderer.
	/// </summary>
	public class GestureLayoutRenderer : ViewRenderer<GestureView, GestureViewiOS>
    {
		#region Private Properties

		/// <summary>
		/// The swipe view ios.
		/// </summary>
		private GestureViewiOS _swipeViewIOS;

		/// <summary>
		/// The gestures added.
		/// </summary>
		private bool gesturesAdded;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.iOS.Renderers.GestureView.GestureLayoutRenderer"/> class.
		/// </summary>
		public GestureLayoutRenderer()
		{
			_swipeViewIOS = new GestureViewiOS ();
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called when element changes.
		/// </summary>
		/// <returns>The element changed.</returns>
		/// <param name="e">E.</param>
		protected override void OnElementChanged (ElementChangedEventArgs<GestureView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
			{
				SetNativeControl(_swipeViewIOS);
			}

			if (Element != null && !gesturesAdded)
			{
				_swipeViewIOS.InitGestures(Element);
				gesturesAdded = true;
			}
		}

		#endregion
    }
}