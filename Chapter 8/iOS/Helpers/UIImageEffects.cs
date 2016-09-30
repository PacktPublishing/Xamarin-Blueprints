// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIImageEffects.cs" company="Flush Arcade">
//   Copyright (c) 2016 Flush Arcade All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.iOS.Helpers
{
	using UIKit;
	using CoreGraphics;

	/// <summary>
	/// User interface image effects.
	/// </summary>
	public static class UIImageEffects
	{
		/// <summary>
		/// Gets the colored image.
		/// </summary>
		/// <returns>The colored image.</returns>
		/// <param name="image">Image.</param>
		/// <param name="color">Color.</param>
		public static UIImage GetColoredImage(UIImage image, UIColor color)
		{
			UIImage coloredImage = null;

			UIGraphics.BeginImageContext(image.Size);

			using (CGContext context = UIGraphics.GetCurrentContext())
			{
				context.TranslateCTM(0, image.Size.Height);
				context.ScaleCTM(1.0f, -1.0f);

				var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);

				// draw image, (to get transparancy mask)
				context.SetBlendMode(CGBlendMode.Normal);
				context.DrawImage(rect, image.CGImage);

				// draw the color using the sourcein blend mode so its only draw on the non-transparent pixels
				context.SetBlendMode(CGBlendMode.SourceIn);
				context.SetFillColor(color.CGColor);
				context.FillRect(rect);

				coloredImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
			}

			return coloredImage;
		}
	}
}