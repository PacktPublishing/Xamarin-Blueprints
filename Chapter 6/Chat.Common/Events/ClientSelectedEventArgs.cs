// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientSelectedEventArgs.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Events
{
	using System;

	using Chat.Common.Model;

	/// <summary>
	/// Client selected event arguments.
	/// </summary>
	public class ClientSelectedEventArgs : EventArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets the client.
		/// </summary>
		/// <value>The client.</value>
		public Client Client { private set; get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Common.Events.ClientSelectedEventArgs"/> class.
		/// </summary>
		/// <param name="client">Client.</param>
		public ClientSelectedEventArgs(Client client)
		{
			Client = client;
		}

		#endregion
	}
}