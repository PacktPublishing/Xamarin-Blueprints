// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLiteSetup.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Droid.DataAccess
{
	using System;
	using System.IO;

	using SQLite.Net.Interop;

	using FileStorage.Portable.DataAccess.Storage;

	/// <summary>
	/// The SQLite setup object.
	/// </summary>
	public class SQLiteSetup : ISQLiteSetup
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the database path.
		/// </summary>
		/// <value>The database path.</value>
		public string DatabasePath { get; set; }

		/// <summary>
		/// Gets or sets the platform.
		/// </summary>
		/// <value>The platform.</value>
		public ISQLitePlatform Platform { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Droid.DataAccess.SQLiteSetup"/> class.
		/// </summary>
		/// <param name="platform">Platform.</param>
		public SQLiteSetup(ISQLitePlatform platform)
		{
			DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "filestorage.db3"); ;
			Platform = platform;
		}

		#endregion
	}
}