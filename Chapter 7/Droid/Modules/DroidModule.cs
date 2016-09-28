// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DroidModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Droid.Modules
{
	using SQLite.Net.Interop;
	using SQLite.Net.Platform.XamarinAndroid;

	using Autofac;

	using FileStorage.Droid.Extras;
	using FileStorage.Droid.DataAccess;
	using FileStorage.Droid.Logging;

	using FileStorage.Portable.Extras;
	using FileStorage.Portable.Logging;
	using FileStorage.Portable.DataAccess.Storage;
	using FileStorage.Portable.Ioc;

	/// <summary>
	/// Droid module.
	/// </summary>
	public class DroidModule : IModule
	{
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		public void Register(ContainerBuilder builder)
		{
			builder.RegisterType<DroidMethods>().As<IMethods>().SingleInstance();
			builder.RegisterType<LoggerDroid>().As<ILogger>().SingleInstance();

			builder.RegisterType<SQLiteSetup>().As<ISQLiteSetup>().SingleInstance();
			builder.RegisterType<SQLitePlatformAndroid>().As<ISQLitePlatform>().SingleInstance();
		}

		#endregion
	}
}