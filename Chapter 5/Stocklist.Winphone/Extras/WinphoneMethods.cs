// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinphoneMethods.cs" company="Flush Arcade">
//   Copyright (c) 2015 Flush Arcade All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Winphone.Extras
{
    using Windows.UI.Xaml;

    using Stocklist.Portable.Extras;

    /// <summary>
    /// The methods interface
    /// </summary>
    public class WinphoneMethods : IMethods
    {
		#region Public Methods

		/// <summary>
	    /// Exits the application
	    /// </summary>
        public void Exit()
        {
            Application.Current.Exit();
        }

		#endregion
    }
}

