// --------------------------------------------------------------------------------------------------
//  <copyright file="OrientationToDoubleConverter.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Converters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Xamarin.Forms;

	using Camera.Portable.Ioc;
	using Camera.Portable.Enums;
	using Camera.Portable.Logging;

	/// <summary>
	/// Orientation to double converter.
	/// </summary>
	public class OrientationToDoubleConverter:IValueConverter
	{
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
					var cases = str.Split(',').Select(x => Double.Parse(x)).ToList();

					if (value is Orientation)
					{
						switch ((Orientation)value)
						{
							case Orientation.LandscapeRight:
							case Orientation.LandscapeLeft:
								return cases[0];
							case Orientation.Portrait:
								return cases[1];
							case Orientation.None:
								return 0;
						}
					}
				}
			}
			catch (Exception error)
			{
				IoC.Resolve<ILogger>().WriteLineTime("OrientationToDoubleConverter \n" +
					"Convert() Failed to switch flash on/off \n " +
					"ErrorMessage: \n" +
					error.Message + "\n" +
					"Stacktrace: \n " +
					error.StackTrace);
			}

			return 0;
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
	}
}

