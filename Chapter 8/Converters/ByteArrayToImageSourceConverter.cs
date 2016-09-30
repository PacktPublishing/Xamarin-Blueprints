// --------------------------------------------------------------------------------------------------
//  <copyright file="ByteArrayToImageSourceConverter.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2014 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Camera.Converters
{
	using System;
	using System.IO;
	using System.Globalization;

	using Xamarin.Forms;

	/// <summary>
	/// Byte array to image source converter.
	/// </summary>
	public class ByteArrayToImageSourceConverter : IValueConverter
	{
		#region Public Methods

		/// <summary>
		/// Convert the specified value, targetType, parameter and culture.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="culture">Culture.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			byte[] bytes = value as byte[];
			var defaultFile = parameter as string;

			if (bytes != null && bytes.Length > 1)
			{
				return ImageSource.FromStream(() => new MemoryStream(bytes));
			}

			if (defaultFile != null)
			{
				return ImageSource.FromFile(defaultFile);
			}

			return ImageSource.FromFile ("loading.png");
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

