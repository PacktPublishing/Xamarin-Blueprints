// --------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreenActivity.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Droid
{
	using Android.Widget;
	using Android.OS;
	using Android.Content.PM;
	using Android.App;

	using MvvmCross.Core.ViewModels;
	using MvvmCross.Platform;
	using MvvmCross.Droid.Views;

	[Activity(Label = "Chat.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		public SplashScreenActivity(): base(Resource.Layout.SplashScreen)
		{
		}
	}
}