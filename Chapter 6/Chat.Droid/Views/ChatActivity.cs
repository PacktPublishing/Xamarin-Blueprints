// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Droid.Views
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Widget;
	using Android.Views;
	using Android.Graphics;

	using Chat.Common.Presenter;

	/// <summary>
	/// Chat activity.
	/// </summary>
	[Activity(Label = "Chat", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ChatActivity : Activity, ChatPresenter.IChatView
	{
		#region Private Properties

		/// <summary>
		/// The presenter.
		/// </summary>
		private ChatPresenter _presenter;

		/// <summary>
		/// The scroll view inner layout.
		/// </summary>
		private LinearLayout _scrollViewInnerLayout;

		/// <summary>
		/// The edit text.
		/// </summary>
		private EditText _editText;

		/// <summary>
		/// The last send click.
		/// </summary>
		private long _lastSendClick = 0;

		/// <summary>
		/// The width.
		/// </summary>
		private int _width;

		/// <summary>
		/// The current top.
		/// </summary>
		private float _currentTop;

		/// <summary>
		/// The dialog shown.
		/// </summary>
		private bool _dialogShown = false;

		#endregion

		#region Protected Methods

		/// <summary>
		/// Ons the create.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.ChatView);

			var metrics = Resources.DisplayMetrics;
			_width = (int)((metrics.WidthPixels) / Resources.DisplayMetrics.Density);

			_scrollViewInnerLayout = FindViewById<LinearLayout>(Resource.Id.scrollViewInnerLayout);
			_editText = FindViewById<EditText>(Resource.Id.chatField);

			var sendButton = FindViewById<Button>(Resource.Id.sendButton);
			sendButton.Touch += HandleSendButton;

			var app = ChatApplication.GetApplication(this);
			app.CurrentActivity = this;

			_presenter = app.Presenter as ChatPresenter;
			_presenter.SetView(this);
			app.CurrentActivity = this;
		}

		/// <summary>
		/// Ons the pause.
		/// </summary>
		/// <returns>The pause.</returns>
		protected override void OnPause()
		{
			base.OnPause();

			if (_presenter != null)
			{
				_presenter.ReleaseView();
			}
		}

		#endregion

		#region IChatView implementation

		/// <summary>
		/// Notifies the chat message received.
		/// </summary>
		/// <returns>The chat message received.</returns>
		/// <param name="message">Message.</param>
		public void NotifyChatMessageReceived(string message)
		{
			// perform action on UI thread
			Application.SynchronizationContext.Post(state =>
			{
				CreateChatBox(true, message);
			}, null);
		}

		#endregion

		#region IView implementation

		/// <summary>
		/// Sets the error message.
		/// </summary>
		/// <returns>The error message.</returns>
		/// <param name="message">Message.</param>
		public void SetErrorMessage(string message)
		{
			if (!_dialogShown)
			{
				_dialogShown = true;

				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder
					.SetTitle("Chat")
					.SetMessage(message)
					.SetNeutralButton("Ok", (sender, e) => { _dialogShown = false; })
					.Show();
			}
		}

		/// <summary>
		/// Gets or sets the is in progress.
		/// </summary>
		/// <value>The is in progress.</value>
		public bool IsInProgress { get; set; }

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the send button.
		/// </summary>
		/// <returns>The send button.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleSendButton(object sender, View.TouchEventArgs e)
		{
			// multiple-clicking prevention using a threshold of 1000 ms
			if (SystemClock.ElapsedRealtime() - _lastSendClick < 1000)
			{
				return;
			}
			_lastSendClick = SystemClock.ElapsedRealtime();

			_presenter.SendChat(_editText.Text).ConfigureAwait(false);
			CreateChatBox(false, _editText.Text);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the chat box.
		/// </summary>
		/// <returns>The chat box.</returns>
		/// <param name="received">Received.</param>
		/// <param name="message">Message.</param>
		public void CreateChatBox(bool received, string message)
		{
			var view = LayoutInflater.Inflate(Resource.Layout.ChatBoxView, null);
			view.SetX(received ? _width : 0);
			view.SetY(_currentTop);

			var messageTextView = view.FindViewById<TextView>(Resource.Id.messageTextView);
			messageTextView.Text = message;

			var color = Color.ParseColor(received ? "#4CD964" : "#5AC8FA");

			messageTextView.SetBackgroundColor(color);

			_scrollViewInnerLayout.AddView(view);

			_currentTop += 60;
		}

		#endregion
	}
}