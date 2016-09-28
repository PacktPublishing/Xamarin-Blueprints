// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinphoneModule.cs" company="Flush Arcade">
//   Copyright (c) 2015 Flush Arcade All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Winphone.Modules
{
	using Autofac;

    using Stocklist.Winphone.Extras;

    using Stocklist.Portable.Extras;
    using Stocklist.Portable.Ioc;

	/// <summary>
	/// The Win phone Module.
	/// </summary>
	public class WinphoneModule : IModule
	{
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		public void Register(ContainerBuilder builder)
		{
            builder.RegisterType<WinphoneMethods>().As<IMethods>().SingleInstance();
		}

		#endregion
	}
}