// --------------------------------------------------------------------------------------------------
//  <copyright file="MainPageViewModel.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Portable.ViewModels
{
	using MvvmCross.Core.ViewModels;

	public class MainPageViewModel : MvxViewModel
	{
		#region Private Properties

		private string title = "Welcome";

		private string descriptionMessage = "Welcome to the Music Room";

		private string ChatTitle = "Audio Player";

		private string exitTitle = "Exit";

		private MvxCommand ChatCommand;

		private MvxCommand exitCommand;

		#endregion

		#region Public Properties

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

		public string ChatTitle
		{
			get 
			{ 
				return ChatTitle; 
			} 
			set 
			{ 
				if (!value.Equals(ChatTitle))
				{
					ChatTitle = value;
					RaisePropertyChanged (() => ChatTitle);
				}
			}
		}

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

		public MvxCommand ChatCommand
		{
			get
			{
				return this.ChatCommand;
			}

			set
			{
				if (!value.Equals(ChatCommand))
				{
					ChatCommand = value;
					RaisePropertyChanged (() => ChatCommand);
				}
			}
		}

		public MvxCommand ExitCommand
		{
			get
			{
				return this.exitCommand;
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

		public MainPageViewModel ()
		{
			this.exitCommand = new MvxCommand (() =>
			{
				// exit the application
				Close(this);
			});

			this.ChatCommand = new MvxCommand(() =>
			{
				ShowViewModel<ChatPageViewModel>();
			});
		}

		#endregion
	}
}