// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortableModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.Modules
{
	using System;

	using Autofac;

	using Stocklist.Portable.Ioc;
	using Stocklist.Portable.ViewModels;
	using Stocklist.Portable.UI;

	using Stocklist.Portable.WebServices.StocklistWebServiceController;

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
			builder.RegisterType<StocklistPageViewModel> ().SingleInstance();
			builder.RegisterType<StockItemDetailsPageViewModel>().InstancePerDependency();

			builder.RegisterType<StockItemViewModel>().InstancePerDependency();

			builder.RegisterType<StocklistWebServiceController> ().As<IStocklistWebServiceController>().SingleInstance();
		}

		#endregion
	}
}