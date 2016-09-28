// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.XamForms.UI
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Xamarin.Forms;

	using Stocklist.XamForms.Pages;

	using Stocklist.Portable.UI;
	using Stocklist.Portable.Enums;
	using Stocklist.Portable.Ioc;

	/// <summary>
	/// Navigation service.
	/// </summary>
	public class NavigationService : INavigationService
	{
		#region INavigationService implementation

		/// <summary>
		/// Navigate the specified pageName and navigationParameters.
		/// </summary>
		/// <param name="pageName">Page name.</param>
		/// <param name="navigationParameters">Navigation parameters.</param>
		public async Task Navigate (PageNames pageName, IDictionary<string, object> navigationParameters)
		{
			var page = GetPage (pageName);

			if (page != null) 
			{
				var navigablePage = page as INavigableXamarinFormsPage;

				if (navigablePage != null) 
				{
					await IoC.Resolve<NavigationPage> ().PushAsync (page);
					navigablePage.OnNavigatedTo (navigationParameters);
				}
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="page">Page.</param>
		private Page GetPage(PageNames page)
		{
			switch(page)
			{
				case PageNames.MainPage:
					return IoC.Resolve<MainPage> ();
				case PageNames.StocklistPage:
					return IoC.Resolve<Func<StocklistPage>>()();
				case PageNames.StockItemDetailsPage:
					return IoC.Resolve<Func<StockItemDetailsPage>>()();
				default:
					return null;
			}
		}

		#endregion
	}
}