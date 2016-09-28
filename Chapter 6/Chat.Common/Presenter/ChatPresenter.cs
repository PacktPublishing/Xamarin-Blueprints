// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatPresenter.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Presenter
{
	using System.Threading.Tasks;

	using Chat.Common.Events;
	using Chat.Common.Model;

	using Chat.ServiceAccess.SignalR;

	/// <summary>
	/// Chat presenter.
	/// </summary>
	public class ChatPresenter : BasePresenter
	{
		#region Private Properties

		/// <summary>
		/// The client.
		/// </summary>
		private Client _client;

		/// <summary>
		/// The view.
		/// </summary>
		private IChatView _view;

		#endregion

		#region IChatView

		/// <summary>
		/// Chat view.
		/// </summary>
		public interface IChatView : IView
		{
			void NotifyChatMessageReceived(string message);

			void CreateChatBox(bool received, string message);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Common.Presenter.ChatPresenter"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="navigationService">Navigation service.</param>
		/// <param name="client">Client.</param>
		/// <param name="signalRClient">Signal RC lient.</param>
		public ChatPresenter(ApplicationState state, INavigationService navigationService, 
		                     Client client, SignalRClient signalRClient)
		{
			_navigationService = navigationService;
			_state = state;
			_client = client;
			_signalRClient = signalRClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the view.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="view">View.</param>
		public void SetView(IChatView view)
		{
			_view = view;

			_signalRClient.OnDataReceived -= HandleSignalRDataReceived;
			_signalRClient.OnDataReceived += HandleSignalRDataReceived;

			ChatReceived -= HandleChatReceived;
			ChatReceived += HandleChatReceived;
		}

		/// <summary>
		/// Releases the view.
		/// </summary>
		/// <returns>The view.</returns>
		public void ReleaseView()
		{
			_signalRClient.OnDataReceived -= HandleSignalRDataReceived;
		}

		/// <summary>
		/// Sends the chat.
		/// </summary>
		/// <returns>The chat.</returns>
		/// <param name="message">Message.</param>
		public async Task SendChat(string message)
		{
			await _signalRClient.SendMessageToClient(_client.Username, message);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the chat received.
		/// </summary>
		/// <returns>The chat received.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleChatReceived(object sender, ChatEventArgs e)
		{
			_view.NotifyChatMessageReceived(e.Message);
		}

		#endregion
	}
}