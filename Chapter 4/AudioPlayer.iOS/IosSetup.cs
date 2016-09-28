// --------------------------------------------------------------------------------------------------
//  <copyright file="IosSetup.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS
{
	using UIKit;

	using MvvmCross.iOS.Platform;

	using MvvmCross.Core.ViewModels;

	using MvvmCross.Platform.Converters;
	using MvvmCross.Platform.Platform;

	using AudioPlayer.Portable.Logging;
	using AudioPlayer.Portable;

	public class IosSetup : MvxIosSetup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TicTacToeLab.iOS.IosSetup"/> class.
		/// </summary>
		/// <param name="applicationDelegate">Application delegate.</param>
		/// <param name="window">Window.</param>
		public IosSetup(MvxApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
		{
		}

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
	}
}