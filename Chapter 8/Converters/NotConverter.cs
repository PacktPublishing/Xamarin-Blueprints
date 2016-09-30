// --------------------------------------------------------------------------------------------------
//  <copyright file="NotConverter.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Converters
{
	using System;

	using Xamarin.Forms;

	/// <summary>
	/// Not converter.
	/// </summary>
	public class NotConverter : IValueConverter
	{
		#region Public Methods

		/// <summary>
		/// Convert the specified value, targetType, parameter and culture.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="culture">Culture.</param>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var b = value as bool?;

			if (b != null)
			{
				return !b;
			}

			return value;
		}

		/// <summary>
		/// Converts the back.
		/// </summary>
		/// <returns>The back.</returns>
		/// <param name="value">Value.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="culture">Culture.</param>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}