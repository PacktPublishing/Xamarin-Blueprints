// --------------------------------------------------------------------------------------------------
//  <copyright file="App.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Portable
{
	using System;

	using MvvmCross.Core.ViewModels;

	using MvvmCross.Platform.IoC;

	using Chat.Portable.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
			RegisterAppStart<MainPageViewModel>();
        }
    }
}