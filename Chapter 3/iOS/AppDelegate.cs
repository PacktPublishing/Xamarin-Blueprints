// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.iOS
{
	using Foundation;
	using UIKit;

	using Locator.iOS.Modules;

	using Locator.Shared.Modules;

	using Locator.Modules;

	using Locator.Portable.Ioc;
	using Locator.Portable.Modules;

	/// <summary>
	/// App delegate.
	/// </summary>
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		#region Public Methods

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <returns>The launching.</returns>
		/// <param name="app">App.</param>
		/// <param name="options">Options.</param>
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.FormsMaps.Init();
			global::Xamarin.Forms.Forms.Init ();

			InitIoC ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Inits the IoC container and modules
		/// </summary>
		/// <returns>The io c.</returns>
		private void InitIoC()
		{
			IoC.CreateContainer ();
			IoC.RegisterModule (new IOSModule());
			IoC.RegisterModule (new SharedModule(false));
			IoC.RegisterModule (new XamFormsModule());
			IoC.RegisterModule (new PortableModule());
			IoC.StartContainer ();
		}

		#endregion
	}
}