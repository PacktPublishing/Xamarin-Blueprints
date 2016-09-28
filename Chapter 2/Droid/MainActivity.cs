// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;

	using SpeechTalk.Droid.Modules;

	using SpeechTalk.Ioc;
	using SpeechTalk.Modules;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "SpeechTalk.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		/// <summary>
		/// Called when activity is created.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			InitIoC ();

			LoadApplication (new App ());
		}

		/// <summary>
		/// Inits the IoC container and modules.
		/// </summary>
		/// <returns>The io c.</returns>
		private void InitIoC()
		{
			IoC.CreateContainer ();
			IoC.RegisterModule (new DroidModule());
			IoC.RegisterModule (new PCLModule());
			IoC.StartContainer ();
		}
	}
}