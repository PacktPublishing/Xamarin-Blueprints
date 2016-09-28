// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharedNetworkModule.cs" company="Flush Arcade Pty Ltd">
//   Copyright (c) 2015 Flush Arcade Pty Ltd All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Locator.Shared.Modules
{
	using System.Net;
	using System.Net.Http;

	using ModernHttpClient;

	using Autofac;

	using Locator.Portable.Ioc;

	/// <summary>
	/// Shared module.
	/// </summary>
	public sealed class SharedModule : IModule
	{
		#region Fields

		/// <summary>
		/// The is windows.
		/// </summary>
		private bool _isWindows;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Locator.Shared.Modules.SharedModule"/> class.
		/// </summary>
		/// <param name="isWindows">Is windows.</param>
		public SharedModule(bool isWindows)
		{
			_isWindows = isWindows;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">Builder.</param>
		public void Register(ContainerBuilder builder)
		{
			HttpClientHandler clientHandler = _isWindows ? new HttpClientHandler() : new NativeMessageHandler();
			clientHandler.UseCookies = false;
			clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			builder.Register(cb => clientHandler).As<HttpClientHandler>().SingleInstance();
		}
			
		#endregion
	}
}