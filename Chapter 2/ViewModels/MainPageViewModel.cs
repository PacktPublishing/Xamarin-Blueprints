// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.ViewModels
{
	using System;
	using System.Windows.Input;

	using Xamarin.Forms;

	/// <summary>
	/// Main page view model.
	/// </summary>
	public class MainPageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The text to speech.
		/// </summary>
		private readonly ITextToSpeech _textToSpeech;

		/// <summary>
		/// The description message.
		/// </summary>
		private string descriptionMessage = "Enter text and press the 'Speak' button to start speaking";

		/// <summary>
		/// The speak entry placeholder.
		/// </summary>
		private string speakEntryPlaceholder = "Text to speak";

		/// <summary>
		/// The speak text.
		/// </summary>
		private string speakText = string.Empty;

		/// <summary>
		/// The speak title.
		/// </summary>
		private string speakTitle = "Speak";

		/// <summary>
		/// The speak command.
		/// </summary>
		private ICommand speakCommand;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the description message.
		/// </summary>
		/// <value>The description message.</value>
		public string DescriptionMessage
		{
			get
			{
				return descriptionMessage;
			}

			set
			{
				if (value.Equals(descriptionMessage))
				{
					return;
				}

				descriptionMessage = value;
				OnPropertyChanged("DescriptionMessage");
			}
		}

		/// <summary>
		/// Gets or sets the speak entry placeholder.
		/// </summary>
		/// <value>The speak entry placeholder.</value>
		public string SpeakEntryPlaceholder
		{
			get
			{
				return speakEntryPlaceholder;
			}

			set
			{
				if (value.Equals(speakEntryPlaceholder))
				{
					return;
				}

				speakEntryPlaceholder = value;
				OnPropertyChanged("SpeakEntryPlaceholder");
			}
		}

		/// <summary>
		/// Gets or sets the speak text.
		/// </summary>
		/// <value>The speak text.</value>
		public string SpeakText
		{
			get
			{
				return speakText;
			}

			set
			{
				if (value.Equals(speakText))
				{
					return;
				}

				speakText = value;
				OnPropertyChanged("SpeakText");
			}
		}

		/// <summary>
		/// Gets or sets the speak title.
		/// </summary>
		/// <value>The speak title.</value>
		public string SpeakTitle
		{
			get
			{
				return speakTitle;
			}

			set
			{
				if (value.Equals(speakTitle))
				{
					return;
				}

				speakTitle = value;
				OnPropertyChanged("SpeakTitle");
			}
		}

		/// <summary>
		/// Gets or sets the speak command.
		/// </summary>
		/// <value>The speak command.</value>
		public ICommand SpeakCommand
		{
			get
			{
				return speakCommand;
			}

			set
			{
				if (value.Equals(speakCommand))
				{
					return;
				}

				speakCommand = value;
				OnPropertyChanged("SpeakCommand");
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SpeechTalk.ViewModels.MainPageViewModel"/> class.
		/// </summary>
		/// <param name="textToSpeech">Text to speech.</param>
		public MainPageViewModel (ITextToSpeech textToSpeech)
		{
			_textToSpeech = textToSpeech;

			speakCommand = new Command ((c) => textToSpeech.Speak (SpeakText));
		}

		#endregion
	}
}