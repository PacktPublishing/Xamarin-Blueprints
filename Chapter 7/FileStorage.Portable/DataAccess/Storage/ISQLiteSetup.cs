// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISQLiteSetup.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.DataAccess.Storage
{
	using SQLite.Net.Interop;

	/// <summary>
	/// The SQLite setup object.
	/// </summary>
	public interface ISQLiteSetup
	{
		#region Properties

		/// <summary>
		/// Gets or sets the database path.
		/// </summary>
		/// <value>The database path.</value>
		string DatabasePath { get; set; }

		/// <summary>
		/// Gets or sets the platform.
		/// </summary>
		/// <value>The platform.</value>
		ISQLitePlatform Platform { get; set; }

		#endregion
	}
}