// --------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreenActivity.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid
{
	using Android.Content.PM;
	using Android.App;

	using MvvmCross.Droid.Views;

	/// <summary>
	/// Splash screen activity.
	/// </summary>
	[Activity(Label = "AudioPlayer.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.SplashScreenActivity"/> class.
		/// </summary>
		public SplashScreenActivity(): base(Resource.Layout.SplashScreen)
		{
		}

		#endregion
	}
}