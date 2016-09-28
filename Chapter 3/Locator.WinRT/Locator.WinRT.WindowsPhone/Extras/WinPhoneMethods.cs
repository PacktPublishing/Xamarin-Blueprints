// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinPhoneMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.WinPhone.Extras
{
    using Locator.Portable.Extras;

    using Windows.UI.Xaml;

    /// <summary>
    /// The methods interface
    /// </summary>
    public class WinPhoneMethods : IMethods
    {
		/// <summary>
	    /// Exits the application.
	    /// </summary>
        public void Exit()
        {
            Application.Current.Exit();
        }
    }
}

