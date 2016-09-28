// --------------------------------------------------------------------------------------------------
//  <copyright file="MainPage.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS.Views
{
	using System;

	using Foundation;
	using UIKit;

	using MvvmCross.iOS.Views;
	using MvvmCross.Core.ViewModels;
	using MvvmCross.Binding.BindingContext;

	using AudioPlayer.Portable.ViewModels;

	/// <summary>
	/// Main page.
	/// </summary>
	[MvxViewFor(typeof(MainPageViewModel))]
	public class MainPage : MvxViewController
	{
		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var mainView = new UIView () 
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White
			};

			var imageView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = new UIImage("audio.png")
			};

			var descriptionLabel = new UILabel () 
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Center
			};
				
			var audioPlayerButton = new UIButton (UIButtonType.RoundedRect) 
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			var exitButton = new UIButton (UIButtonType.RoundedRect) 
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			View.Add (mainView);

			// add buttons to the main view
			mainView.Add (imageView);
			mainView.Add (descriptionLabel);
			mainView.Add (audioPlayerButton);
			mainView.Add (exitButton);

			View.AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[mainView]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("mainView", mainView)));
			View.AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|[mainView]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("mainView", mainView)));

			mainView.AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|-100-[imageView(200)]-50-[descriptionLabel]-50-[audioPlayerButton]-[exitButton]", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("imageView", imageView, "descriptionLabel", descriptionLabel, "audioPlayerButton", audioPlayerButton, "exitButton", exitButton)));
			mainView.AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-5-[imageView]-5-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary("imageView", imageView)));
			mainView.AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-5-[descriptionLabel]-5-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("descriptionLabel", descriptionLabel)));
			mainView.AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-5-[audioPlayerButton]-5-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("audioPlayerButton", audioPlayerButton)));
			mainView.AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-5-[exitButton]-5-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("exitButton", exitButton)));

			// create the binding set
			var set = this.CreateBindingSet<MainPage, MainPageViewModel> ();
			set.Bind(this).For("Title").To(vm => vm.Title);
			set.Bind(descriptionLabel).To(vm => vm.DescriptionMessage);
			set.Bind(audioPlayerButton).For("Title").To(vm => vm.AudioPlayerTitle);
			set.Bind(audioPlayerButton).To(vm => vm.AudioPlayerCommand);
			set.Bind(exitButton).For("Title").To(vm => vm.ExitTitle);
			set.Bind(exitButton).To(vm => vm.ExitCommand);
			set.Apply ();
		}
	}
}


