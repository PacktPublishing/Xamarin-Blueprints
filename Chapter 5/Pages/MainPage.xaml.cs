// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.XamForms.Pages
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Input;

	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	using Stocklist.XamForms.UI;

	using Stocklist.Portable.ViewModels;

	/// <summary>
	/// Main page.
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage, INavigableXamarinFormsPage
	{
		#region Private Properties

		/// <summary>
		/// The original template.
		/// </summary>
		private bool _originalTemplate = true;

		/// <summary>
		/// The black template.
		/// </summary>
		private ControlTemplate _blackTemplate;

		/// <summary>
		/// The white template.
		/// </summary>
		private ControlTemplate _whiteTemplate;

		#endregion

		#region Public Static Properties

		/// <summary>
		/// The stocklist command property.
		/// </summary>
		public static readonly BindableProperty StocklistCommandProperty = BindableProperty.Create("StocklistCommand", typeof(ICommand), typeof(MainPage), null);

		/// <summary>
		/// The exit command property.
		/// </summary>
		public static readonly BindableProperty ExitCommandProperty = BindableProperty.Create("ExitCommand", typeof(ICommand), typeof(MainPage), null);

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the stocklist command.
		/// </summary>
		/// <value>The stocklist command.</value>
		public ICommand StocklistCommand
		{
			get { return (ICommand)GetValue(StocklistCommandProperty); }
		}

		/// <summary>
		/// Gets the exit command.
		/// </summary>
		/// <value>The exit command.</value>
		public ICommand ExitCommand
		{
			get { return (ICommand)GetValue(ExitCommandProperty); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.XamForms.Pages.MainPage"/> class.
		/// </summary>
		public MainPage()
		{
			InitializeComponent();

			_blackTemplate = (ControlTemplate)Application.Current.Resources["MainBlackTemplate"];
			_whiteTemplate = (ControlTemplate)Application.Current.Resources["MainWhiteTemplate"];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.XamForms.Pages.MainPage"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public MainPage(MainPageViewModel model) : this()
		{
			BindingContext = model;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Called when navigated to
		/// </summary>
		/// <returns>The navigated to.</returns>
		/// <param name="navigationParameters">Navigation parameters.</param>
		public void OnNavigatedTo(IDictionary<string, object> navigationParameters)
		{
			this.Show(navigationParameters);
		}

		/// <summary>
		/// Changes the theme clicked.
		/// </summary>
		/// <returns>The theme clicked.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void ChangeThemeClicked(object sender, EventArgs e)
		{
			_originalTemplate = !_originalTemplate;
			ControlTemplate = _originalTemplate ? _blackTemplate : _whiteTemplate;
			BackgroundColor = _originalTemplate ? Color.Black : Color.White;
		}

		#endregion
	}
}