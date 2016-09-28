// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stocklist.Portable.ViewModels
{
	using System;
	using System.Windows.Input;

	using Stocklist.Portable.Enums;
	using Stocklist.Portable.UI;
	using Stocklist.Portable.Extras;

	/// <summary>
	/// Main page view model.
	/// </summary>
	public class MainPageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The in progress.
		/// </summary>
		private bool _inProgress;

		/// <summary>
		/// The stocklist command.
		/// </summary>
		private ICommand _stocklistCommand;

		/// <summary>
		/// The exit command.
		/// </summary>
		private ICommand _exitCommand;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the in progress.
		/// </summary>
		/// <value>The in progress.</value>
		public bool InProgress
		{
			get
			{
				return _inProgress;
			}

			set
			{
				if (value.Equals(_inProgress))
				{
					return;
				}

				_inProgress = value;
				OnPropertyChanged("InProgress");
			}
		}

		/// <summary>
		/// Gets or sets the stocklist command.
		/// </summary>
		/// <value>The stocklist command.</value>
		public ICommand StocklistCommand
		{
			get
			{
				return _stocklistCommand;
			}

			set
			{
				if (value.Equals(_stocklistCommand))
				{
					return;
				}

				_stocklistCommand = value;
				OnPropertyChanged("StocklistCommand");
			}
		}

		/// <summary>
		/// Gets or sets the exit command.
		/// </summary>
		/// <value>The exit command.</value>
		public ICommand ExitCommand
		{
			get
			{
				return _exitCommand;
			}

			set
			{
				if (value.Equals(_exitCommand))
				{
					return;
				}

				_exitCommand = value;
				OnPropertyChanged("ExitCommand");
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stocklist.Portable.ViewModels.MainPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="commandFactory">Command factory.</param>
		/// <param name="methods">Methods.</param>
		public MainPageViewModel (INavigationService navigation, Func<Action, ICommand> commandFactory,
			IMethods methods) : base (navigation)
		{
			_exitCommand = commandFactory (() =>
			{
				methods.Exit();
			});

			_stocklistCommand = commandFactory (async () =>
			{
				await Navigation.Navigate(PageNames.StocklistPage, null);
			});
		}

		#endregion
	}
}

