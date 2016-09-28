// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectedClientsUpdatedEventArgs.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Events
{
	using System;
	using System.Collections.Generic;

	using Chat.Common.Model;

	/// <summary>
	/// Connected clients updated event arguments.
	/// </summary>
	public class ConnectedClientsUpdatedEventArgs : EventArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets the connected clients.
		/// </summary>
		/// <value>The connected clients.</value>
		public IList<Client> ConnectedClients { private set; get; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Common.Events.ConnectedClientsUpdatedEventArgs"/> class.
		/// </summary>
		/// <param name="connectedClients">Connected clients.</param>
		public ConnectedClientsUpdatedEventArgs(IEnumerable<Client> connectedClients)
		{
			ConnectedClients = new List<Client>();

			foreach (var client in connectedClients)
			{
				ConnectedClients.Add(client);
			}
		}

		#endregion
	}
}