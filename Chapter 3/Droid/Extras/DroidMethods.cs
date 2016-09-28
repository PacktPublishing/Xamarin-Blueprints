// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DroidMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Droid.Extras
{
	using Locator.Portable.Extras;

	/// <summary>
	/// The android methods interface
	/// </summary>
	public class DroidMethods : IMethods
	{
		#region Public Methods

		/// <summary>
		/// Exit this instance.
		/// </summary>
		public void Exit()
		{
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
		}

		#endregion
	}
}