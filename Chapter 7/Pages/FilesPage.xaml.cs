// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilesPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Pages
{
	using System;
	using System.Collections.Generic;

	using FileStorage.UI;
	using FileStorage.Portable.ViewModels;

	/// <summary>
	/// Map page.
	/// </summary>
	public partial class FilesPage : ExtendedContentPage, INavigableXamarinFormsPage
	{
		#region Private Properties

		/// <summary>
		/// The view model.
		/// </summary>
		private FilesPageViewModel _viewModel;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Pages.MapPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public FilesPage (FilesPageViewModel model) : base(model)
		{
			_viewModel = model;
			BindingContext = model;

			InitializeComponent ();

			Appearing += HandleAppearing;
			Disappearing += HandleDisappearing;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the disappearing.
		/// </summary>
		/// <returns>The disappearing.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleDisappearing (object sender, EventArgs e)
		{
			_viewModel.OnDisppear ();
		}

		/// <summary>
		/// Handles the appearing.
		/// </summary>
		/// <returns>The appearing.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleAppearing (object sender, EventArgs e)
		{
			_viewModel.OnAppear ();
		}

		#endregion

		#region INavigableXamarinFormsPage interface

		/// <summary>
		/// Called when page is navigated to.
		/// </summary>
		/// <returns>The navigated to.</returns>
		/// <param name="navigationParameters">Navigation parameters.</param>
		public void OnNavigatedTo(IDictionary<string, object> navigationParameters)
		{
			this.Show(navigationParameters);
		}

		#endregion
	}
}