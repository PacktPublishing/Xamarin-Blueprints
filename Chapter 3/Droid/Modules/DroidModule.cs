// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOSModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Droid.Modules
{
	using Locator.Portable.Ioc;

	using Autofac;

	using Locator.Droid.Location;
	using Locator.Droid.Extras;

	using Locator.Portable.Extras;

	using Locator.Portable.Location;

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
			builder.RegisterType<GeolocatorDroid>().As<IGeolocator>().SingleInstance();
			builder.RegisterType<DroidMethods>().As<IMethods>().SingleInstance();
		}

		#endregion
	}
}