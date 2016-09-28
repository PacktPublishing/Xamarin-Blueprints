// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOSModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.iOS.Modules
{
	using Autofac;

	using Stocklist.iOS.Extras;

	using Stocklist.Portable.Extras;
	using Stocklist.Portable.Ioc;

	/// <summary>
	/// The iOS Module.
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
		}

		#endregion
	}
}