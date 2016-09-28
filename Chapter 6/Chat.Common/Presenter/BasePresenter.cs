// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientsListPresenter.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Presenter
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Threading;

	using Newtonsoft.Json;

	using Chat.ServiceAccess.SignalR;
	using Chat.ServiceAccess.Web;

	using Chat.Common.Events;
	using Chat.Common.Model;

	/// <summary>
	/// Base presenter.
	/// </summary>
	public abstract class BasePresenter
	{
		#region Private Properties

		/// <summary>
		/// The signal RE vents.
		/// </summary>
		private IDictionary<string, Action<string>> _signalREvents;

		#endregion

		#region Protected Properties

		/// <summary>
		/// The navigation service.
		/// </summary>
		protected INavigationService _navigationService;

		/// <summary>
		/// The state.
		/// </summary>
		protected ApplicationState _state;

		/// <summary>
		/// The signal RC lient.
		/// </summary>
		protected SignalRClient _signalRClient;

		/// <summary>
		/// The web API access.
		/// </summary>
		protected WebApiAccess _webApiAccess;

		/// <summary>
		/// The access token.
		/// </summary>
		protected string _accessToken;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when connected clients updated.
		/// </summary>
		public event EventHandler<ConnectedClientsUpdatedEventArgs> ConnectedClientsUpdated;

		/// <summary>
		/// Occurs when chat received.
		/// </summary>
		public event EventHandler<ChatEventArgs> ChatReceived;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Common.Presenter.BasePresenter"/> class.
		/// </summary>
		public BasePresenter()
		{
			_webApiAccess = new WebApiAccess();

			_signalREvents = new Dictionary<string, Action<string>>()
			{
				{"clients", (data) => 
					{
						var list = JsonConvert.DeserializeObject<IEnumerable<string>>(data);

						if (ConnectedClientsUpdated != null)
						{
							ConnectedClientsUpdated(this, new ConnectedClientsUpdatedEventArgs(list.Select(x => new Client
							{
								Username = x,
							})));
						}
					}
				},
				{"chat", (data) => 
					{
						if (ChatReceived != null)
						{
							ChatReceived(this, new ChatEventArgs(data));
						}
					}
				},
			};
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Inits the signal r.
		/// </summary>
		/// <returns>The signal r.</returns>
		/// <param name="accessToken">Access token.</param>
		public async Task InitSignalR(string accessToken)
		{
			_signalRClient = new SignalRClient();

			await _signalRClient.Connect(accessToken);
		}

		/// <summary>
		/// Gets all connected clients.
		/// </summary>
		/// <returns>The all connected clients.</returns>
		public async Task GetAllConnectedClients()
		{
			var clients = await _webApiAccess.GetAllConnectedUsersAsync(CancellationToken.None);

			if (clients != null)
			{
				if (ConnectedClientsUpdated != null)
				{
					ConnectedClientsUpdated(this,
						new ConnectedClientsUpdatedEventArgs(clients
					                                         .Where(x => !x.ToLower().Contains(_state.Username))
					                                         .Select(x => new Client
															 {
																Username = x,
															 })));
				}
			}
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Handles the signal RD ata received.
		/// </summary>
		/// <returns>The signal RD ata received.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void HandleSignalRDataReceived(object sender, Tuple<string, string> e)
		{
			_signalREvents[e.Item1](e.Item2);
		}

		#endregion
	}
}