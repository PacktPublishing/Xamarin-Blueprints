// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;

	using Locator.Droid.Modules;

	using Locator.Shared.Modules;

	using Locator.Modules;

	using Locator.Portable.Modules;
	using Locator.Portable.Ioc;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "Locator.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		#region Protected Methods

		/// <summary>
		/// Called when the activity is created.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			global::Xamarin.FormsMaps.Init(this, bundle);

			InitIoC();

			LoadApplication (new App ());
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
			IoC.RegisterModule (new DroidModule());
			IoC.RegisterModule (new SharedModule(false));
			IoC.RegisterModule (new XamFormsModule());
			IoC.RegisterModule (new PortableModule());
			IoC.StartContainer ();
		}

		#endregion
	}
}