// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOSMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.iOS.Extras
{
	using UIKit;

	using Locator.Portable.Extras;

	/// <summary>
	/// The methods interface
	/// </summary>
	public class IOSMethods : IMethods
	{
		#region Public Methods

		/// <summary>
		/// Exit this instance.
		/// </summary>
		public void Exit()
		{
			UIApplication.SharedApplication.PerformSelector(new ObjCRuntime.Selector("terminateWithSuccess"), null, 0f);
		}

		#endregion
	}
}

