// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace FileStorage
{
	using Xamarin.Forms;

	using FileStorage.Portable.Ioc;

	/// <summary>
	/// The App.
	/// </summary>
	public partial class App : Application
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.App"/> class.
		/// </summary>
		public App()
		{
			InitializeComponent();

			// The Application ResourceDictionary is available in Xamarin.Forms 1.3 and later
			if (Current.Resources == null)
			{
				Current.Resources = new ResourceDictionary();
			}

			MainPage = IoC.Resolve<NavigationPage>();
		}

		#endregion
	}
}