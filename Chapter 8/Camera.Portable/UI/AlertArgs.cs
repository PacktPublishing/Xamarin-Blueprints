// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertArgs.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Portable
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// Alert arguments.
	/// </summary>
	public class AlertArgs : EventArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the tcs.
		/// </summary>
		/// <value>The tcs.</value>
		public TaskCompletionSource<bool> Tcs { get; set; }

		#endregion
	}
}