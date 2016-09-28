// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XamFormsModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Modules
{
	using System.Windows.Input;

	using Autofac;

	using Xamarin.Forms;

	using FileStorage.Portable.Ioc;
	using FileStorage.Pages;
	using FileStorage.UI;

	using FileStorage.Portable.UI;

	/// <summary>
	/// Xamarin forms module.
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
			builder.RegisterType<FilesPage> ().SingleInstance();
			builder.RegisterType<EditFilePage>().SingleInstance();

			builder.RegisterType<Command> ().As<ICommand>().InstancePerDependency();

			builder.Register (x => new NavigationPage(x.Resolve<MainPage>())).AsSelf().SingleInstance();

			builder.RegisterType<NavigationService> ().As<INavigationService>().SingleInstance();
		}

		#endregion
	}
}