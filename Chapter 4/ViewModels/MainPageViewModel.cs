// --------------------------------------------------------------------------------------------------
//  <copyright file="MainPageViewModel.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Portable.ViewModels
{
	using MvvmCross.Core.ViewModels;

	/// <summary>
	/// Main page view model.
	/// </summary>
	public class MainPageViewModel : MvxViewModel
	{
		#region Private Properties

		/// <summary>
		/// The title.
		/// </summary>
		private string title = "Welcome";

		/// <summary>
		/// The description message.
		/// </summary>
		private string descriptionMessage = "Welcome to the Music Room";

		/// <summary>
		/// The audio player title.
		/// </summary>
		private string audioPlayerTitle = "Audio Player";

		/// <summary>
		/// The exit title.
		/// </summary>
		private string exitTitle = "Exit";

		/// <summary>
		/// The audio player command.
		/// </summary>
		private MvxCommand audioPlayerCommand;

		/// <summary>
		/// The exit command.
		/// </summary>
		private MvxCommand exitCommand;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				if (!value.Equals(title))
				{
					descriptionMessage = value;
					RaisePropertyChanged(() => Title);
				}
			}
		}

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
				if (!value.Equals(descriptionMessage))
				{
					descriptionMessage = value;
					RaisePropertyChanged (() => DescriptionMessage);
				}
			}
		}

		/// <summary>
		/// Gets or sets the audio player title.
		/// </summary>
		/// <value>The audio player title.</value>
		public string AudioPlayerTitle
		{
			get 
			{ 
				return audioPlayerTitle; 
			} 
			set 
			{ 
				if (!value.Equals(audioPlayerTitle))
				{
					audioPlayerTitle = value;
					RaisePropertyChanged (() => AudioPlayerTitle);
				}
			}
		}

		/// <summary>
		/// Gets or sets the exit title.
		/// </summary>
		/// <value>The exit title.</value>
		public string ExitTitle
		{
			get 
			{ 
				return exitTitle; 
			} 
			set 
			{ 
				if (!value.Equals(exitTitle))
				{
					exitTitle = value;
					RaisePropertyChanged (() => ExitTitle);
				}
			}
		}

		/// <summary>
		/// Gets or sets the audio player command.
		/// </summary>
		/// <value>The audio player command.</value>
		public MvxCommand AudioPlayerCommand
		{
			get
			{
				return audioPlayerCommand;
			}

			set
			{
				if (!value.Equals(audioPlayerCommand))
				{
					audioPlayerCommand = value;
					RaisePropertyChanged (() => AudioPlayerCommand);
				}
			}
		}

		/// <summary>
		/// Gets or sets the exit command.
		/// </summary>
		/// <value>The exit command.</value>
		public MvxCommand ExitCommand
		{
			get
			{
				return exitCommand;
			}

			set
			{
				if (!value.Equals(exitCommand))
				{
					exitCommand = value;
					RaisePropertyChanged (() => ExitCommand);
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Portable.ViewModels.MainPageViewModel"/> class.
		/// </summary>
		public MainPageViewModel ()
		{
			exitCommand = new MvxCommand (() =>
			{
				// exit the application
				Close(this);
			});

			audioPlayerCommand = new MvxCommand(() =>
			{
				ShowViewModel<AudioPlayerPageViewModel>();
			});
		}

		#endregion
	}
}