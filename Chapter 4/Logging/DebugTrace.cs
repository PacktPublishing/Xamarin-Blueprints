// --------------------------------------------------------------------------------------------------
//  <copyright file="DebugTrace.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Portable.Logging
{
	using System;
	using System.Diagnostics;

	using MvvmCross.Platform.Platform;

	/// <summary>
	/// Debug trace.
	/// </summary>
    public class DebugTrace : IMvxTrace
    {
		#region Public Methods

		/// <summary>
		/// Trace the specified level, tag and message.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="message">Message.</param>
        public void Trace(MvxTraceLevel level, string tag, Func<string> message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message());
        }

		/// <summary>
		/// Trace the specified level, tag and message.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="message">Message.</param>
        public void Trace(MvxTraceLevel level, string tag, string message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message);
        }

		/// <summary>
		/// Trace the specified level, tag, message and args.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
        public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
        {
            try
            {
                Debug.WriteLine(string.Format(tag + ":" + level + ":" + message, args));
            }
            catch (FormatException)
            {
                Trace(MvxTraceLevel.Error, tag, "Exception during trace of {0} {1}", level, message);
            }
        }

		#endregion
    }
}