// --------------------------------------------------------------------------------------------------
//  <copyright file="SoundHandler.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS.Sound
{
	using AudioPlayer.Portable.Sound;
	using AVFoundation;
	using Foundation;

	/// <summary>
	/// Sound handler.
	/// </summary>
	public class SoundHandler : ISoundHandler
	{
		#region Private Properties

		/// <summary>
		/// The audio player.
		/// </summary>
		private AVAudioPlayer audioPlayer;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the is playing.
		/// </summary>
		/// <value>The is playing.</value>
		public bool IsPlaying { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Load the audio.
		/// </summary>
		public void Load()
		{
			audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename("Moby - The Only Thing.mp3"));
		}

		/// <summary>
		/// Play/Pauses the audio.
		/// </summary>
		/// <returns>The pause.</returns>
		public void PlayPause()
		{
			if (audioPlayer != null)
			{
				if (IsPlaying)
				{
					audioPlayer.Stop();
				}
				else
				{
					audioPlayer.Play();
				}

				IsPlaying = !IsPlaying;
			}
		}

		/// <summary>
		/// Stop this audio.
		/// </summary>
		public void Stop()
		{
			if (audioPlayer != null)
			{
				audioPlayer.Stop();
			}
		}

		/// <summary>
		/// Returns the audio duration.
		/// </summary>
		public double Duration()
		{
			if (audioPlayer != null)
			{
				return audioPlayer.Duration;
			}

			return 0;
		}

		/// <summary>
		/// Sets the audio position.
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="value">Value.</param>
		public void SetPosition(double value)
		{
			if (audioPlayer != null)
			{
				audioPlayer.CurrentTime = value;
			}
		}

		/// <summary>
		/// Returns current position of audio.
		/// </summary>
		/// <returns>The position.</returns>
		public double CurrentPosition()
		{
			if (audioPlayer != null)
			{
				return audioPlayer.CurrentTime;
			}

			return 0;
		}

		/// <summary>
		/// Fast forwards audio position.
		/// </summary>
		public void Forward()
		{
			if (audioPlayer != null)
			{
				IsPlaying = false;

				audioPlayer.Stop();
				audioPlayer.CurrentTime = audioPlayer.Duration;
			}
		}

		/// <summary>
		/// Rewind the audio position.
		/// </summary>
		public void Rewind()
		{
			if (audioPlayer != null)
			{
				IsPlaying = false;

				audioPlayer.Stop();
				audioPlayer.CurrentTime = 0;
			}
		}

		#endregion
	}
}