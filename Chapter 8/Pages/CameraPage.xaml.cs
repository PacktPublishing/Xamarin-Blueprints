// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Pages
{
	using System;
	using System.Collections.Generic;

	using Xamarin.Forms;

	using Camera.UI;
	using Camera.Portable.ViewModels;
	using Camera.Portable.Enums;
	using Camera.Portable.Logging;
	using Camera.Controls;
	using Camera.Converters;

	/// <summary>
	/// Camera page.
	/// </summary>
	public partial class CameraPage : ExtendedContentPage, INavigableXamarinFormsPage
	{
		#region Private Properties

		/// <summary>
		/// The camera button container width
		/// </summary>
		private float CAMERA_BUTTON_CONTAINER_WIDTH = 70f;

		/// <summary>
		/// The model.
		/// </summary>
		private CameraPageViewModel _model;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.Pages.MainPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public CameraPage(CameraPageViewModel model) : base(model)
		{
			BindingContext = model;
			_model = model;

			InitializeComponent();

			Appearing += HandleAppearing;
			Disappearing += HandleDisappearing;

			// handler for camera view
			CameraView.Photo += HandlePictureTaken;
			CameraView.AvailabilityChange += HandleCameraAvailability;
			CameraView.Loading += HandleLoading;
			CameraView.Busy += HandleBusy;

			// handler for focusing
			FocusView.TouchFocus += HandleFocusChange;
		}

		#endregion

		#region INavigableXamarinFormsPage implementation

		/// <summary>
		/// Raises the navigated to event.
		/// </summary>
		/// <param name="navigationParameters">Navigation parameters.</param>
		public void OnNavigatedTo(IDictionary<string, object> navigationParameters)
		{
#if WINDOWS_PHONE

			OrientationPage.TouchHandler += HandleTouchHandler;

#endif

			_model.CameraLoading = false;

			LoadingView.SetBinding(VisualElement.IsVisibleProperty, new Binding("CameraLoading"));

			_model.CanCapture = CameraView.CameraAvailable;

			switch (PageOrientation)
			{
				case Orientation.Portrait:
					// set starting focus points for each orientation
					FocusView.SetFocusPoints(new Point(Width / 2, Height / 2), 
					                         new Point(Height / 2, Width / 2));
					break;
				case Orientation.LandscapeLeft:
				case Orientation.LandscapeRight:
					// set starting focus points for each orientation
					FocusView.SetFocusPoints(new Point(Height / 2, Width / 2), 
					                         new Point(Width / 2, Height / 2));
					break;
			}

			CameraView.NotifyOpenCamera(true);

			// camera must store these widths for IOS on orientation changes for camera preview layer resizing
#if __IOS__
				CameraView.NotifyWidths (CAMERA_BUTTON_CONTAINER_WIDTH);
#endif

			this.Show(navigationParameters);
		}

		#endregion

#if WINDOWS_PHONE
		protected void HandleTouchHandler(object sender, Point e)
		{
			if (model != null)
			if (model.ComboShowing)
			return;

			if (TetrixGrid.TetrixGridLayout.Orientation == Orientation.Portrait)
			if ((e.Y + FocusView.TargetHeight) >= (CameraButtonContainerPortrait.Y))
			return;

			if ((TetrixGrid.TetrixGridLayout.Orientation != Orientation.Portrait) || model.FilmOn)
			if ((e.X + FocusView.TargetWidth) >= (TetrixGrid.X))
			return;

			FocusView.NotifyFocus (e);
		}
#endif

		#region Private Methods


		/// <summary>
		/// Handles the disappearing.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleDisappearing(object sender, EventArgs e)
		{
			OrientationHandler -= HandleOrientationChange;

#if WINDOWS_PHONE

			OrientationPage.TouchHandler -= HandleTouchHandler;

#endif

			_model.OnDisappear();
		}

		/// <summary>
		/// Handles the appearing.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleAppearing(object sender, EventArgs e)
		{
			// handler for orientation changes 
			OrientationHandler += HandleOrientationChange;

			_model.OnAppear();
		}

		/// <summary>
		/// Handles the busy.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">If set to <c>true</c> e.</param>
		private void HandleBusy(object sender, bool e)
		{
			_model.CameraLoading = e;
		}

		/// <summary>
		/// Handles the loading.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">If set to <c>true</c> e.</param>
		private void HandleLoading(object sender, bool e)
		{
			_model.CameraLoading = e;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles the picture taken.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="data">Data.</param>
		public void HandlePictureTaken(object sender, byte[] data)
		{
			if (_model.CanCapture)
			{
				// create new photo item and add to collection
				_model.AddPhoto(data);
			}
		}

		/// <summary>
		/// Handles the orientation change.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="arg">Argument.</param>
		public void HandleOrientationChange(object sender, Orientation arg)
		{
			FocusView.Orientation = CameraView.Orientation = OrientationPage.PageOrientation = _model.PageOrientation = arg;

			// bindings will not work with column width properties so we have to perform this manually
			switch (PageOrientation)
			{
				case Orientation.LandscapeLeft:
				case Orientation.LandscapeRight:
					MainLayout.ColumnDefinitions[5].Width = new GridLength(CAMERA_BUTTON_CONTAINER_WIDTH, 
					                                                       GridUnitType.Absolute);
					break;
				case Orientation.Portrait:
					MainLayout.ColumnDefinitions[4].Width = new GridLength(CAMERA_BUTTON_CONTAINER_WIDTH, 
					                                                       GridUnitType.Absolute);
					break;
			}

			if (_model.CanCapture)
			{
				FocusView.Reset();
			}

			CameraView.NotifyOrientationChange(arg);
		}

		/// <summary>
		/// Handles the focus change.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="arg">Argument.</param>
		public void HandleFocusChange(object sender, Point arg)
		{
			if (_model.CanCapture)
			{
				CameraView.NotifyFocus(arg);
			}
		}

		/// <summary>
		/// Handles the camera availability.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="available">If set to <c>true</c> available.</param>
		public void HandleCameraAvailability(object sender, bool available)
		{
			_model.CanCapture = available;

			if (available)
			{
				_model.CameraLoading = false;

				// wait until camera is available before animating focus target, we have to invoke on UI thread as this is run asynchronously
				Device.BeginInvokeOnMainThread(() =>
				{
					// set starting list opacity based on orientation
					var orientation = (Height > Width) ? Orientation.Portrait : Orientation.LandscapeLeft;
					// set starting orientation
					HandleOrientationChange(null, orientation);

					// these bindings are created after page intitalizes
					PhotoEditLayout.SetBinding(VisualElement.IsVisibleProperty, new Binding("PhotoEditOn"));

					// camera button layouts
					CameraButtonContainerLandscape.SetBinding(VisualElement.OpacityProperty, new Binding("PageOrientation", converter: new OrientationToDoubleConverter(), converterParameter: "1, 1"));
					CameraButtonContainerLandscape.SetBinding(VisualElement.IsVisibleProperty, new Binding("PageOrientation", converter: new OrientationToBoolConverter(), converterParameter: "true, false"));
					CameraButtonContainerPortrait.SetBinding(VisualElement.OpacityProperty, new Binding("PageOrientation", converter: new OrientationToDoubleConverter(), converterParameter: "0, 1"));
					CameraButtonContainerPortrait.SetBinding(VisualElement.IsVisibleProperty, new Binding("PageOrientation", converter: new OrientationToBoolConverter(), converterParameter: "false, true"));

					FocusView.Reset();
				});
			}
		}

		/// <summary>
		/// Handles the shutter.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void HandleShutter(object sender, EventArgs args)
		{
			CameraView.NotifyShutter();
		}

		/// <summary>
		/// Handles the flash.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void HandleFlash(object sender, EventArgs args)
		{
			_model.IsFlashOn = !_model.IsFlashOn;
			CameraView.NotifyFlash(_model.IsFlashOn);
		}

		/// <summary>
		/// Handles the delete.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void HandleDelete(object sender, EventArgs args)
		{
			_model.ResetEditPhoto();
		}

		#endregion
	}
}