// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Portable.Ioc
{
	using Autofac;

	/// <summary>
	/// Module.
	/// </summary>
	public interface IModule
	{
		#region Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
		void Register(ContainerBuilder builder);

		#endregion
	}
}