// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.iOS
{
	using Foundation;
	using UIKit;

	using SpeechTalk.iOS.Modules;

	using SpeechTalk.Ioc;
	using SpeechTalk.Modules;

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

		#region Private Methods

		/// <summary>
		/// Inits the IoC container and modules.
		/// </summary>
		/// <returns>The io c.</returns>
		private void InitIoC()
		{
			IoC.CreateContainer ();
			IoC.RegisterModule (new IOSModule());
			IoC.RegisterModule (new PCLModule());
			IoC.StartContainer ();
		}

		#endregion
	}
}