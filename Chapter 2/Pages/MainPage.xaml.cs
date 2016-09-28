// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.Pages
{
	using Xamarin.Forms;

	using SpeechTalk.ViewModels;

	/// <summary>
	/// Main page.
	/// </summary>
	public partial class MainPage : ContentPage
	{
		#region Private Properties

		/// <summary>
		/// The view model.
		/// </summary>
		private MainPageViewModel _viewModel;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SpeechTalk.Pages.MainPage"/> class.
		/// </summary>
		public MainPage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SpeechTalk.Pages.MainPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public MainPage (MainPageViewModel model)
		{
			BindingContext = model;
			InitializeComponent ();
		}

		#endregion
	}
}