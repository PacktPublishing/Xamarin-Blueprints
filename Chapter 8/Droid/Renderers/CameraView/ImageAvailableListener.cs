// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageAvailableListener.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Droid.Renderers.CameraView
{
	using System;

	using Java.Nio;
	using Java.IO;

	using Android.Media;
	using Android.Graphics;
	using System.IO;

	/// <summary>
	/// This CameraCaptureSession.StateListener uses Action delegates to allow 
	/// the methods to be defined inline, as they are defined more than once.
	/// </summary>
	public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
	{
		/// <summary>
		/// Occurs when photo.
		/// </summary>
		public event EventHandler<byte[]> Photo;

		/// <summary>
		/// Ons the image available.
		/// </summary>
		/// <param name="reader">Reader.</param>
		public void OnImageAvailable(ImageReader reader)
		{
			Image image = null;

			try
			{
				image = reader.AcquireLatestImage();
				ByteBuffer buffer = image.GetPlanes()[0].Buffer;
				byte[] imageData = new byte[buffer.Capacity()];
				buffer.Get(imageData);

				Photo?.Invoke(this, imageData);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (image != null)
				{
					image.Close();
				}
			}
		}
	}
}