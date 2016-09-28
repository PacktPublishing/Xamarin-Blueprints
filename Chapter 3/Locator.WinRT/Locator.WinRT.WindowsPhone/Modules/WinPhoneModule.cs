// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinPhoneModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.WinPhone.Modules
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Autofac;

    using Locator.WinPhone.Location;
    using Locator.WinPhone.Extras;
    using Locator.Portable.Location;
    using Locator.Portable.Extras;
    using Locator.Portable.Ioc;

	/// <summary>
	/// Windows phone module.
	/// </summary>
    public class WinPhoneModule : IModule
    {
		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GeolocatorWinPhone>().As<IGeolocator>().SingleInstance();
            builder.RegisterType<WinPhoneMethods>().As<IMethods>().SingleInstance();
        }
    }
}