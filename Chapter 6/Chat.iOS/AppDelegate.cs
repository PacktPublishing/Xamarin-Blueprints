// --------------------------------------------------------------------------------------------------
//  <copyright file="AppDelegate.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.iOS
{
	using Foundation;
	using UIKit;

	using Chat.iOS.Views;
	using Chat.iOS.Services;

	using Chat.Common.Presenter;

	/// <summary>
	/// App delegate.
	/// </summary>
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		/// <summary>
		/// The window.
		/// </summary>
		UIWindow _window;

		/// <summary>
		/// The navigation controller.
		/// </summary>
		UINavigationController _navigationController;

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <returns>The launching.</returns>
		/// <param name="app">App.</param>
		/// <param name="options">Options.</param>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			_navigationController = new UINavigationController();

			var state = new ApplicationState();

			var presenter = new LoginPresenter(state, new NavigationService(_navigationController));
			var controller = new LoginViewController(presenter);

			_navigationController.PushViewController(controller, false);
			_window.RootViewController = _navigationController;

			// make the window visible
			_window.MakeKeyAndVisible();

			return true;
		}
	}
}