// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using Android.App;
	using Android.Widget;
	using Android.OS;
	using Android.Content;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "Gallery.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		#region Private Properties

		/// <summary>
		/// The adapter.
		/// </summary>
		private ListAdapter adapter;

		#endregion

		#region Protected Methods

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			adapter = new ListAdapter (this);

			var listView = FindViewById<ListView> (Resource.Id.listView);
			listView.Adapter = adapter;

			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => 
			{
				var galleryItem = adapter.GetItemByPosition (e.Position);
				var photoActivity = new Intent(this, typeof(PhotoActivity));
				photoActivity.PutExtra ("ImageData", galleryItem.ImageData);
				photoActivity.PutExtra ("Title", galleryItem.Title);
				photoActivity.PutExtra ("Date", galleryItem.Date);
				StartActivity(photoActivity);
			};
		}

		#endregion
	}
}