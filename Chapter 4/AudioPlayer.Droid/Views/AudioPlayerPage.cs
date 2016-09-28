// --------------------------------------------------------------------------------------------------
//  <copyright file="AudioPlayerPage.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid
{
	using Android.App;
	using Android.Graphics;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using MvvmCross.Droid.Views;

	using AudioPlayer.Droid.Controls;

	using AudioPlayer.Portable.ViewModels;

	/// <summary>
	/// Audio player page.
	/// </summary>
	[Activity(NoHistory = true)]
	public class AudioPlayerPage : MvxActivity
	{
		#region Private Properties

		/// <summary>
		/// The playing.
		/// </summary>
		private bool playing;

		/// <summary>
		/// The play button.
		/// </summary>
		private ImageButton playButton;

		/// <summary>
		/// The seek bar.
		/// </summary>
		private CustomSeekBar _seekBar;

		/// <summary>
		/// The model.
		/// </summary>
		private AudioPlayerPageViewModel model;

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called when activity is created.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.AudioPlayerPage);

			_seekBar = FindViewById<CustomSeekBar>(Resource.Id.seekBar);
			_seekBar.ValueChanged += HandleValueChanged;

			playButton = FindViewById<ImageButton>(Resource.Id.PlayButton);
			playButton.SetColorFilter(Color.White);
			playButton.Click += HandlePlayClick;

			var rewindButton = FindViewById<ImageButton>(Resource.Id.RewindButton);
			rewindButton.SetColorFilter(Color.White);
			rewindButton.Click += HandleRewindForwardClick;

			var forwardButton = FindViewById<ImageButton>(Resource.Id.ForwardButton);
			forwardButton.SetColorFilter(Color.White);
			forwardButton.Click += HandleRewindForwardClick;

			model = (AudioPlayerPageViewModel)ViewModel;
		}

		/// <summary>
		/// Called when activity is destroyed.
		/// </summary>
		/// <returns>The destroy.</returns>
		protected override void OnDestroy()
		{
			model.Dispose();

			base.OnDestroy();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the value changed.
		/// </summary>
		/// <returns>The value changed.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleValueChanged(object sender, System.EventArgs e)
		{
			model.UpdateAudioPosition(_seekBar.Progress);
		}

		/// <summary>
		/// Handles the play click.
		/// </summary>
		/// <returns>The play click.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandlePlayClick(object sender, System.EventArgs e)
		{
			playing = !playing;
			playButton.SetImageResource(playing ? Resource.Drawable.pause : Resource.Drawable.play);
		}

		/// <summary>
		/// Handles the rewind forward click.
		/// </summary>
		/// <returns>The rewind forward click.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleRewindForwardClick(object sender, System.EventArgs e)
		{
			playing = false;
			playButton.SetImageResource(Resource.Drawable.play);
		}

		#endregion
	}
}