// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarouselView.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Controls
{
	using System;
	using System.Windows.Input;

	using Xamarin.Forms;

	/// <summary>
	/// Carousel view.
	/// </summary>
	public partial class CarouselView : ContentView
	{
		#region Private Properties

		/// <summary>
		/// The animating.
		/// </summary>
		private bool _animating;

		#endregion

		#region Public Properties

		/// <summary>
		/// The index of the selected.
		/// </summary>
		public int SelectedIndex = 0;

		/// <summary>
		/// The selected command property.
		/// </summary>
		public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create<CarouselView, ICommand>(w => w.SelectedCommand, default(ICommand),
				propertyChanged: (bindable, oldvalue, newvalue) => { });

		/// <summary>
		/// Gets or sets the selected command.
		/// </summary>
		/// <value>The selected command.</value>
		public ICommand SelectedCommand
		{
			get { return (ICommand)GetValue(SelectedCommandProperty); }
			set { SetValue(SelectedCommandProperty, value); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Controls.CarouselView"/> class.
		/// </summary>
		public CarouselView()
		{
			InitializeComponent();

			GestureView.SwipeLeft += HandleSwipeLeft;
			GestureView.SwipeRight += HandleSwipeRight;
			GestureView.Touch += HandleTouch;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles the touch.
		/// </summary>
		/// <returns>The touch.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void HandleTouch(object sender, EventArgs e)
		{
			if (SelectedCommand != null && !_animating)
			{
				var cell = CarouselScroll.GetSelectedItem(SelectedIndex);
				SelectedCommand.Execute(cell);
			}
		}

		/// <summary>
		/// Handles the swipe left.
		/// </summary>
		/// <returns>The swipe left.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void HandleSwipeLeft(object sender, EventArgs e)
		{
			if (((CarouselScroll.ScrollX + CarouselScroll.Width) < (CarouselScroll.Content.Width - CarouselScroll.Width)) && !_animating)
			{
				_animating = true;

				SelectedIndex++;
				await CarouselScroll.ScrollToAsync(CarouselScroll.ScrollX + Width + 20, 0, true);

				_animating = false;
			}
		}

		/// <summary>
		/// Handles the swipe right.
		/// </summary>
		/// <returns>The swipe right.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void HandleSwipeRight(object sender, EventArgs e)
		{
			if (CarouselScroll.ScrollX > 0 && !_animating)
			{
				_animating = true;

				SelectedIndex--;
				await CarouselScroll.ScrollToAsync(CarouselScroll.ScrollX - Width - 20, 0, true);

				_animating = false;
			}
		}

		#endregion
	}
}