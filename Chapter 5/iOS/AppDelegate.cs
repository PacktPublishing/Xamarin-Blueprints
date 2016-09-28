// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.iOS
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Foundation;
	using UIKit;

	using Stocklist.iOS.Modules;

	using Stocklist.Shared.Modules;

	using Stocklist.XamForms.Modules;
	using Stocklist.XamForms;

	using Stocklist.Portable.Ioc;
	using Stocklist.Portable.Modules;

	/// <summary>
	/// App delegate.
	/// </summary>
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		#region Public Methods

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <returns>The launching.</returns>
		/// <param name="app">App.</param>
		/// <param name="options">Options.</param>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			InitIoC();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Inits the IoC container
		/// </summary>
		/// <returns>The io c.</returns>
		private void InitIoC()
		{
			IoC.CreateContainer();
			IoC.RegisterModule(new IOSModule());
			IoC.RegisterModule(new SharedModule(false));
			IoC.RegisterModule(new XamFormsModule());
			IoC.RegisterModule(new PortableModule());
			IoC.StartContainer();
		}

		#endregion
	}
}