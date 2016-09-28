// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorableExtensions.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.DataAccess.Storable
{
	using System.Reflection;
	using System.Linq;

	/// <summary>
	/// Storable extensions.
	/// </summary>
	public static class StorableExtensions
	{
		#region Public Methods

		/// <summary>
		/// Creates the insert or replace query.
		/// </summary>
		/// <returns>The insert or replace query.</returns>
		/// <param name="storable">Storable.</param>
		public static string CreateInsertOrReplaceQuery(this IStorable storable)
		{
			var properties = storable.GetType().GetRuntimeProperties();
			var tableName = storable.GetType().Name;

			string propetiesString = "";
			string propertyValuesString = "";

			var index = 0;

			foreach (var property in properties)
			{
				propetiesString += (index == (properties.Count() - 1)) ? property.Name : property.Name + ", ";

				var value = property.GetValue(storable);
				var valueString = value == null ? "null" : value is bool ? "'" + ((bool)value ? 1 : 0) + "'" : "'" + value + "'";

				// if data is serialized
				if (property.Name.Equals("Data") && !valueString.Equals("null"))
				{
					valueString = valueString.Replace("\"", "\\\"");
				}

				propertyValuesString += valueString + ((index == (properties.Count() - 1)) ? string.Empty : ", ");

				index++;
			}

			return string.Format("INSERT OR REPLACE INTO {0}({1}) VALUES ({2});",
								  tableName, propetiesString, propertyValuesString);
		}

		#endregion
	}
}