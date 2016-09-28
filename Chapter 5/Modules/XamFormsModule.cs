// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XamFormsModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.XamForms.Modules
{
	using System.Windows.Input;

	using Autofac;

	using Xamarin.Forms;

	using Stocklist.XamForms.Pages;
	using Stocklist.XamForms.UI;

	using Stocklist.Portable.Ioc;
	using Stocklist.Portable.UI;

	/// <summary>
	/// Xam forms module.
	/// </summary>
	public class XamFormsModule : IModule
	{
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		public void Register(ContainerBuilder builder)
		{
			builder.RegisterType<MainPage> ().SingleInstance();
			builder.RegisterType<StocklistPage> ().SingleInstance();
			builder.RegisterType<StockItemDetailsPage>().InstancePerDependency();

			builder.RegisterType<Xamarin.Forms.Command> ().As<ICommand>().InstancePerDependency();

			builder.Register (x => new NavigationPage(x.Resolve<MainPage>())).AsSelf().SingleInstance();

			builder.RegisterType<NavigationService> ().As<INavigationService>().SingleInstance();
		}

		#endregion
	}
}