// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockItemViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.ViewModels
{
	using Stocklist.Portable.Enums;
	using Stocklist.Portable.UI;
	using Stocklist.Portable.Extras;

	using Stocklist.Portable.Repositories.StocklistWebServiceController.Contracts;

	/// <summary>
	/// Stock item view model.
	/// </summary>
	public class StockItemViewModel : ViewModelBase
	{
		#region Private Properties

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

		#endregion

		#region Public Methods

		/// <summary>
		/// Apply the specified contract.
		/// </summary>
		/// <param name="contract">Contract.</param>
		public void Apply(StockItemContract contract)
		{
			Id = contract.Id;
			Name = contract.Name;
			Category = contract.Category;
			Price = contract.Price;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.Portable.ViewModels.StockItemViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		public StockItemViewModel (INavigationService navigation) : base (navigation)
		{
		}

		#endregion
	}
}

