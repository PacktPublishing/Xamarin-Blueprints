// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatEventArgs.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Events
{
	using System;

	using Chat.Common.Model;

	/// <summary>
	/// Chat event arguments.
	/// </summary>
	public class ChatEventArgs : EventArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { private set; get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Common.Events.ChatEventArgs"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		public ChatEventArgs(string message)
		{
			Message = message;
		}

		#endregion
	}
}