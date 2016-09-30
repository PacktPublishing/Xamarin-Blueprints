// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraCaptureListener.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Droid.Renderers.CameraView
{
	using System;

	using Java.IO;

	using Android.Hardware.Camera2;
	using Android.Widget;

	/// <summary>
	/// This CameraCaptureSession.StateListener uses Action delegates to allow 
	/// the methods to be defined inline, as they are defined more than once.
	/// </summary>
	public class CameraCaptureListener : CameraCaptureSession.CaptureCallback
	{
		/// <summary>
		/// Occurs when photo complete.
		/// </summary>
		public event EventHandler PhotoComplete;

		/// <summary>
		/// Ons the capture completed.
		/// </summary>
		/// <param name="session">Session.</param>
		/// <param name="request">Request.</param>
		/// <param name="result">Result.</param>
		public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, 
		                                        TotalCaptureResult result)
		{
			PhotoComplete?.Invoke(this, EventArgs.Empty);
		}
	}
}