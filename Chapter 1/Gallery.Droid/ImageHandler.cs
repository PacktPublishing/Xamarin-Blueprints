// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageHandler.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using System.Collections.Generic;
	using System.IO;

	using Android.Content;
	using Android.Provider;
	using Android.Graphics;

	using Gallery.Shared;

	/// <summary>
	/// Image handler.
	/// </summary>
	public static class ImageHandler
	{
		#region Public Static Methods

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		public static IEnumerable<GalleryItem> GetFiles(Context context)
		{
			ContentResolver cr = context.ContentResolver;

			string[] columns = new string[] 
			{
				MediaStore.Images.ImageColumns.Id,
				MediaStore.Images.ImageColumns.Title,
				MediaStore.Images.ImageColumns.Data,
				MediaStore.Images.ImageColumns.DateAdded,
				MediaStore.Images.ImageColumns.MimeType,
				MediaStore.Images.ImageColumns.Size,
			};
			
			var cursor = cr.Query(MediaStore.Images.Media.ExternalContentUri, columns, null, null, null);

			int columnIndex = cursor.GetColumnIndex(columns[2]);

			int index = 0;

			// create max 100 items
			while (cursor.MoveToNext () && index < 100) 
			{
				index++;

				var url = cursor.GetString(columnIndex);

				var imageData = CreateCompressedImageDataFromBitmap (url);

				yield return new GalleryItem () 
				{
					Title = cursor.GetString(1),
					Date = cursor.GetString(3),
					ImageData = imageData,
					ImageUri = url,
				};
			}
		}

		#endregion

		#region Private Static Methods

		/// <summary>
		/// Creates the compressed image data from bitmap.
		/// </summary>
		/// <returns>The compressed image data from bitmap.</returns>
		/// <param name="url">URL.</param>
		private static byte[] CreateCompressedImageDataFromBitmap(string url)
		{
			BitmapFactory.Options options = new BitmapFactory.Options ();
			// This makes sure bitmap is not loaded into memory
			options.InJustDecodeBounds = true;
			// Then get the properties of the bitmap
			BitmapFactory.DecodeFile (url, options);
			// CalculateInSampleSize calculates the right aspect ratio for the picture and then calculate
			// the factor where it will be downsampled with.
			options.InSampleSize = BitmapHelpers.CalculateInSampleSize (options, 1600, 1200);
			// Now that we know the downsampling factor, the right sized bitmap is loaded into memory.
			// So we set the InJustDecodeBounds to false because we now know the exact dimensions.
			options.InJustDecodeBounds = false;

			// Now we are loading it with the correct options. And saving precious memory.
			Bitmap bm = BitmapFactory.DecodeFile (url, options);

			// Convert it to Base64 by first converting the bitmap to
			// a byte array. Then convert the byte array to a Base64 String.
			var stream = new MemoryStream ();
			bm.Compress (Bitmap.CompressFormat.Jpeg, 80, stream);
			return stream.ToArray ();
		}

		#endregion
	}
}