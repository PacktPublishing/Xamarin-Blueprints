// --------------------------------------------------------------------------------------------------
//  <copyright file="BoolToStringConverter.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2014 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Converters
{
	using System;
	using System.Linq;

	using Xamarin.Forms;

	using Camera.Portable.Ioc;
	using Camera.Portable.Logging;

	/// <summary>
	/// Bool to string converter.
	/// </summary>
	public class BoolToStringConverter:IValueConverter
	{
		#region Public Methods

		/// <summary>
		/// Convert the specified value, targetType, parameter and culture.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="culture">Culture.</param>
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				var str = parameter as string;

				if (str != null) 
				{
					// split string by ',', convert to int and store in case list
					var cases = str.Split(',').Select(x => x).ToList();

					if (value is bool)
					{
						return (bool)value ? cases[0] : cases[1];
					}
				}
			}
			catch (Exception error) 
			{
				IoC.Resolve<ILogger>().WriteLineTime("BoolToStringConverter \n" +
					"Convert() Failed to switch flash on/off \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);
			}

			return string.Empty;
		}

		/// <summary>
		/// Converts the back.
		/// </summary>
		/// <returns>The back.</returns>
		/// <param name="value">Value.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="culture">Culture.</param>
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}