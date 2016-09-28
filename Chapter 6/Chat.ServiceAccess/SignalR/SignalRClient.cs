// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRClient.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.ServiceAccess.SignalR
{
	using System;
	using System.Threading.Tasks;

	using Microsoft.AspNet.SignalR.Client;

	using Chat.Common.Presenter;

	/// <summary>
	/// Signal RC lient.
	/// </summary>
	public class SignalRClient
    {
		#region Private Properties

		/// <summary>
		/// The connection.
		/// </summary>
		private readonly HubConnection _connection;

		/// <summary>
		/// The proxy.
		/// </summary>
        private readonly IHubProxy _proxy;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when on data received.
		/// </summary>
		public event EventHandler<Tuple<string, string>> OnDataReceived;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.ServiceAccess.SignalR.SignalRClient"/> class.
		/// </summary>
		public SignalRClient()
        {
			_connection = new HubConnection("http://10.0.0.128:52786/");
            _proxy = _connection.CreateHubProxy("ChatHub");
        }

		#endregion

		#region Public Methods

		/// <summary>
		/// Connect the specified accessToken.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		public async Task<bool> Connect(string accessToken)
        {
			try
			{
				_connection.Headers.Add("Authorization", string.Format("Bearer {0}", accessToken));

				await _connection.Start();

				_proxy.On<string, string>("displayMessage", (id, data) =>
				{
					if (OnDataReceived != null)
					{
						OnDataReceived(this, new Tuple<string, string>(id, data));
					}
				});

				return true;
			}
			catch (Exception e) 
			{
				Console.WriteLine(e);
			}

			return false;
		}

		/// <summary>
		/// Disconnect this instance.
		/// </summary>
		public void Disconnect()
		{
			_connection.Stop();
			_connection.Dispose();
		}

		/// <summary>
		/// Sends the message to client.
		/// </summary>
		/// <returns>The message to client.</returns>
		/// <param name="user">User.</param>
		/// <param name="message">Message.</param>
		public async Task SendMessageToClient(string user, string message)
		{
			await _proxy.Invoke("Send", new object[]
			{
				message,
				user
			});
		}

		#endregion
	}
}