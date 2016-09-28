// --------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreenActivity.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid
{
	using Android.App;
	using Android.OS;

	using MvvmCross.Droid.Views;
	using MvvmCross.Platform;

	using AudioPlayer.Droid.Sound;

	using AudioPlayer.Portable.Sound;
	using AudioPlayer.Portable;

	/// <summary>
	/// Main page.
	/// </summary>
	[Activity(Label = "Audio Player")]
	public class MainPage : MvxActivity
	{
		#region Protected Methods

		/// <summary>
		/// Called when activity is created.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetupIoC();

			SetContentView(Resource.Layout.MainPage);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Setups the io c.
		/// </summary>
		/// <returns>The io c.</returns>
		private void SetupIoC()
		{
			Mvx.RegisterType<ISoundHandler, SoundHandler>();
			PortableMvxIoCRegistrations.InitIoC();
		}

		#endregion
	}
}