// --------------------------------------------------------------------------------------------------
//  <copyright file="SoundHandler.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid.Sound
{
	using System;
	using System.Diagnostics;

	using Android.Content.Res;
	using Android.Media;
	using Android.Content;

	using AudioPlayer.Portable.Sound;

	public class SoundHandler : ISoundHandler
	{
		#region Private Properties

		/// <summary>
		/// The media player.
		/// </summary>
		private MediaPlayer mediaPlayer;

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
			try
			{
				mediaPlayer = new MediaPlayer();
				mediaPlayer.SetAudioStreamType(Stream.Music);

				AssetFileDescriptor descriptor = Android.App.Application.Context.Assets.OpenFd("Moby - The Only Thing.mp3");
				mediaPlayer.SetDataSource(descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);

				mediaPlayer.Prepare();
				mediaPlayer.SetVolume(1f, 1f);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		/// <summary>
		/// Play/Pauses the audio.
		/// </summary>
		/// <returns>The pause.</returns>
		public void PlayPause()
		{
			if (mediaPlayer != null)
			{
				if (IsPlaying)
				{
					mediaPlayer.Pause();
				}
				else
				{
					mediaPlayer.Start();
				}

				IsPlaying = !IsPlaying;
			}
		}

		/// <summary>
		/// Stop this audio.
		/// </summary>
		public void Stop()
		{
			if (mediaPlayer != null)
			{
				mediaPlayer.Stop();
				mediaPlayer.Reset();
			}
		}

		/// <summary>
		/// Returns the audio duration.
		/// </summary>
		public double Duration()
		{
			if (mediaPlayer != null)
			{
				return mediaPlayer.Duration / 1000;
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
			if (mediaPlayer != null)
			{
				mediaPlayer.SeekTo((int)value * 1000);
			}
		}

		/// <summary>
		/// Returns current position of audio.
		/// </summary>
		/// <returns>The position.</returns>
		public double CurrentPosition()
		{
			if (mediaPlayer != null)
			{
				return mediaPlayer.CurrentPosition / 1000;
			}

			return 0;
		}

		/// <summary>
		/// Fast forwards audio position.
		/// </summary>
		public void Forward()
		{
			if (mediaPlayer != null)
			{
				IsPlaying = false;

				mediaPlayer.Pause();
				mediaPlayer.SeekTo(mediaPlayer.Duration);
			}
		}

		/// <summary>
		/// Rewind the audio position.
		/// </summary>
		public void Rewind()
		{
			if (mediaPlayer != null)
			{
				IsPlaying = false;

				mediaPlayer.Pause();
				mediaPlayer.SeekTo(0);
			}
		}

		#endregion
	}
}