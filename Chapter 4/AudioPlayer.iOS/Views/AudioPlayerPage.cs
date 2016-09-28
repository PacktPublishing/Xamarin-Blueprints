// --------------------------------------------------------------------------------------------------
//  <copyright file="AudioPlayerPage.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS.Views
{
	using System;
	using System.Linq;

	using Foundation;
	using UIKit;

	using MvvmCross.iOS.Views;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Binding.BindingContext;

	using AudioPlayer.Portable.ViewModels;
	using AudioPlayer.iOS.Extras;

	/// <summary>
	/// Main page.
	/// </summary>
	[MvxViewFor(typeof(AudioPlayerPageViewModel))]
	public class AudioPlayerPage : MvxViewController
	{
		#region Private Properties

		/// <summary>
		/// The play button.
		/// </summary>
		private UIButton playButton;

		/// <summary>
		/// The progress slider.
		/// </summary>
		private UISlider progressSlider;

		/// <summary>
		/// The playing.
		/// </summary>
		private bool playing;

		/// <summary>
		/// The model.
		/// </summary>
		private AudioPlayerPageViewModel model;

		#endregion

		#region Public Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mainView = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White
			};

			var buttonView = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.Clear
			};

			var imageView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = new UIImage("moby.png")
			};

			var descriptionLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Center
			};

			var currentLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Left,
			};

			var endLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Right,
			};

			progressSlider = new UISlider()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				MinValue = 0
			};

			progressSlider.ValueChanged += progressSliderValueChanged;

			playButton = new UIButton(UIButtonType.Custom)
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			playButton.TouchUpInside += handlePlayButton;
			playButton.SetImage(UIImage.FromFile("play.png"), UIControlState.Normal);

			var rewindButton = new UIButton(UIButtonType.Custom)
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			rewindButton.TouchUpInside += handleRewindForwardButton;
			rewindButton.SetImage(UIImage.FromFile("rewind.png"), UIControlState.Normal);

			var fastForwardButton = new UIButton(UIButtonType.Custom)
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
			fastForwardButton.TouchUpInside += handleRewindForwardButton;
			fastForwardButton.SetImage(UIImage.FromFile("fast_forward.png"), UIControlState.Normal);

			var views = new DictionaryViews()
			{
				{"mainView", mainView},
				{"buttonView", buttonView},
				{"imageView", imageView},
				{"descriptionLabel", descriptionLabel},
				{"currentLabel", currentLabel},
				{"endLabel", endLabel},
				{"progressSlider", progressSlider},
				{"playButton", playButton},
				{"rewindButton", rewindButton},
				{"fastForwardButton", fastForwardButton}
			};

			View.Add(mainView);

			mainView.Add(imageView);
			mainView.Add(descriptionLabel);
			mainView.Add(buttonView);
			mainView.Add(currentLabel);
			mainView.Add(endLabel);
			mainView.Add(progressSlider);

			buttonView.Add(playButton);
			buttonView.Add(rewindButton);
			buttonView.Add(fastForwardButton);

			View.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|[mainView]|", NSLayoutFormatOptions.DirectionLeftToRight, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|[mainView]|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.ToArray());

			mainView.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|-100-[imageView(200)]-[descriptionLabel(30)]-[buttonView(50)]-[currentLabel(30)]-[progressSlider]", NSLayoutFormatOptions.DirectionLeftToRight, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-100-[imageView(200)]-[descriptionLabel(30)]-[buttonView(50)]-[endLabel(30)]-[progressSlider]", NSLayoutFormatOptions.DirectionLeftToRight, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-20-[progressSlider]-20-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-25-[currentLabel(100)]", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:[endLabel(100)]-25-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-5-[descriptionLabel]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-5-[imageView]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(new[] { NSLayoutConstraint.Create(buttonView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.CenterX, 1, 0) })
				.ToArray());

			buttonView.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|-5-[rewindButton]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-5-[playButton]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-5-[fastForwardButton]-5-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-20-[rewindButton]-[playButton(100)]-[fastForwardButton]-20-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.ToArray());
			
			// create the binding set
			var set = this.CreateBindingSet<AudioPlayerPage, AudioPlayerPageViewModel>();
			set.Bind(this).For("Title").To(vm => vm.Title);
			set.Bind(descriptionLabel).To(vm => vm.DescriptionMessage);
			set.Bind(currentLabel).To(vm => vm.CurrentTimeStr);
			set.Bind(endLabel).To(vm => vm.EndTimeStr);
			set.Bind(progressSlider).For(v => v.Value).To(vm => vm.CurrentTime).TwoWay().Apply();
			set.Bind(progressSlider).For(v => v.MaxValue).To(vm => vm.EndTime);
			set.Bind(playButton).To(vm => vm.PlayPauseCommand);
			set.Bind(rewindButton).To(vm => vm.RewindCommand);
			set.Bind(fastForwardButton).To(vm => vm.ForwardCommand);
			set.Apply();

			model = (AudioPlayerPageViewModel)DataContext;
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <returns>The did appear.</returns>
		/// <param name="animated">Animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			model.Load();

			base.ViewDidAppear(animated);
		}

		/// <summary>
		/// Views the did disappear.
		/// </summary>
		/// <returns>The did disappear.</returns>
		/// <param name="animated">Animated.</param>
		public override void ViewDidDisappear(bool animated)
		{
			model.Dispose();

			base.ViewDidDisappear(animated);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Progresses the slider value changed.
		/// </summary>
		/// <returns>The slider value changed.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void progressSliderValueChanged(object sender, EventArgs e)
		{
			model.UpdateAudioPosition(progressSlider.Value);
		}

		/// <summary>
		/// Handles the touch up inside.
		/// </summary>
		/// <returns>The touch up inside.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void handlePlayButton(object sender, EventArgs e)
		{
			playing = !playing;
			playButton.SetImage(UIImage.FromFile(playing ? "pause.png" : "play.png"), UIControlState.Normal);
		}

		/// <summary>
		/// Handles the rewind forward button.
		/// </summary>
		/// <returns>The rewind forward button.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void handleRewindForwardButton(object sender, EventArgs e)
		{
			playing = false;
			playButton.SetImage(UIImage.FromFile("play.png"), UIControlState.Normal);
		}

		#endregion
	}
}