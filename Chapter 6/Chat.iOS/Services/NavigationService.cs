// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.iOS.Services
{
	using System.Collections.Generic;

	using UIKit;

	using Chat.iOS.Views;

	using Chat.Common;
	using Chat.Common.Presenter;

	/// <summary>
	/// Navigation service.
	/// </summary>
	public class NavigationService : INavigationService
	{
		#region Private Properties

		/// <summary>
		/// The navigation controller.
		/// </summary>
		private UINavigationController _navigationController;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.iOS.Services.NavigationService"/> class.
		/// </summary>
		/// <param name="navigationController">Navigation controller.</param>
		public NavigationService(UINavigationController navigationController)
		{
			_navigationController = navigationController;
		}

		#endregion

		#region INavigationService implementation

		/// <summary>
		/// Pushs the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="presenter">Presenter.</param>
		public void PushPresenter(BasePresenter presenter)
		{
			if (presenter is LoginPresenter)
			{
				var viewController = new LoginViewController(presenter as LoginPresenter);
				_navigationController.PushViewController(viewController, true);
			}
			else if (presenter is ClientsListPresenter)
			{
				var viewController = new ClientsListViewController(presenter as ClientsListPresenter);
				_navigationController.PushViewController(viewController, true);
			}
			else if (presenter is ChatPresenter)
			{
				var viewController = new ChatViewController(presenter as ChatPresenter);
				_navigationController.PushViewController(viewController, true);
			}
		}

		/// <summary>
		/// Pops the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="animated">Animated.</param>
		public void PopPresenter(bool animated)
		{
			_navigationController.PopViewController(animated);
		}

		#endregion
	}
}