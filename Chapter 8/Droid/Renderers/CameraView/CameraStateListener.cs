// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraDroid.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Droid.Renderers.CameraView
{
	using Android.Hardware.Camera2;

	/// <summary>
	/// Camera state listener.
	/// </summary>
	public class CameraStateListener : CameraDevice.StateCallback
	{
		/// <summary>
		/// The camera.
		/// </summary>
		public CameraDroid Camera;

		/// <summary>
		/// Called when camera is connected.
		/// </summary>
		/// <param name="camera">Camera.</param>
		public override void OnOpened(CameraDevice camera)
		{
			if (Camera != null)
			{
				Camera._cameraDevice = camera;
				Camera.StartPreview();
				Camera.OpeningCamera = false;

				Camera?.NotifyAvailable(true);
			}
		}

		/// <summary>
		/// Called when camera is disconnected.
		/// </summary>
		/// <param name="camera">Camera.</param>
		public override void OnDisconnected(CameraDevice camera)
		{
			if (Camera != null)
			{
				camera.Close();
				Camera._cameraDevice = null;
				Camera.OpeningCamera = false;

				Camera?.NotifyAvailable(false);
			}
		}

		/// <summary>
		/// Called when an error occurs.
		/// </summary>
		/// <param name="camera">Camera.</param>
		/// <param name="error">Error.</param>
		public override void OnError(CameraDevice camera, CameraError error)
		{
			camera.Close();

			if (Camera != null)
			{
				Camera._cameraDevice = null;
				Camera.OpeningCamera = false;

				Camera?.NotifyAvailable(false);
			}
		}
	}
}