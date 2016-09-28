// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageHelpers.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using System;

	using Android.Graphics;
	using Android.Widget;

	/// <summary>
	/// Bitmap helpers.
	/// </summary>
	public static class BitmapHelpers
	{
		#region Public Static Methods

		/// <summary>
		/// Calculates the size of the in sample.
		/// </summary>
		/// <returns>The in sample size.</returns>
		/// <param name="options">Options.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
		public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			// Raw height and width of image
			float height = options.OutHeight;
			float width = options.OutWidth;
			double inSampleSize = 1D;

			if (height > reqHeight || width > reqWidth)
			{
				int halfHeight = (int)(height / 2);
				int halfWidth = (int)(width / 2);

				// Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
				while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
				{
					inSampleSize *= 2;
				}
			}

			return (int)inSampleSize;
		}

		/// <summary>
		/// Creates the bitmap.
		/// </summary>
		/// <returns>The bitmap.</returns>
		/// <param name="imageView">Image view.</param>
		/// <param name="imageData">Image data.</param>
		public static async void CreateBitmap(ImageView imageView, byte[] imageData)
		{
			try
			{
				if (imageData != null) 
				{
					var bm = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length);
					if (bm != null) 
					{
						imageView.SetImageBitmap(bm);
					}
				}
			}
			catch (Exception e) 
			{
				Console.WriteLine ("Bitmap creation failed: " + e);
			}
		}
	}

	#endregion
}