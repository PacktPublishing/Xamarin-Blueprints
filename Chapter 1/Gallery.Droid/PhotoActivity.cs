// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhotoActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using Android.App;
	using Android.Widget;
	using Android.OS;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "Gallery.Droid", Icon = "@drawable/icon")]
	public class PhotoActivity : Activity
	{
		#region Protected Methods

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Photo);

			var imageData = Intent.GetByteArrayExtra ("ImageData");
			var title = Intent.GetStringExtra ("Title") ?? string.Empty;
			var date = Intent.GetStringExtra ("Date") ?? string.Empty;

			// set image
			var imageView = FindViewById<ImageView> (Resource.Id.image_photo);
			BitmapHelpers.CreateBitmap (imageView, imageData);

			// set labels
			var titleTextView = FindViewById<TextView> (Resource.Id.title_photo);
			titleTextView.Text = title;
			var dateTextView = FindViewById<TextView> (Resource.Id.date_photo);
			dateTextView.Text = date;
		}

		#endregion
	}
}