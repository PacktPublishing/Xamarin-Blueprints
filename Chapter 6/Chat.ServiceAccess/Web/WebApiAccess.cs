// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApi.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.ServiceAccess.Web
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Chat.ServiceAccess.Web.Contracts;

	using Newtonsoft.Json;
	using Chat.Common.Model;

	/// <summary>
	/// Web API access.
	/// </summary>
	public class WebApiAccess
	{
		#region Public Properties

		/// <summary>
		/// The base address.
		/// </summary>
		private string _baseAddress = "http://10.0.0.128:52786/";

		#endregion

		#region Public Methods

		/// <summary>
		/// Logins the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="name">Name.</param>
		/// <param name="password">Password.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		public async Task<bool> LoginAsync(string name, string password, CancellationToken? cancellationToken = null)
		{
			var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseAddress + "api/Account/Login"))
			{
				Content = new StringContent(string.Format("Username={0}&Password={1}", name, password), Encoding.UTF8, "application/x-www-form-urlencoded"),
			};

			var client = new HttpClient();

			var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false));

			switch (response.StatusCode)
			{
				case HttpStatusCode.NotFound:
					throw new Exception(string.Empty);
			}

			var responseContent = await response.Content.ReadAsStringAsync();

			var loginSuccess = false;
			bool.TryParse(responseContent, out loginSuccess);
			return loginSuccess;
		}

		/// <summary>
		/// Registers the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="name">Name.</param>
		/// <param name="password">Password.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		public async Task<bool> RegisterAsync(string name, string password, CancellationToken? cancellationToken = null)
		{
			var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseAddress + "api/Account/Register"))
			{
				Content = new StringContent(string.Format("Username={0}&Password={1}", name, password), Encoding.UTF8, "application/x-www-form-urlencoded"),
			};

			var client = new HttpClient();

			var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false));

			return response.StatusCode == HttpStatusCode.OK;
		}

		/// <summary>
		/// Gets the token async.
		/// </summary>
		/// <returns>The token async.</returns>
		/// <param name="name">Name.</param>
		/// <param name="password">Password.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		public async Task<TokenContract> GetTokenAsync(string name, string password, CancellationToken? cancellationToken = null)
		{
			var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseAddress + "token"))
			{
				Content = new StringContent(string.Format("Username={0}&Password={1}&grant_type=password", name, password), Encoding.UTF8, "application/x-www-form-urlencoded"),
			};

			var client = new HttpClient();

			var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false));

			switch (response.StatusCode)
			{
				case HttpStatusCode.NotFound:
					throw new Exception(string.Empty);
			}

			var tokenJson = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TokenContract>(tokenJson); ;
		}

		/// <summary>
		/// Gets all connected users async.
		/// </summary>
		/// <returns>The all connected users async.</returns>
		/// <param name="cancellationToken">Cancellation token.</param>
		public async Task<IEnumerable<string>> GetAllConnectedUsersAsync(CancellationToken? cancellationToken = null)
		{
			var httpMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(_baseAddress + "api/Account/GetAllConnectedUsers"));

			var client = new HttpClient();

			var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false));

			switch (response.StatusCode)
			{
				case HttpStatusCode.NotFound:
					throw new Exception(string.Empty);
			}

			var responseContent = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<IEnumerable<string>>(responseContent);
		}

		#endregion
	}
}