// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.iOS
{
	using Foundation;
	using UIKit;

	using Camera.iOS.Modules;

	using Camera.Modules;
	using Camera.Controls;

	using Camera.Portable.Ioc;
	using Camera.Portable.Modules;
	using Camera.Portable.Enums;

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
			global::Xamarin.Forms.Forms.Init ();

			InitIoC ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		#endregion


		/// <summary>
		/// Dids the change status bar orientation.
		/// </summary>
		/// <param name="application">Application.</param>
		/// <param name="oldStatusBarOrientation">Old status bar orientation.</param>
		public override void DidChangeStatusBarOrientation(UIApplication application, UIInterfaceOrientation oldStatusBarOrientation)
		{
			// change listview opacity based upon orientation
			switch (UIApplication.SharedApplication.StatusBarOrientation)
			{
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					OrientationPage.NotifyOrientationChange(Orientation.Portrait);
					break;
				case UIInterfaceOrientation.LandscapeLeft:
					OrientationPage.NotifyOrientationChange(Orientation.LandscapeLeft);
					break;
				case UIInterfaceOrientation.LandscapeRight:
					OrientationPage.NotifyOrientationChange(Orientation.LandscapeRight);
					break;
			}
		}

		#region Private Methods

		/// <summary>
		/// Inits the IoC container and modules
		/// </summary>
		/// <returns>The io c.</returns>
		private void InitIoC()
		{
			IoC.CreateContainer ();
			IoC.RegisterModule (new IOSModule());
			//IoC.RegisterModule (new SharedModule(false));
			IoC.RegisterModule (new XamFormsModule());
			IoC.RegisterModule (new PortableModule());
			IoC.StartContainer ();
		}

		#endregion
	}
}