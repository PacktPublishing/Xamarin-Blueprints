// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraIOS.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.iOS.Renderers.CameraView
{
	using System;
	using System.Threading.Tasks;
	using System.Globalization;

	using Xamarin.Forms;

	using Foundation;
	using UIKit;
	using AVFoundation;
	using CoreGraphics;

	using Camera.iOS.Hardware;

	using Camera.Portable.Enums;
	using Camera.Portable.Logging;
	using Camera.Portable.Ioc;

	/// <summary>
	/// Camera ios.
	/// </summary>
	public sealed class CameraIOS : UIView
	{
		#region Private Properties

		/// <summary>
		/// The tag.
		/// </summary>
		private readonly string _tag;

		/// <summary>
		/// The log.
		/// </summary>
		private readonly ILogger _log;

		/// <summary>
		/// The preview layer.
		/// </summary>
		private readonly AVCaptureVideoPreviewLayer _previewLayer;

		/// <summary>
		/// The capture session.
		/// </summary>
		private readonly AVCaptureSession _captureSession;

		/// <summary>
		/// The main view.
		/// </summary>
		private UIView _mainView;

		/// <summary>
		/// The input.
		/// </summary>
		private AVCaptureDeviceInput _input;

		/// <summary>
		/// The output.
		/// </summary>
		private AVCaptureStillImageOutput _output;

		/// <summary>
		/// The capture connection.
		/// </summary>
		private AVCaptureConnection _captureConnection;

		/// <summary>
		/// The device.
		/// </summary>
		private AVCaptureDevice _device;

		/// <summary>
		/// The camera busy.
		/// </summary>
		private bool _cameraBusy;

		/// <summary>
		/// The camera available.
		/// </summary>
		private bool _cameraAvailable;

		/// <summary>
		/// The width of the camera button container.
		/// </summary>
		private float _cameraButtonContainerWidth;

		/// <summary>
		/// The image scale.
		/// </summary>
		private float _imgScale = 1.25f;

		/// <summary>
		/// The system version.
		/// </summary>
		private double _systemVersion;

		/// <summary>
		/// The width.
		/// </summary>
		private nint _width;

		/// <summary>
		/// The height.
		/// </summary>
		private nint _height;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when busy.
		/// </summary>
		public event EventHandler<bool> Busy;

		/// <summary>
		/// Occurs when available.
		/// </summary>
		public event EventHandler<bool> Available;

		/// <summary>
		/// Occurs when photo.
		/// </summary>
		public event EventHandler<byte[]> Photo;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.iOS.Renderers.CameraView.CameraIOS"/> class.
		/// </summary>
		public CameraIOS()
		{
			_log = IoC.Resolve<ILogger>();
			_tag = $"{GetType()} ";

			// retrieve system version 
			var versionParts = UIDevice.CurrentDevice.SystemVersion.Split ('.');
			var versionString = versionParts [0] + "." + versionParts [1];
			_systemVersion = Convert.ToDouble (versionString, CultureInfo.InvariantCulture);

			_mainView = new UIView () { TranslatesAutoresizingMaskIntoConstraints = false };
			AutoresizingMask = UIViewAutoresizing.FlexibleMargins;

			_captureSession = new AVCaptureSession();

			_previewLayer = new AVCaptureVideoPreviewLayer(_captureSession)
			{
				VideoGravity = AVLayerVideoGravity.Resize
			};

			_mainView.Layer.AddSublayer (_previewLayer);

			// retrieve camera device if available
			_cameraAvailable = RetrieveCameraDevice ();

			Add (_mainView);

			// set layout constraints for main view
			AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[mainView]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("mainView", _mainView)));
			AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|[mainView]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("mainView", _mainView)));
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Function will be called for IOS version less than 8
		/// </summary>
		/// <param name="orientation">Orientation.</param>
		private void AdjustPreviewLayer(Orientation orientation)
		{
			CGRect previewLayerFrame = _previewLayer.Frame;

			switch (orientation)
			{
				case Orientation.Portrait:
					previewLayerFrame.Height = UIScreen.MainScreen.Bounds.Height - _cameraButtonContainerWidth;
					previewLayerFrame.Width = UIScreen.MainScreen.Bounds.Width;
					break;

				case Orientation.LandscapeLeft:
				case Orientation.LandscapeRight:
					if (_systemVersion >= 8)
					{
						previewLayerFrame.Width = UIScreen.MainScreen.Bounds.Width - _cameraButtonContainerWidth;
						previewLayerFrame.Height = UIScreen.MainScreen.Bounds.Height;
					}
					else
					{
						previewLayerFrame.Width = UIScreen.MainScreen.Bounds.Height - _cameraButtonContainerWidth;
						previewLayerFrame.Height = UIScreen.MainScreen.Bounds.Width;
					}
					break;
			}

			try
			{
				_previewLayer.Frame = previewLayerFrame;
			}
			catch (Exception error)
			{
				_log.WriteLineTime(_tag + "\n" +
					"AdjustPreviewLayer() Failed to adjust frame \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);				
			}
		}

		/// <summary>
		/// Sets the start orientation.
		/// </summary>
		private void SetStartOrientation()
		{
			Orientation sOrientation = Orientation.None;

			switch (UIApplication.SharedApplication.StatusBarOrientation)
			{
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					sOrientation = Orientation.Portrait;
					break;
				case UIInterfaceOrientation.LandscapeLeft:
					sOrientation = Orientation.LandscapeLeft;
					break;
				case UIInterfaceOrientation.LandscapeRight:
					sOrientation = Orientation.LandscapeRight;
					break;
			}

			HandleOrientationChange(sOrientation);
		}

		/// <summary>
		/// Sets the busy.
		/// </summary>
		/// <param name="busy">If set to <c>true</c> busy.</param>
		private void SetBusy(bool busy)
		{
			_cameraBusy = busy;

			// set camera busy 
			Busy?.Invoke(this, _cameraBusy);
		}

		/// <summary>
		/// Captures the image with metadata.
		/// </summary>
		/// <returns>The image with metadata.</returns>
		/// <param name="captureStillImageOutput">Capture still image output.</param>
		/// <param name="connection">Connection.</param>
		private async Task CaptureImageWithMetadata(AVCaptureStillImageOutput captureStillImageOutput, AVCaptureConnection connection)
		{
			var sampleBuffer = await captureStillImageOutput.CaptureStillImageTaskAsync(connection);
			var imageData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
			var image = UIImage.LoadFromData(imageData);

			RotateImage(ref image);

			try
			{
				byte[] imgData = image.AsJPEG().ToArray();

				if (Photo != null)
				{
					Photo(this, imgData);
				}
			}
			catch (Exception error)
			{
				_log.WriteLineTime(_tag + "\n" +
					"CaptureImageWithMetadata() Failed to take photo \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);				
			}
		}

		/// <summary>
		/// Rotates the image.
		/// </summary>
		/// <param name="image">Image.</param>
		private void RotateImage(ref UIImage image)
		{
			CGImage imgRef = image.CGImage;
			CGAffineTransform transform = CGAffineTransform.MakeIdentity();

			var imgHeight = imgRef.Height * _imgScale;
			var imgWidth = imgRef.Width * _imgScale;

			CGRect bounds = new CGRect(0, 0, imgWidth, imgHeight);
			CGSize imageSize = new CGSize(imgWidth, imgHeight);
			UIImageOrientation orient = image.Orientation;

			switch (orient)
			{
				case UIImageOrientation.Up:
					transform = CGAffineTransform.MakeIdentity();
					break;
				case UIImageOrientation.Down:
					transform = CGAffineTransform.MakeTranslation(imageSize.Width, imageSize.Height);
					transform = CGAffineTransform.Rotate(transform, (float)Math.PI);
					break;
				case UIImageOrientation.Right:
					bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
					transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0);
					transform = CGAffineTransform.Rotate(transform, (float)Math.PI / 2.0f);
					break;
				default:
					throw new Exception("Invalid image orientation");
			}

			UIGraphics.BeginImageContext(bounds.Size);
			CGContext context = UIGraphics.GetCurrentContext();

			if (orient == UIImageOrientation.Right)
			{
				context.ScaleCTM(-1, 1);
				context.TranslateCTM(-imgHeight, 0);
			}
			else
			{
				context.ScaleCTM(1, -1);
				context.TranslateCTM(0, -imgHeight);
			}

			context.ConcatCTM(transform);

			context.DrawImage(new CGRect(0, 0, imgWidth, imgHeight), imgRef);
			image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw(CGRect rect)
		{
			_previewLayer.Frame = rect;

			base.Draw(rect);
		}

		/// <summary>
		/// Takes the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		public async Task TakePhoto()
		{
			if (!_cameraBusy) 
			{
				SetBusy(true);

				try 
				{
					// set output orientation
					_output.Connections [0].VideoOrientation = _previewLayer.Orientation;

					var connection = _output.Connections[0];

					await CaptureImageWithMetadata(_output, connection);

					SetBusy(false);
				}
				catch (Exception error)
				{
					_log.WriteLineTime(_tag + "\n" +
						"TakePhoto() Error with camera output capture \n " +
						"ErrorMessage: \n" +
						error.Message + "\n" +
						"Stacktrace: \n " +
						error.StackTrace);
						
					IoC.Resolve<ILogger>().WriteLineTime  ("CameraIOS: Error with camera output capture - " + error);
				}
			}
		}

		/// <summary>
		/// Switchs the flash.
		/// </summary>
		/// <param name="flashOn">If set to <c>true</c> flash on.</param>
		public void SwitchFlash(bool flashOn)
		{
			NSError err;

			if (_cameraAvailable && _device != null) 
			{
				try 
				{
					_device.LockForConfiguration(out err);
					_device.TorchMode = flashOn ? AVCaptureTorchMode.On : AVCaptureTorchMode.Off;
					_device.UnlockForConfiguration();
				} 
				catch (Exception error) 
				{
					_log.WriteLineTime(_tag + "\n" +
						"SwitchFlash() Failed to switch flash on/off \n " +
						"ErrorMessage: \n" +
						error.Message + "\n" +
						"Stacktrace: \n " +
						error.StackTrace);					
				}
			}
		}

		/// <summary>
		/// Sets the bounds.
		/// </summary>
		/// <returns>The bounds.</returns>
		public void SetBounds(nint width, nint height)
		{
			_height = height;
			_width = width;
		}

		/// <summary>
		/// Changes the focus point.
		/// </summary>
		/// <param name="fPoint">F point.</param>
		public void ChangeFocusPoint(Point fPoint)
		{
			NSError err;

			if (_cameraAvailable && _device != null) 
			{
				try 
				{
					_device.LockForConfiguration(out err);

					var focus_x = fPoint.X / Bounds.Width;
					var focus_y = fPoint.Y / Bounds.Height;

					// set focus point
					if (_device.FocusPointOfInterestSupported)
						_device.FocusPointOfInterest = new CGPoint(focus_x, focus_y);
					if (_device.ExposurePointOfInterestSupported)
						_device.ExposurePointOfInterest = new CGPoint(focus_x, focus_y);

					_device.UnlockForConfiguration();
				} 
				catch (Exception error) 
				{
					_log.WriteLineTime(_tag + "\n" +
						"SwitchFlash() Failed to adjust focus \n " +
						"ErrorMessage: \n" +
						error.Message + "\n" +
						"Stacktrace: \n " +
						error.StackTrace);					
				}
			}
		}

		/// <summary>
		/// Retrieves the camera device.
		/// </summary>
		/// <returns><c>true</c>, if camera device was retrieved, <c>false</c> otherwise.</returns>
		public bool RetrieveCameraDevice()
		{
			_device = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);

			if (_device == null) 
			{
				_log.WriteLineTime(_tag + "\n" + "RetrieveCameraDevice() No device detected \n ");
			
				return false;
			}

			return true;
		}

		/// <summary>
		/// Initializes the camera.
		/// </summary>
		/// <returns>The camera.</returns>
		public void InitializeCamera()
		{
			try 
			{
				NSError error;
				NSError err;

				_device.LockForConfiguration(out err);
				_device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				_device.UnlockForConfiguration();

				_input = new AVCaptureDeviceInput(_device, out error);
				_captureSession.AddInput(_input);

				_output = new AVCaptureStillImageOutput();

				var dict = new NSMutableDictionary();
				dict[AVVideo.CodecKey] = new NSNumber((int) AVVideoCodec.JPEG);
				_captureSession.AddOutput (_output);

				InvokeOnMainThread(delegate 
					{
						// capture connection used for rotating camera
						_captureConnection = _previewLayer.Connection;
						SetStartOrientation();
						// set orientation before loading camera
						_captureSession.StartRunning ();
					});
			}
			catch (Exception error) 
			{
				_log.WriteLineTime(_tag + "\n" +
					"InitializeCamera() Camera failed to initialise \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);	
			}

			Available?.Invoke(this, _cameraAvailable);

			_log.WriteLineTime(_tag + "\n" + "RetrieveCameraDevice() Camera initalised \n ");
		}

		/// <summary>
		/// Sets the widths.
		/// </summary>
		/// <param name="cameraButtonContainerWidth">Camera button container width.</param>
		public void SetWidths(float cameraButtonContainerWidth)
		{
			_cameraButtonContainerWidth = cameraButtonContainerWidth;
		}

		/// <summary>
		/// Handles the orientation change.
		/// </summary>
		/// <param name="orientation">Orientation.</param>
		public void HandleOrientationChange(Orientation orientation)
		{
			if (_captureConnection != null)
			{
				switch (orientation)
				{
					case Orientation.Portrait:
						_captureConnection.VideoOrientation = AVCaptureVideoOrientation.Portrait;
						break;
					case Orientation.LandscapeLeft:
						_captureConnection.VideoOrientation = AVCaptureVideoOrientation.LandscapeLeft;
						break;
					case Orientation.LandscapeRight:
						_captureConnection.VideoOrientation = AVCaptureVideoOrientation.LandscapeRight;
						break;
				}
			}

			AdjustPreviewLayer(orientation);
		}

		/// <summary>
		/// Stops the and dispose.
		/// </summary>
		public void StopAndDispose()
		{
			if (_device != null)
			{
				// if flash is on turn off
				if (_device.TorchMode == AVCaptureTorchMode.On)
				{
					SwitchFlash(false);
				}
			}

			_captureSession.StopRunning();
			// dispose output elements
			_input.Dispose();
			_output.Dispose();
		}

		#endregion
	}
}