// --------------------------------------------------------------------------------------------------
//  <copyright file="ISoundHandler.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Portable
{
	using MvvmCross.Platform;

	using AudioPlayer.Portable.ViewModels;

	/// <summary>
	/// Portable mvx io CR egistrations.
	/// </summary>
	public static class PortableMvxIoCRegistrations
	{
		#region Public Static Methods

		/// <summary>
		/// Inits the IoC container.
		/// </summary>
		/// <returns>The io c.</returns>
		public static void InitIoC()
		{
			Mvx.IocConstruct<MainPageViewModel>();
			Mvx.IocConstruct<AudioPlayerPageViewModel>();
		}

		#endregion
	}
}