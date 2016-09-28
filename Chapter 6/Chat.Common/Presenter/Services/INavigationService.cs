// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationService.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Common
{
	using Chat.Common.Presenter;

	/// <summary>
	/// Navigation service.
	/// </summary>
	public interface INavigationService
    {
		#region Methods

		/// <summary>
		/// Pushs the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="presenter">Presenter.</param>
		void PushPresenter(BasePresenter presenter);

		/// <summary>
		/// Pops the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="animated">Animated.</param>
		void PopPresenter (bool animated);

		#endregion
    }
}