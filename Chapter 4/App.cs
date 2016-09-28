// --------------------------------------------------------------------------------------------------
//  <copyright file="App.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Portable
{
	using MvvmCross.Platform.IoC;
	using MvvmCross.Core.ViewModels;

	using AudioPlayer.Portable.ViewModels;

	/// <summary>
	/// App.
	/// </summary>
    public class App : MvxApplication
    {
		#region Public Methods

		/// <summary>
		/// Initialize this instance.
		/// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
			RegisterAppStart<MainPageViewModel>();
        }

		#endregion
    }
}