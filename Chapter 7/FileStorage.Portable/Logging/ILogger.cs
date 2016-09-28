// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILog.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.Logging
{
    /// <summary>
    /// The Debug interface.
    /// </summary>
    public interface ILogger
    {
        #region Public Methods

        /// <summary>
        /// The write line.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void WriteLine(string message);

        /// <summary>
        /// The write line time.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        void WriteLineTime(string message, params object[] args);

        #endregion
    }
}