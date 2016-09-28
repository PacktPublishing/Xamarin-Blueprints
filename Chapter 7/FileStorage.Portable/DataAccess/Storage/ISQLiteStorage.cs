// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISQLiteSetup.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.DataAccess.Storage
{
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using FileStorage.Portable.DataAccess.Storable;

	/// <summary>
	/// SQLite storage.
	/// </summary>
	public interface ISQLiteStorage
	{
		#region Methods

		/// <summary>
		/// Creates the SQL ite async connection.
		/// </summary>
		/// <returns>The SQL ite async connection.</returns>
		void CreateSQLiteAsyncConnection();

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task CreateTable<T>() where T : class, IStorable, new();

		/// <summary>
		/// Drops the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task DropTable<T>() where T : class, IStorable, new();

		/// <summary>
		/// Inserts the object.
		/// </summary>
		/// <returns>The object.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task InsertObject<T>(T item) where T : class, IStorable, new();

		/// <summary>
		/// Gets the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task<IList<T>> GetTable<T>() where T : class, IStorable, new();

		/// <summary>
		/// Gets the object.
		/// </summary>
		/// <returns>The object.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task<T> GetObject<T>(string key) where T : class, IStorable, new();

		/// <summary>
		/// Removes all table objects.
		/// </summary>
		/// <returns>The all table objects.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task ClearTable<T>() where T : class, IStorable, new();

		/// <summary>
		/// Deletes the object.
		/// </summary>
		/// <returns>The object.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task DeleteObject<T>(T item);

		/// <summary>
		/// Deletes the object by key.
		/// </summary>
		/// <returns>The object by key.</returns>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task DeleteObjectByKey<T>(string key) where T : class, IStorable, new();

		/// <summary>
		/// Closes the connection.
		/// </summary>
		/// <returns>The connection.</returns>
		void CloseConnection();

		#endregion
	}
}