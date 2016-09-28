// --------------------------------------------------------------------------------------------------
//  <copyright file="ChatViewController.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.iOS.Views
{
	using System;
	using System.Linq;

	using Foundation;
	using UIKit;
	using CoreGraphics;

	using Chat.iOS.Extras;
	using Chat.iOS.Extensions;

	using Chat.Common.Presenter;

	/// <summary>
	/// Chat view controller.
	/// </summary>
	public class ChatViewController : UIViewController, ChatPresenter.IChatView
	{
		#region Private Properties

		/// <summary>
		/// The presenter.
		/// </summary>
		private ChatPresenter _presenter;

		/// <summary>
		/// The chat field.
		/// </summary>
		private UITextField _chatField;

		/// <summary>
		/// The scroll view.
		/// </summary>
		private UIScrollView _scrollView;

		/// <summary>
		/// The current top.
		/// </summary>
		private int _currentTop = 20;

		/// <summary>
		/// The width.
		/// </summary>
		private nfloat _width;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.iOS.Views.ChatViewController"/> class.
		/// </summary>
		/// <param name="presenter">Presenter.</param>
		public ChatViewController(ChatPresenter presenter)
		{
			_presenter = presenter;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		/// <returns>The did load.</returns>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Chat Room";

			_presenter.SetView(this);

			View.BackgroundColor = UIColor.White;

			_width = View.Bounds.Width;

			var _sendButton = new UIButton(UIButtonType.RoundedRect)
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			_sendButton.SetTitle("Send", UIControlState.Normal);
			_sendButton.TouchUpInside += HandleSendButton;

			_chatField = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.Clear.FromHex("#DFE4E6"),
				Placeholder = "Enter message"
			};

			_scrollView = new UIScrollView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			Add(_chatField);
			Add(_sendButton);
			Add(_scrollView);

			var views = new DictionaryViews()
			{
				{"sendButton", _sendButton},
				{"chatField", _chatField},
				{"scrollView", _scrollView},
			};

			this.View.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|-68-[chatField(60)]", NSLayoutFormatOptions.DirectionLeftToRight, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-62-[sendButton(60)]-20-[scrollView]|", NSLayoutFormatOptions.DirectionLeftToRight, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-5-[chatField]-[sendButton(60)]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|[scrollView]|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.ToArray());
		}

		/// <summary>
		/// Called when view is disposed.
		/// </summary>
		/// <returns>The did unload.</returns>
		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			_presenter.ReleaseView();
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
			InvokeOnMainThread(() => CreateChatBox(true, message));
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
			var alert = new UIAlertView()
			{
				Title = "Chat",
				Message = message
			};
			alert.AddButton("OK");
			alert.Show();
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
		private void HandleSendButton(object sender, EventArgs e)
		{
			_presenter.SendChat(_chatField.Text).ConfigureAwait(false);
			CreateChatBox(false, _chatField.Text);
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
			_scrollView.ContentSize = new CGSize(_width, _currentTop);

			_scrollView.AddSubview(new ChatBoxView(message)
			{
				Frame = new CGRect(received ? _width - 120 : 20, _currentTop, 100, 60),
				BackgroundColor = UIColor.Clear.FromHex(received ? "#4CD964" : "#5AC8FA")
			});

			_currentTop += 80;
		}

		#endregion
	}
}