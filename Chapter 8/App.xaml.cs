// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace Camera
{
	using Xamarin.Forms;

	using Camera.Portable.Ioc;

	/// <summary>
	/// The App.
	/// </summary>
	public partial class App : Application
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.App"/> class.
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

		#region Protected Methods

		/// <summary>
		/// Override the starting function
		/// </summary>
		/// <returns>The start.</returns>
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		/// <summary>
		/// Override the OnSleep function
		/// </summary>
		/// <returns>The sleep.</returns>
		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		/// <summary>
		/// Overrides the OnResume function
		/// </summary>
		/// <returns>The resume.</returns>
		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		#endregion
	}
}