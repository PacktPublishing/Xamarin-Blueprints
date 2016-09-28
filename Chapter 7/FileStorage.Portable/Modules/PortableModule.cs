// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortableModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.Modules
{
	using Autofac;

	using FileStorage.Portable.Ioc;
	using FileStorage.Portable.ViewModels;
	using FileStorage.Portable.DataAccess.Storage;

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
			builder.RegisterType<SQLiteStorage>().As<ISQLiteStorage>().SingleInstance();

			builder.RegisterType<MainPageViewModel> ().SingleInstance();
			builder.RegisterType<FilesPageViewModel> ().SingleInstance();
			builder.RegisterType<EditFilePageViewModel>().SingleInstance();

			builder.RegisterType<FileItemViewModel>().InstancePerDependency();
		}

		#endregion
	}
}