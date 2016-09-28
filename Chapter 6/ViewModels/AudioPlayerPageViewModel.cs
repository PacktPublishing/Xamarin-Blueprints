// --------------------------------------------------------------------------------------------------
//  <copyright file="ChatPageViewModel.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Portable.ViewModels
{
	using MvvmCross.Core.ViewModels;

	using System;
	using System.Threading.Tasks;
	using System.Threading;

	using Chat.Portable.Sound;

	public class ChatPageViewModel : MvxViewModel
	{
		#region Private Properties

		private readonly ISoundHandler soundHandler;

		private string title = "Audio Player";

		private string descriptionMessage = "Moby - The Only Thing";

		private MvxCommand playPauseCommand;

		private MvxCommand forwardCommand;

		private MvxCommand rewindCommand;

		private float audioPosition;

		private double currentTime;

		private double endTime;

		private bool updating;

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

		public MvxCommand PlayPauseCommand
		{
			get
			{
				return this.playPauseCommand;
			}

			set
			{
				if (!value.Equals(playPauseCommand))
				{
					playPauseCommand = value;
					RaisePropertyChanged (() => PlayPauseCommand);
				}
			}
		}

		public MvxCommand RewindCommand
		{
			get
			{
				return this.rewindCommand;
			}

			set
			{
				if (!value.Equals(rewindCommand))
				{
					rewindCommand = value;
					RaisePropertyChanged(() => RewindCommand);
				}
			}
		}

		public MvxCommand ForwardCommand
		{
			get
			{
				return this.forwardCommand;
			}

			set
			{
				if (!value.Equals(forwardCommand))
				{
					forwardCommand = value;
					RaisePropertyChanged(() => ForwardCommand);
				}
			}
		}

		public float AudioPosition
		{
			get
			{
				return this.audioPosition;
			}

			set
			{
				if (!value.Equals(audioPosition))
				{
					audioPosition = value;
					RaisePropertyChanged(() => AudioPosition);
				}
			}
		}

		public string CurrentTimeStr
		{
			get
			{
				return TimeSpan.FromSeconds(this.CurrentTime).ToString("mm\\:ss");
			}
		}

		public double CurrentTime
		{
			get
			{
				return this.currentTime;
			}

			set
			{
				if (!value.Equals(currentTime))
				{
					currentTime = value;
					RaisePropertyChanged(() => CurrentTime);
					// everytime we change the current time, the time span values must also update
					RaisePropertyChanged(() => CurrentTimeStr);
				}
			}
		}

		public string EndTimeStr
		{
			get
			{
				return TimeSpan.FromSeconds(this.EndTime).ToString("mm\\:ss");
			}
		}

		public double EndTime
		{
			get
			{
				return this.endTime;
			}

			set
			{
				if (!value.Equals(endTime))
				{
					endTime = value;
					RaisePropertyChanged(() => EndTime);
					RaisePropertyChanged(() => EndTimeStr);
				}
			}
		}

		#endregion

		#region Constructors

		public ChatPageViewModel (ISoundHandler soundHandler)
		{
			this.soundHandler = soundHandler;

			// load sound file
			this.soundHandler.Load();

			this.EndTime = this.soundHandler.Duration();

			this.playPauseCommand = new MvxCommand(() =>
			{
				// start/stop UI updates if the audio is not playing
				if (soundHandler.IsPlaying)
				{
					this.updating = false;
				}
				else
				{
					this.Load();
				}

				soundHandler.PlayPause();
			});

			this.rewindCommand = new MvxCommand(() =>
			{
				// set current time to the beginning
				this.CurrentTime = 0;
				this.soundHandler.Rewind();
				this.updating = false;
			});

			this.forwardCommand = new MvxCommand(() =>
			{
				// set current time to the end
				this.CurrentTime = this.soundHandler.Duration();
				this.soundHandler.Forward();
				this.updating = false;
			});
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Updates the audio position.
		/// </summary>
		/// <returns>The audio position.</returns>
		/// <param name="value">Value.</param>
		public void UpdateAudioPosition(double value)
		{
			this.soundHandler.SetPosition(value);
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Load()
		{
			// make sure we only start the loop once
			if (!this.updating)
			{
				this.updating = true;

				// we are going to post a regular update to the UI witht he current time
				var context = SynchronizationContext.Current;

				Task.Run(async () =>
				{
					while (this.updating)
					{
						await Task.Delay(1000);

						context.Post(unused =>
						{
							var current = this.soundHandler.CurrentPosition(); ;

							if (current > 0)
							{
								this.CurrentTime = current;
							}

						}, null);
					}
				});
			}
		}

		/// <summary>
		/// Stops the updating.
		/// </summary>
		/// <returns>The updating.</returns>
		public void Dispose()
		{
			this.updating = false;
			this.soundHandler.Stop();
		}

		#endregion
	}
}