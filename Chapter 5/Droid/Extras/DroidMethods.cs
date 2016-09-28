// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Stocklist.Droid.Extras
{
	using Stocklist.Portable.Extras;

	/// <summary>
	/// The methods interface
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