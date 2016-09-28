// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedContentPage.cs" company="Medibio">
//   Copyright (c) 2016 Mediobio All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.UI
{
	using System;

	using Xamarin.Forms;

	using FileStorage.Portable.ViewModels;

	/// <summary>
	/// Extended content page.
	/// </summary>
	public class ExtendedContentPage : ContentPage
	{
		#region Private Properties

		/// <summary>
		/// The model.
		/// </summary>
		private ViewModelBase _model;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MyCareManager.XamForms.Controls.Page.ExtendedContentPage"/> class.
		/// </summary>
		public ExtendedContentPage(ViewModelBase model)
		{
			_model = model;
			_model.Alert -= HandleAlert;
			_model.Alert += HandleAlert;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the alert.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private async void HandleAlert(object sender, string message)
		{
			await DisplayAlert("FileStorage", message, "OK");
		}

		#endregion
	}
}