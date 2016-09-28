// --------------------------------------------------------------------------------------------------
//  <copyright file="ISoundHandler.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Portable
{
	using MvvmCross.Platform;

	using Chat.Portable.ViewModels;

	public static class PortableMvxIoCRegistrations
	{
		public static void InitIoC()
		{
			Mvx.IocConstruct<MainPageViewModel>();
			Mvx.IocConstruct<ChatPageViewModel>();
		}
	}
}