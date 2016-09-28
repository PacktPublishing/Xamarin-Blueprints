// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomLabel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.XamForms.Controls
{
	using Xamarin.Forms;

	/// <summary>
	/// Custom label.
	/// </summary>
	public class CustomLabel : Label
	{
		#region Public Static Properties

		/// <summary>
		/// The android font style property.
		/// </summary>
		public static readonly BindableProperty AndroidFontStyleProperty = BindableProperty.Create<CustomLabel, string>(
			p => p.AndroidFontStyle, default(string));

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the android font style.
		/// </summary>
		/// <value>The android font style.</value>
		public string AndroidFontStyle
		{
			get
			{
				return (string)GetValue(AndroidFontStyleProperty);
			}
			set
			{
				SetValue(AndroidFontStyleProperty, value);
			}
		}

		#endregion
	}
}