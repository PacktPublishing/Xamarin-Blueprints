// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientsTableSource.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.iOS 
{
	using System;
	using System.Collections.Generic;

	using UIKit;
	using Foundation;

	using Chat.Common.Model;

	/// <summary>
	/// Clients table source.
	/// </summary>
	public class ClientsTableSource : UITableViewSource 
	{
		#region Public Properties

		/// <summary>
		/// Occurs when item selected.
		/// </summary>
		public event EventHandler<Client> ItemSelected;

		#endregion

		#region Private Properties

		/// <summary>
		/// The clients.
		/// </summary>
		private List<Client> _clients;

		/// <summary>
		/// The cell identifier.
		/// </summary>
		string CellIdentifier = "ClientCell";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.iOS.ClientsTableSource"/> class.
		/// </summary>
		public ClientsTableSource ()
		{
			_clients = new List<Client>();
		}

		#endregion

		#region Public Methods

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
				_clients.Add (client);
			}
		}

		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <returns>The of sections.</returns>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _clients.Count;
		}

		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <returns>The selected.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (ItemSelected != null)
			{
				ItemSelected (this, _clients[indexPath.Row]);
			}

			tableView.DeselectRow (indexPath, true);
		}

		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80;
		}

		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

			if (indexPath.Row < _clients.Count)
			{
				var client = _clients[indexPath.Row];

				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
				}

				cell.TextLabel.Text = client.Username;
			}

			return cell;
		}

		#endregion
	}
}