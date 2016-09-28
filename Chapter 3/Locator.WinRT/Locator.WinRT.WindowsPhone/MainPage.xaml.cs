// --------------------------------------------------------------------------------------------------
//  <copyright file="MainPage.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Locator.WinPhone
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

	using Locator.Modules;
	using Locator.Portable.Ioc;
	using Locator.Portable.Modules;
	using Locator.Shared.Modules;
	using Locator.WinPhone.Modules;

    /// <summary>
	/// Main page.
	/// </summary>
    public sealed partial class MainPage : Xamarin.Forms.Platform.WinRT.WindowsPhonePage
    {
        public MainPage()
        {
            this.InitializeComponent();

            InitIoC();

            NavigationCacheMode = NavigationCacheMode.Required;
            LoadApplication(new Locator.App());
        }

        private void InitIoC()
        {
            IoC.CreateContainer();
            IoC.RegisterModule(new WinPhoneModule());
            IoC.RegisterModule(new SharedModule(true));
            IoC.RegisterModule(new XamFormsModule());
            IoC.RegisterModule(new PortableModule());
            IoC.StartContainer();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
