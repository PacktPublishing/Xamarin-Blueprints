// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StocklistPageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.ViewModels
{
	using System;
	using System.Reactive.Linq;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;

	using Stocklist.Portable.UI;
	using Stocklist.Portable.WebServices.StocklistWebServiceController;

	/// <summary>
	/// Stocklist page view model.
	/// </summary>
	public class StocklistPageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The stocklist repository.
		/// </summary>
		private readonly IStocklistWebServiceController _stocklistWebServiceController;

		/// <summary>
		/// The stock item factory.
		/// </summary>
		private readonly Func<StockItemViewModel> _stockItemFactory;

		/// <summary>
		/// The selected.
		/// </summary>
		private StockItemViewModel _selected;

		/// <summary>
		/// The in progress.
		/// </summary>
		private bool _inProgress;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the stock items.
		/// </summary>
		/// <value>The stock items.</value>
		public ObservableCollection<StockItemViewModel> StockItems { get; set; }

		/// <summary>
		/// Gets or sets the selected.
		/// </summary>
		/// <value>The selected.</value>
		public StockItemViewModel Selected
		{
			get
			{
				return _selected;
			}

			set
			{
				if (value.Equals(_selected))
				{
					return;
				}
				else
				{
					Navigation.Navigate(Enums.PageNames.StockItemDetailsPage, new Dictionary<string, object>()
					{
						{"id", value.Id},
					}).ConfigureAwait(false);
				}

				_selected = value;
				OnPropertyChanged("Selected");
			}
		}

		/// <summary>
		/// Gets or sets the in progress.
		/// </summary>
		/// <value>The in progress.</value>
		public bool InProgress
		{
			get
			{
				return _inProgress;
			}

			set
			{
				if (value.Equals(_inProgress))
				{
					return;
				}

				_inProgress = value;
				OnPropertyChanged("InProgress");
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Loads the view-model.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="parameters">Parameters.</param>
		protected override async Task LoadAsync(IDictionary<string, object> parameters)
		{
			try
			{
				InProgress = true;

				// reset the list everytime we load the page
				StockItems.Clear();

				var stockItems = await _stocklistWebServiceController.GetAllStockItems();

				// for all contracts build stock item view model and add to the observable collection
				foreach (var model in stockItems.Select(x =>
					{
						var model = _stockItemFactory();
						model.Apply(x);
						return model;
					}))
				{
					StockItems.Add(model);
				}

				InProgress = false;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.Portable.ViewModels.StocklistPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="stocklistWebServiceController">Stocklist repository.</param>
		/// <param name="stockItemFactory">Stock item factory.</param>
		public StocklistPageViewModel(INavigationService navigation, IStocklistWebServiceController stocklistWebServiceController,
			Func<StockItemViewModel> stockItemFactory) : base(navigation)
		{
			_stockItemFactory = stockItemFactory;

			_stocklistWebServiceController = stocklistWebServiceController;

			StockItems = new ObservableCollection<StockItemViewModel>();
		}

		#endregion
	}
}