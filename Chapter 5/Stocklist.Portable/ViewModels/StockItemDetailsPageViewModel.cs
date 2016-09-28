// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StocklistPageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.ViewModels
{
	using System;
	using System.Reactive.Linq;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Windows.Input;

	using Stocklist.Portable.UI;
	using Stocklist.Portable.WebServices.StocklistWebServiceController;

	/// <summary>
	/// Stocklist page view model.
	/// </summary>
	public class StockItemDetailsPageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The stocklist repository.
		/// </summary>
		private readonly IStocklistWebServiceController _stocklistWebServiceController;

		/// <summary>
		/// The identifier.
		/// </summary>
		private int _id;

		/// <summary>
		/// The name.
		/// </summary>
		private string _name;

		/// <summary>
		/// The category.
		/// </summary>
		private string _category;

		/// <summary>
		/// The price.
		/// </summary>
		private decimal _price;

		/// <summary>
		/// The in progress.
		/// </summary>
		private bool _inProgress;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id
		{
			get
			{
				return _id;
			}

			set
			{
				if (value.Equals(_id))
				{
					return;
				}

				_id = value;
				OnPropertyChanged("Id");
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return _name;
			}

			set
			{
				if (value.Equals(_name))
				{
					return;
				}

				_name = value;
				OnPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>The category.</value>
		public string Category
		{
			get
			{
				return _category;
			}

			set
			{
				if (value.Equals(_category))
				{
					return;
				}

				_category = value;
				OnPropertyChanged("Category");
			}
		}

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>The price.</value>
		public decimal Price
		{
			get
			{
				return _price;
			}

			set
			{
				if (value.Equals(_price))
				{
					return;
				}

				_price = value;
				OnPropertyChanged("Price");
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
			InProgress = true;

			if (parameters.ContainsKey("id"))
			{
				Id = (int)parameters["id"];
			}

			var contract = await _stocklistWebServiceController.GetStockItem(Id);

			if (contract != null)
			{
				Name = contract.Name;
				Category = contract.Category;
				Price = contract.Price;
			}

			InProgress = false;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.Portable.ViewModels.StockItemDetailsPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="stocklistWebServiceController">Stocklist repository.</param>
		/// <param name="commandFactory">Command factory.</param>
		public StockItemDetailsPageViewModel(INavigationService navigation, IStocklistWebServiceController stocklistWebServiceController,
			Func<Action, ICommand> commandFactory) : base(navigation)
		{
			_stocklistWebServiceController = stocklistWebServiceController;
		}

		#endregion
	}
}