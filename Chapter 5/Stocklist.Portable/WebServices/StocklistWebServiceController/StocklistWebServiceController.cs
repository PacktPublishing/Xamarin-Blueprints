// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StocklistRepository.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.WebServices.StocklistWebServiceController
{
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Reactive.Linq;
	using System.Reactive.Subjects;
	using System.Reactive.Threading.Tasks;
	using System.Text;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Threading;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using Stocklist.Portable.Repositories.StocklistWebServiceController.Contracts;
	using Stocklist.Portable.Resources;

	/// <summary>
	/// Stocklist web service controller.
	/// </summary>
	public sealed class StocklistWebServiceController : IStocklistWebServiceController
	{
		#region Fields

		/// <summary>
		/// The client handler.
		/// </summary>
		private readonly HttpClientHandler _clientHandler;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Gets all stock items.
		/// </summary>
		/// <returns>The all stock items.</returns>
		public IObservable<List<StockItemContract>> GetAllStockItems ()
		{
			var authClient = new HttpClient (_clientHandler);

			var message = new HttpRequestMessage (HttpMethod.Get, new Uri (Config.ApiAllItems));

			return Observable.FromAsync(() => authClient.SendAsync (message, new CancellationToken(false)))
				.SelectMany(async response => 
					{
						if (response.StatusCode != HttpStatusCode.OK)
						{
							throw new Exception("Respone error");
						}

						return await response.Content.ReadAsStringAsync();
					})
				.Select(json => JsonConvert.DeserializeObject<List<StockItemContract>>(json));
		}

		/// <summary>
		/// Gets the stock item.
		/// </summary>
		/// <returns>The stock item.</returns>
		/// <param name="id">Identifier.</param>
		public IObservable<StockItemContract> GetStockItem(int id)
		{
			var authClient = new HttpClient(_clientHandler);

			var message = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(Config.GetStockItem, id)));

			return Observable.FromAsync(() => authClient.SendAsync(message, new CancellationToken(false)))
				.SelectMany(async response =>
					{
						if (response.StatusCode != HttpStatusCode.OK)
						{
							throw new Exception("Respone error");
						}

						return await response.Content.ReadAsStringAsync();
					})
				.Select(json => JsonConvert.DeserializeObject<StockItemContract>(json));
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Stocklist.Portable.Repositories.StocklistWebServiceController.GeocodingRepository"/> class.
		/// </summary>
		/// <param name="clientHandler">Client handler.</param>
		public StocklistWebServiceController(HttpClientHandler clientHandler)
		{
			_clientHandler = clientHandler;
		}

		#endregion
	}
}