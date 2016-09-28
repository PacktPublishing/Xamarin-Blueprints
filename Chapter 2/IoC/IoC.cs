// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.Ioc
{
	using System.Collections.Generic;

	using Autofac;

	/// <summary>
	/// The IoC Container.
	/// </summary>
	public static class IoC
	{
		#region Public Static Properties

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>
		public static IContainer Container { get; private set; }

		#endregion

		#region Private Static Properties

		/// <summary>
		/// The builder.
		/// </summary>
		private static ContainerBuilder builder;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates the container.
		/// </summary>
		/// <returns>The container.</returns>
		public static void CreateContainer() 
		{
			builder = new ContainerBuilder();
		}

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Starts the container.
		/// </summary>
		/// <returns>The container.</returns>
		public static void StartContainer()
		{
			Container = builder.Build();
		}

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <returns>The module.</returns>
		/// <param name="module">Module.</param>
		public static void RegisterModule(IModule module)
		{
			module.Register (builder);
		}

		/// <summary>
		/// Registers the modules.
		/// </summary>
		/// <returns>The modules.</returns>
		/// <param name="modules">Modules.</param>
		public static void RegisterModules(IEnumerable<IModule> modules)
		{
			foreach (var module in modules) 
			{
				module.Register (builder);
			}
		}

		/// <summary>
		/// Resolve this instance.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Resolve<T>()
		{
			return Container.Resolve<T> ();
		}

		#endregion
	}
}