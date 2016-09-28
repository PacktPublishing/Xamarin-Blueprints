// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator
{
	using Xamarin.Forms;

	using Locator.Portable.Ioc;

	/// <summary>
	/// App.
	/// </summary>
	public class App : Application
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.App"/> class.
		/// </summary>
		public App ()
		{
			MainPage = IoC.Resolve<NavigationPage> ();
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called when app is started.
		/// </summary>
		/// <returns>The start.</returns>
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		/// <summary>
		/// Called when app is background.
		/// </summary>
		/// <returns>The sleep.</returns>
		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		/// <summary>
		/// Called when app is foreground.
		/// </summary>
		/// <returns>The resume.</returns>
		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		#endregion
	}
}