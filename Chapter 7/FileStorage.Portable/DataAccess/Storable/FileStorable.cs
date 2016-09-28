// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileStorable.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.DataAccess.Storable
{
	using Newtonsoft.Json;

	using SQLite.Net.Attributes;

	/// <summary>
	/// File storable.
	/// </summary>
	public class FileStorable : IStorable
    {
        #region Public Properties

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
      	[PrimaryKey]
		public string Key { get; set; }

		/// <summary>
		/// Gets or sets the contents.
		/// </summary>
		/// <value>The contents.</value>
		public string Contents { get; set; }

        #endregion
    }
}