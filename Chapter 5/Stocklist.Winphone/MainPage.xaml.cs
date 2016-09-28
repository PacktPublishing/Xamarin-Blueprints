// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.cs" company="Flush Arcade">
//   Copyright (c) 2015 Flush Arcade All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Winphone
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    using Xamarin.Forms;

    using Stocklist.Winphone.Modules;

    using Stocklist.Shared.Modules;

    using Stocklist.XamForms.Modules;

    using Stocklist.Portable.Modules;
    using Stocklist.Portable.Ioc;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
		#region Public Methods

		/// <summary>
		/// Initializes a new instance of MainPage
		/// </summary>
		/// <returns>The io c.</returns>
        public MainPage()
        {
            InitializeComponent();

			InitIoC();

            this.NavigationCacheMode = NavigationCacheMode.Required;
			LoadApplication(new Stocklist.XamForms.App());
        }

		#endregion

		#region Private Methods

		/// <summary>
		/// Inits the IoC container
		/// </summary>
		/// <returns>The io c.</returns>
        private void InitIoC()
        {
            IoC.CreateContainer();
            IoC.RegisterModule(new WinphoneModule());
            IoC.RegisterModule(new SharedModule(true));
            IoC.RegisterModule(new XamFormsModule());
            IoC.RegisterModule(new PortableModule());
            IoC.StartContainer();
        }

		#endregion
    }
}
