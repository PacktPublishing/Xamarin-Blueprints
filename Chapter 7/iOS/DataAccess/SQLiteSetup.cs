// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLiteSetup.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.iOS.DataAccess
{
	using System.IO;
	using System;

	using SQLite.Net.Interop;

	using FileStorage.Portable.DataAccess;
	using FileStorage.Portable.DataAccess.Storage;

	/// <summary>
	/// The SQLite setup object.
	/// </summary>
	public class SQLiteSetup : ISQLiteSetup
	{
		public string DatabasePath { get; set; }

		public ISQLitePlatform Platform { get; set; }

		public SQLiteSetup(ISQLitePlatform platform)
		{
			DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "filestorage.db3");;
			Platform = platform;
		}
	}
}