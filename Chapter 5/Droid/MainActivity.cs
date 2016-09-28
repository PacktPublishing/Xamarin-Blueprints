// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;

	using Stocklist.Droid.Modules;

	using Stocklist.Shared.Modules;

	using Stocklist.XamForms.Modules;

	using Stocklist.Portable.Modules;
	using Stocklist.Portable.Ioc;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity(Label = "Stocklist.Portable.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		#region Protected Methods

		/// <summary>
		/// OnCreate override for MainActivity
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			InitIoC();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new XamForms.App());
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initialise the IoC container
		/// </summary>
		private void InitIoC()
		{
			IoC.CreateContainer();
			IoC.RegisterModule(new DroidModule());
			IoC.RegisterModule(new SharedModule(false));
			IoC.RegisterModule(new XamFormsModule());
			IoC.RegisterModule(new PortableModule());
			IoC.StartContainer();
		}

		#endregion
	}
}