// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Droid.Services
{
	using Chat.Droid.Views;

	using System;

	using Android.Content;

	using Chat.Common;
	using Chat.Common.Presenter;

	/// <summary>
	/// Navigation service.
	/// </summary>
	public class NavigationService : INavigationService
	{
		#region Private Properties

		/// <summary>
		/// At application.
		/// </summary>
		private ChatApplication _application;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Droid.Services.NavigationService"/> class.
		/// </summary>
		/// <param name="application">Application.</param>
		public NavigationService(ChatApplication application)
		{
			_application = application;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Pushs the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="presenter">Presenter.</param>
		public void PushPresenter(BasePresenter presenter)
		{
			var oldPresenter = _application.Presenter as BasePresenter;

			if (presenter != oldPresenter)
			{
				_application.Presenter = presenter;
				Intent intent = null;

				if (presenter is LoginPresenter)
				{
					intent = new Intent(_application.CurrentActivity, typeof(LoginActivity));
				}
				else if (presenter is ClientsListPresenter)
				{
					intent = new Intent(_application.CurrentActivity, typeof(ClientsListActivity));
				}
				else if (presenter is ChatPresenter)
				{
					intent = new Intent(_application.CurrentActivity, typeof(ChatActivity));
				}

				if (intent != null)
				{
					_application.CurrentActivity.StartActivity(intent);
				}
			}
		}

		/// <summary>
		/// Pops the presenter.
		/// </summary>
		/// <returns>The presenter.</returns>
		/// <param name="animated">Animated.</param>
		public void PopPresenter(bool animated)
		{
			_application.CurrentActivity.Finish();
		}

		#endregion
	}
}