// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortableModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Portable.Modules
{
	using System;

	using Autofac;

	using Locator.Portable.Ioc;
	using Locator.Portable.ViewModels;
	using Locator.Portable.UI;
	using Locator.Portable.Location;

	using Locator.Portable.WebServices.GeocodingWebServiceController;

	/// <summary>
	/// Portable module.
	/// </summary>
	public class PortableModule : IModule
	{
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		public void Register(ContainerBuilder builder)
		{
			builder.RegisterType<MainPageViewModel> ().SingleInstance();
			builder.RegisterType<MapPageViewModel> ().SingleInstance();

			builder.RegisterType<Position> ().As<IPosition>().SingleInstance();

			builder.RegisterType<GeocodingWebServiceController> ().As<IGeocodingWebServiceController>().SingleInstance();
		}

		#endregion
	}
}