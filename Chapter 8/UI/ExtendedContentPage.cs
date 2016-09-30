// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedContentPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.UI
{
	using System;

	using Camera.Controls;

	using Camera.Portable.ViewModels;
	using Camera.Portable.Enums;
	using Camera.Portable;

	/// <summary>
	/// Extended content page.
	/// </summary>
	public class ExtendedContentPage : OrientationPage
	{
		#region Events

		/// <summary>
		/// Occurs when touch handler.
		/// </summary>
		public event EventHandler AlertFinished;

		/// <summary>
		/// Occurs when orientation change.
		/// </summary>
		public event EventHandler<Orientation> OrientationChange;

		#endregion

		#region Private Properties

		/// <summary>
		/// The model.
		/// </summary>
		private ViewModelBase _model;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.UI.ExtendedContentPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public ExtendedContentPage(ViewModelBase model)
		{
			_model = model;
			_model.Alert -= HandleAlert;
			_model.Alert += HandleAlert;

			Appearing += HandleAppearing;
			Disappearing += HandleDisappearing;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the disappearing.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleDisappearing(object sender, EventArgs e)
		{
			OrientationPage.OrientationHandler -= OrientationPage_OrientationHandler;
		}

		/// <summary>
		/// Trons the page appearing.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleAppearing(object sender, EventArgs e)
		{
			OrientationPage.OrientationHandler -= OrientationPage_OrientationHandler;
		}

		/// <summary>
		/// Orientations the page orientation handler.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void OrientationPage_OrientationHandler(object sender, Orientation e)
		{
			PageOrientation = e;

			if (OrientationChange != null)
			{
				OrientationChange(this, e);
			}
		}

		/// <summary>
		/// Handles the alert.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private async void HandleAlert(object sender, AlertArgs args)
		{
			await DisplayAlert("Camera", args.Message, "OK");

			args.Tcs.SetResult(true);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Override this method to execute an action when the BindingContext changes.
		/// </summary>
		/// <remarks></remarks>
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				_model = (BindingContext as ViewModelBase);
			}
		}

		#endregion
	}
}