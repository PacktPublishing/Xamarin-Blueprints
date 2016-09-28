// --------------------------------------------------------------------------------------------------
//  <copyright file="AppDelegate.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS
{
	using Foundation;
	using UIKit;

	using MvvmCross.iOS.Platform;

	using MvvmCross.Core.ViewModels;

	using MvvmCross.Platform;

	using AudioPlayer.iOS.Sound;

	using AudioPlayer.Portable.Sound;
	using AudioPlayer.Portable;

	/// <summary>
	/// App delegate.
	/// </summary>
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		#region Private Properties

		/// <summary>
		/// The window.
		/// </summary>
		private UIWindow _window;

		#endregion

		#region Public Methods

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
		/// <param name="application">Application.</param>
		/// <param name="launchOptions">Launch options.</param>
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new IosSetup(this, _window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();
			SetupIoC();

			_window.MakeKeyAndVisible();

			return true;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Setups the registrations.
		/// </summary>
		/// <returns>The registrations.</returns>
		private void SetupIoC()
		{
			Mvx.RegisterType<ISoundHandler, SoundHandler>();
			PortableMvxIoCRegistrations.InitIoC();
		}

		#endregion
	}
}