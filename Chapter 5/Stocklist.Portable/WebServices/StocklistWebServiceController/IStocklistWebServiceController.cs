// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeocodingRepository.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.WebServices.StocklistWebServiceController
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Stocklist.Portable.Repositories.StocklistWebServiceController.Contracts;

	/// <summary>
	/// The stock item web service controller interface.
	/// </summary>
	public interface IStocklistWebServiceController
	{
		#region Methods and Operators

		/// <summary>
		/// Gets the stock items.
		/// </summary>
		/// <returns>The stock items.</returns>
		IObservable<List<StockItemContract>> GetAllStockItems ();

		/// <summary>
		/// Gets the stock item.
		/// </summary>
		/// <returns>The stock item.</returns>
		/// <param name="id">Identifier.</param>
		IObservable<StockItemContract> GetStockItem(int id);

		#endregion
	}
}
