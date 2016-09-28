// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerDroid.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Droid.Logging
{
	using System;
	using System.Diagnostics;

	using Android.Util;

	using FileStorage.Portable.Logging;

	/// <summary>
	/// Droid log.
	/// </summary>
	public class LoggerDroid : ILogger
	{
		#region IDebug implementation

		/// <summary>
		/// Writes the line.
		/// </summary>
		/// <returns>The line.</returns>
		/// <param name="text">Text.</param>
		public void WriteLine(string text)
		{
			Log.WriteLine(LogPriority.Info, text, null);
		}

		/// <summary>
		/// Writes the line time.
		/// </summary>
		/// <returns>The line time.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		public void WriteLineTime(string text, params object[] args)
		{
			Log.WriteLine(LogPriority.Info, DateTime.Now.Ticks + " " + String.Format(text, args), null);
		}

		#endregion
	}

}