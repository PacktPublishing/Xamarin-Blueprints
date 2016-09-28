// --------------------------------------------------------------------------------------------------
//  <copyright file="AudioPlayerPageViewModel.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Portable.ViewModels
{
	using MvvmCross.Core.ViewModels;

	using System;
	using System.Threading.Tasks;
	using System.Threading;

	using AudioPlayer.Portable.Sound;

	/// <summary>
	/// Audio player page view model.
	/// </summary>
	public class AudioPlayerPageViewModel : MvxViewModel
	{
		#region Private Properties

		/// <summary>
		/// The sound handler.
		/// </summary>
		private readonly ISoundHandler _soundHandler;

		/// <summary>
		/// The title.
		/// </summary>
		private string _title = "Audio Player";

		/// <summary>
		/// The description message.
		/// </summary>
		private string _descriptionMessage = "Moby - The Only Thing";

		/// <summary>
		/// The play pause command.
		/// </summary>
		private MvxCommand _playPauseCommand;

		/// <summary>
		/// The forward command.
		/// </summary>
		private MvxCommand _forwardCommand;

		/// <summary>
		/// The rewind command.
		/// </summary>
		private MvxCommand _rewindCommand;

		/// <summary>
		/// The audio position.
		/// </summary>
		private float _audioPosition;

		/// <summary>
		/// The current time.
		/// </summary>
		private double _currentTime;

		/// <summary>
		/// The end time.
		/// </summary>
		private double _endTime;

		/// <summary>
		/// The updating.
		/// </summary>
		private bool _updating;

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
				return _title;
			}
			set
			{
				if (!value.Equals(_title))
				{
					_descriptionMessage = value;
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
				return _descriptionMessage; 
			} 
			set 
			{ 
				if (!value.Equals(_descriptionMessage))
				{
					_descriptionMessage = value;
					RaisePropertyChanged (() => DescriptionMessage);
				}
			}
		}

		/// <summary>
		/// Gets or sets the play pause command.
		/// </summary>
		/// <value>The play pause command.</value>
		public MvxCommand PlayPauseCommand
		{
			get
			{
				return _playPauseCommand;
			}

			set
			{
				if (!value.Equals(_playPauseCommand))
				{
					_playPauseCommand = value;
					RaisePropertyChanged (() => PlayPauseCommand);
				}
			}
		}

		/// <summary>
		/// Gets or sets the rewind command.
		/// </summary>
		/// <value>The rewind command.</value>
		public MvxCommand RewindCommand
		{
			get
			{
				return _rewindCommand;
			}

			set
			{
				if (!value.Equals(_rewindCommand))
				{
					_rewindCommand = value;
					RaisePropertyChanged(() => RewindCommand);
				}
			}
		}

		/// <summary>
		/// Gets or sets the forward command.
		/// </summary>
		/// <value>The forward command.</value>
		public MvxCommand ForwardCommand
		{
			get
			{
				return _forwardCommand;
			}

			set
			{
				if (!value.Equals(_forwardCommand))
				{
					_forwardCommand = value;
					RaisePropertyChanged(() => ForwardCommand);
				}
			}
		}

		/// <summary>
		/// Gets or sets the audio position.
		/// </summary>
		/// <value>The audio position.</value>
		public float AudioPosition
		{
			get
			{
				return _audioPosition;
			}

			set
			{
				if (!value.Equals(_audioPosition))
				{
					_audioPosition = value;
					RaisePropertyChanged(() => AudioPosition);
				}
			}
		}

		/// <summary>
		/// Gets the current time string.
		/// </summary>
		/// <value>The current time string.</value>
		public string CurrentTimeStr
		{
			get
			{
				return TimeSpan.FromSeconds(CurrentTime).ToString("mm\\:ss");
			}
		}

		/// <summary>
		/// Gets or sets the current time.
		/// </summary>
		/// <value>The current time.</value>
		public double CurrentTime
		{
			get
			{
				return _currentTime;
			}

			set
			{
				if (!value.Equals(_currentTime))
				{
					_currentTime = value;
					RaisePropertyChanged(() => CurrentTime);
					// everytime we change the current time, the time span values must also update
					RaisePropertyChanged(() => CurrentTimeStr);
				}
			}
		}

		/// <summary>
		/// Gets the end time string.
		/// </summary>
		/// <value>The end time string.</value>
		public string EndTimeStr
		{
			get
			{
				return TimeSpan.FromSeconds(EndTime).ToString("mm\\:ss");
			}
		}

		/// <summary>
		/// Gets or sets the end time.
		/// </summary>
		/// <value>The end time.</value>
		public double EndTime
		{
			get
			{
				return _endTime;
			}

			set
			{
				if (!value.Equals(_endTime))
				{
					_endTime = value;
					RaisePropertyChanged(() => EndTime);
					RaisePropertyChanged(() => EndTimeStr);
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Portable.ViewModels.AudioPlayerPageViewModel"/> class.
		/// </summary>
		/// <param name="soundHandler">Sound handler.</param>
		public AudioPlayerPageViewModel (ISoundHandler soundHandler)
		{
			_soundHandler = soundHandler;

			// load sound file
			soundHandler.Load();

			EndTime = soundHandler.Duration();

			_playPauseCommand = new MvxCommand(() =>
			{
				// start/stop UI updates if the audio is not playing
				if (soundHandler.IsPlaying)
				{
					_updating = false;
				}
				else
				{
					Load();
				}

				soundHandler.PlayPause();
			});

			_rewindCommand = new MvxCommand(() =>
			{
				// set current time to the beginning
				CurrentTime = 0;
				soundHandler.Rewind();
				_updating = false;
			});

			_forwardCommand = new MvxCommand(() =>
			{
				// set current time to the end
				CurrentTime = soundHandler.Duration();
				soundHandler.Forward();
				_updating = false;
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
			_soundHandler.SetPosition(value);
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Load()
		{
			// make sure we only start the loop once
			if (!_updating)
			{
				_updating = true;

				// we are going to post a regular update to the UI witht he current time
				var context = SynchronizationContext.Current;

				Task.Run(async () =>
				{
					while (_updating)
					{
						await Task.Delay(1000);

						context.Post(unused =>
						{
							var current = _soundHandler.CurrentPosition(); ;

							if (current > 0)
							{
								CurrentTime = current;
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
			_updating = false;
			_soundHandler.Stop();
		}

		#endregion
	}
}