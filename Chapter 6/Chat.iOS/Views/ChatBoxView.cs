// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatBoxView.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.iOS.Views
{
	using System.Linq;

	using UIKit;

	using Chat.iOS.Extras;

	/// <summary>
	/// Chat box view.
	/// </summary>
	public class ChatBoxView : UIView
	{
		#region Private Properties

		/// <summary>
		/// The message label.
		/// </summary>
		private UILabel messageLabel;

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.iOS.Views.ChatBoxView"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		public ChatBoxView(string message)
		{
			Layer.CornerRadius = 10;

			messageLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Text = message
			};

			Add(messageLabel);

			var views = new DictionaryViews()
			{
				{"messageLabel", messageLabel},
			};

			AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[messageLabel]|", NSLayoutFormatOptions.AlignAllTop, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-5-[messageLabel]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.ToArray());
		}

		#endregion
	}
}