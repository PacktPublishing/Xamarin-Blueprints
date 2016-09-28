// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockItemDetailsPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.XamForms.Pages
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	using Stocklist.XamForms.UI;

	using Stocklist.Portable.ViewModels;
	using Stocklist.Portable.Ioc;

	/// <summary>
	/// Stock item details page.
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockItemDetailsPage : ContentPage, INavigableXamarinFormsPage
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.XamForms.Pages.StockItemDetailsPage"/> class.
		/// </summary>
		public StockItemDetailsPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.XamForms.Pages.StockItemDetailsPage"/> class.
		/// </summary>
		/// <param name="modelFactory">Model factory.</param>
		public StockItemDetailsPage(Func<StockItemDetailsPageViewModel> modelFactory) : this()
		{
			BindingContext = modelFactory();
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

		#endregion
	}
}