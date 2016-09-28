// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common.Presenter
{
	/// <summary>
	/// View.
	/// </summary>
	public interface IView
	{
		#region Properties

		/// <summary>
		/// The is in.
		/// </summary>
		bool IsInProgress { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Sets the error message.
		/// </summary>
		/// <returns>The error message.</returns>
		/// <param name="message">Message.</param>
		void SetErrorMessage(string message);

		#endregion
	}
}