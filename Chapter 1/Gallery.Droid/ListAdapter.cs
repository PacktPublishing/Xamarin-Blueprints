// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListAdapter.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using System.Collections.Generic;

	using Android.App;
	using Android.Widget;
	using Android.Views;

	using Gallery.Shared;

	/// <summary>
	/// List adapter.
	/// </summary>
	public class ListAdapter : BaseAdapter
	{
		#region Private Properties

		/// <summary>
		/// The items.
		/// </summary>
		private List<GalleryItem> items;

		/// <summary>
		/// The context.
		/// </summary>
		private Activity _context;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Gallery.Droid.ListAdapter"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ListAdapter(Activity context) : base()
		{
			_context = context;
			items = new List<GalleryItem>();

			foreach (var galleryitem in ImageHandler.GetFiles (context))
			{
				items.Add (galleryitem);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the item.
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="position">Position.</param>
		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		/// <summary>
		/// Gets the item by position.
		/// </summary>
		/// <returns>The item by position.</returns>
		/// <param name="position">Position.</param>
		public GalleryItem GetItemByPosition (int position)
		{
			return items[position];
		}

		/// <summary>
		/// Gets the item identifier.
		/// </summary>
		/// <returns>The item identifier.</returns>
		/// <param name="position">Position.</param>
		public override long GetItemId(int position)
		{
			return position;
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public override int Count
		{
			get 
			{ 
				return items.Count; 
			} 
		}

		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="position">Position.</param>
		/// <param name="convertView">Convert view.</param>
		/// <param name="parent">Parent.</param>
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available

			if (view == null)
			{ 
				// otherwise create a new one
				view = _context.LayoutInflater.Inflate(Resource.Layout.CustomCell, null);
			}

			// set image
			var imageView = view.FindViewById<ImageView> (Resource.Id.image);
			BitmapHelpers.CreateBitmap (imageView, items [position].ImageData);

			// set labels
			var titleTextView = view.FindViewById<TextView> (Resource.Id.title);
			titleTextView.Text = items[position].Title;
			var dateTextView = view.FindViewById<TextView> (Resource.Id.date);
			dateTextView.Text = items[position].Date;

			return view;
		}

		#endregion
	}
}