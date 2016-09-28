// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.UI
{
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Xamarin.Forms;

	using FileStorage.Pages;

	using FileStorage.Portable.UI;
	using FileStorage.Portable.Enums;
	using FileStorage.Portable.Ioc;

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

		/// <summary>
		/// Pop this instance.
		/// </summary>
		public async Task Pop()
		{
			await IoC.Resolve<NavigationPage>().PopAsync();
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
				case PageNames.FilesPage:
					return IoC.Resolve<FilesPage> ();
				case PageNames.EditFilePage:
					return IoC.Resolve<EditFilePage>();
				default:
					return null;
			}
		}

		#endregion
	}
}