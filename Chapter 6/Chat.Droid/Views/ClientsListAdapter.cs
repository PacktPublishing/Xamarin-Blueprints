// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientsListAdapter.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Droid.Views
{
	using System.Collections.Generic;

	using Android.App;
	using Android.Widget;
	using Android.Views;

	using Chat.Common.Model;

	/// <summary>
	/// Clients list adapter.
	/// </summary>
	public class ClientsListAdapter : BaseAdapter<Client>
	{
		#region Private Properties

		/// <summary>
		/// The clients.
		/// </summary>
		private List<Client> _clients;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Droid.Views.ClientsListAdapter"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		private Activity _context;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Droid.Views.ClientsListAdapter"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ClientsListAdapter(Activity context) : base()
		{
			_context = context;
			_clients = new List<Client>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the <see cref="T:Chat.Droid.Views.ClientsListAdapter"/> with the specified position.
		/// </summary>
		/// <param name="position">Position.</param>
		public override Client this[int position]
		{
			get
			{
				return _clients[position];
			}
		}

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
				return _clients.Count; 
			} 
		}

		/// <summary>
		/// Updates the clients.
		/// </summary>
		/// <returns>The clients.</returns>
		/// <param name="clients">Clients.</param>
		public void UpdateClients(IEnumerable<Client> clients)
		{
			_clients.Clear();

			foreach (var client in clients)
			{
				_clients.Add(client);
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

			// set labels
			var connectionIdTextView = view.FindViewById<TextView> (Resource.Id.username);
			connectionIdTextView.Text = _clients[position].Username;

			return view;
		}

		#endregion
	}
}