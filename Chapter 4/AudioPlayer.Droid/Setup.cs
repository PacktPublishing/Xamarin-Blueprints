// --------------------------------------------------------------------------------------------------
//  <copyright file="AndroidSetup.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid
{
	using Android.Content;

	using MvvmCross.Droid.Platform;
	using MvvmCross.Platform.Platform;
	using MvvmCross.Core.ViewModels;

	using AudioPlayer.Portable.Logging;
	using AudioPlayer.Portable;

	/// <summary>
	/// Setup.
	/// </summary>
	public class Setup : MvxAndroidSetup
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.Setup"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public Setup(Context context) :base(context)
		{
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Creates the app.
		/// </summary>
		/// <returns>The app.</returns>
		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		/// <summary>
		/// Creates the debug trace.
		/// </summary>
		/// <returns>The debug trace.</returns>
		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}

		#endregion
	}
}