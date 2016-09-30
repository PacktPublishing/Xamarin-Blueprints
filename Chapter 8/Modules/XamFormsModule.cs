// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XamFormsModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Modules
{
	using System.Windows.Input;

	using Autofac;

	using Xamarin.Forms;

	using Camera.Portable.Ioc;
	using Camera.Pages;
	using Camera.UI;

	using Camera.Portable.UI;

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
			builder.RegisterType<CameraPage> ().SingleInstance();

			builder.RegisterType<Command> ().As<ICommand>().InstancePerDependency();

			builder.Register (x => new NavigationPage(x.Resolve<MainPage>())).AsSelf().SingleInstance();

			builder.RegisterType<NavigationService> ().As<INavigationService>().SingleInstance();
		}

		#endregion
	}
}