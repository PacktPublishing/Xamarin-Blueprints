// --------------------------------------------------------------------------------------------------
//  <copyright file="XamarinNavigationExtensions.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2014 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.UI
{
	using System.Collections.Generic;

	using Camera.Portable.ViewModels;

	/// <summary>
	/// Xamarin navigation extensions.
	/// </summary>
	public static class XamarinNavigationExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Show the specified page and parameters.
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="parameters">Parameters.</param>
		public static void Show(this ExtendedContentPage page, IDictionary<string, object> parameters)
		{
			var target = page.BindingContext as ViewModelBase;

			if (target != null)
			{
				target.OnShow(parameters);
			}
		}

		#endregion
	}
}