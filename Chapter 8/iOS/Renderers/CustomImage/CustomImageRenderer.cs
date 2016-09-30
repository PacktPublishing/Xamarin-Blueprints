// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomImageRenderer.cs" company="Flush Arcade">
//   Copyright (c) 2015 Flush Arcade All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.ExportRenderer(typeof(Camera.Controls.CustomImage), 
                                        typeof(Camera.iOS.Renderers.CustomImage.CustomImageRenderer))]

namespace Camera.iOS.Renderers.CustomImage
{
	using System;
	using System.Threading.Tasks;
	using System.IO;
	using System.Diagnostics;

	using Foundation;

	using Xamarin.Forms.Platform.iOS;
	using Xamarin.Forms;

	using UIKit;

	using Camera.Controls;
	using Camera.iOS.Extensions;
	using Camera.iOS.Helpers;
	using Camera.Portable.Logging;
	using Camera.Portable.Ioc;

	/// <summary>
	/// Custom image renderer.
	/// </summary>
	[Preserve(AllMembers = true)]
	public class CustomImageRenderer : ViewRenderer<CustomImage, UIView>
	{
		#region Private Properties

		/// <summary>
		/// The tag.
		/// </summary>
		private readonly string _tag;

		/// <summary>
		/// The log.
		/// </summary>
		private ILogger _log;

		/// <summary>
		/// The image view.
		/// </summary>
		private UIImageView _imageView;

		/// <summary>
		/// The system version.
		/// </summary>
		private int _systemVersion = Convert.ToInt16 (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0]);

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LogIt.Droid.Renderers.CustomImageRenderer"/> class.
		/// </summary>
		public CustomImageRenderer()
		{
			_log = IoC.Resolve<ILogger>();
			_tag = string.Format("{0} ", GetType());
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Raises the element changed event.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnElementChanged (ElementChangedEventArgs<CustomImage> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
			{
				_imageView = new UIImageView();

				// Instantiate the native control
				SetNativeControl(_imageView);
			}

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
				e.OldElement.CustomPropertyChanged -= HandleCustomPropertyChanged;
			}

			if (e.NewElement != null)
			{
				LoadImage();

				e.NewElement.CustomPropertyChanged += HandleCustomPropertyChanged;
				// Configure the control and subscribe to event handlers
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the custom property changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="propertyName">Property name.</param>
		private void HandleCustomPropertyChanged (object sender, string propertyName)
		{
			switch (propertyName)
			{
				case "TintColorString":
				case "TintOn":
					UpdateControlColor();
					break;
				case "Path":
					InvokeOnMainThread(() => LoadImage());
					break;
			}
		}

		/// <summary>
		/// Loads the image.
		/// </summary>
		private void LoadImage()
		{
			try 
			{
				if (Element != null)
				{
					if (!string.IsNullOrEmpty(Element.Path))
					{
						_imageView.Image = ReadBitmapImageFromStorage (Element.Path);

						if (_imageView.Image != null)
						{
							if (_systemVersion >= 7 && Element.TintOn)
							{
								_imageView.Image = _imageView.Image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysTemplate);
							}

							UpdateControlColor();

							_imageView.ContentMode = SetAspect();
						}
					}
				}
			}
			catch (Exception error)
			{
				_log.WriteLineTime(_tag + "\n" +
					"LoadAsync() Failed to load view model.  \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);
			}
		}

		/// <summary>
		/// Sets the aspect.
		/// </summary>
		/// <returns>The aspect.</returns>
		private UIViewContentMode SetAspect()
		{
			if (Element != null)
			{
				switch (Element.Aspect) 
				{
					case Aspect.AspectFill:
						return UIViewContentMode.ScaleAspectFill;
					case Aspect.AspectFit:
						return UIViewContentMode.ScaleAspectFit;
					case Aspect.Fill:
						return UIViewContentMode.ScaleToFill;
					default:
						return UIViewContentMode.ScaleAspectFit;
				}
			}

			return UIViewContentMode.ScaleAspectFit;
		}

		/// <summary>
		/// Reads the bitmap image from storage.
		/// </summary>
		/// <returns>The bitmap image from storage.</returns>
		/// <param name="fn">Fn.</param>
		private UIImage ReadBitmapImageFromStorage(string fn)
		{
			var docsPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string filePath = Path.Combine(Environment.CurrentDirectory, fn);

			try 
			{
				using (Stream stream = File.OpenRead(filePath))
				{
					NSData data = NSData.FromStream (stream);
					return UIImage.LoadFromData (data);
				}
			}
			catch (Exception error)
			{
				_log.WriteLineTime(_tag + "\n" +
					"LoadAsync() Failed to load view model.  \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);
			}

			return UIImage.FromFile (Path.Combine (Environment.CurrentDirectory, "loading.png"));
		}

		/// <summary>
		/// Updates the color of the control.
		/// </summary>
		private void UpdateControlColor()
		{
			if (Element.TintOn && !string.IsNullOrEmpty(Element.TintColorString)) 
			{
				var color = UIColor.Clear.FromHex (Element.TintColorString, 1.0f);

				_imageView.Image = UIImageEffects.GetColoredImage(_imageView.Image, color);
			}
		}

		#endregion
	}
}