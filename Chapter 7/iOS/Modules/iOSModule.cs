// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOSModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.iOS.Modules
{
	using Autofac;

	using FileStorage.iOS.DataAccess;
	using FileStorage.iOS.Extras;
	using FileStorage.iOS.Logging;

	using FileStorage.Portable.DataAccess.Storage;
	using FileStorage.Portable.Extras;
	using FileStorage.Portable.Ioc;
	using FileStorage.Portable.Logging;

	using SQLite.Net.Interop;
	using SQLite.Net.Platform.XamarinIOS;

	/// <summary>
	/// IOS Module.
	/// </summary>
	public class IOSModule : IModule
	{
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		public void Register(ContainerBuilder builder)
		{
			builder.RegisterType<IOSMethods>().As<IMethods>().SingleInstance();
			builder.RegisterType<LoggeriOS>().As<ILogger>().SingleInstance();

			builder.RegisterType<SQLiteSetup>().As<ISQLiteSetup>().SingleInstance();
			builder.RegisterType<SQLitePlatformIOS>().As<ISQLitePlatform>().SingleInstance();
		}

		#endregion
	}
}