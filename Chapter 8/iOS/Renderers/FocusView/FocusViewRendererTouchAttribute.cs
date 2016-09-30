// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FocusViewRendererTouchAttribute.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.ExportRendererAttribute (typeof(Camera.Controls.FocusView), 
                                                  typeof(Camera.iOS.Renderers.FocusView.FocusViewRendererTouchAttribute))]

namespace Camera.iOS.Renderers.FocusView
{
	using Xamarin.Forms.Platform.iOS;

	using Foundation;
	using UIKit;

	using Camera.Controls;

	/// <summary>
	/// Focus view renderer touch attribute.
	/// </summary>
	public class FocusViewRendererTouchAttribute : VisualElementRenderer<FocusView>
	{
		#region Public Methods

		/// <summary>
		/// Toucheses the began.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			FocusView focusView = ((FocusView)this.Element);

			// Get the current touch
			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) 
			{
				var posc = touch.LocationInView (touch.View);
				focusView.NotifyFocus (new Xamarin.Forms.Point(posc.X, posc.Y));
			}
		}

		#endregion
	}
}