// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.ViewModels
{
	using System;
	using System.Windows.Input;
	using System.Threading.Tasks;
	using System.Threading;

	using FileStorage.Portable.Enums;
	using FileStorage.Portable.UI;
	using FileStorage.Portable.Extras;
	using FileStorage.Portable.DataAccess.Storage;
	using FileStorage.Portable.DataAccess.Storable;

	/// <summary>
	/// Main page view model.
	/// </summary>
	public class MainPageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The description message.
		/// </summary>
		private string _descriptionMessage = "Welcome to the Filing Room";

		/// <summary>
		/// The location title.
		/// </summary>
		private string _FilesTitle = "Files";

		/// <summary>
		/// The exit title.
		/// </summary>
		private string _exitTitle = "Exit";

		/// <summary>
		/// The location command.
		/// </summary>
		private ICommand _locationCommand;

		/// <summary>
		/// The exit command.
		/// </summary>
		private ICommand _exitCommand;

		/// <summary>
		/// The storage.
		/// </summary>
		private ISQLiteStorage _storage;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the description message.
		/// </summary>
		/// <value>The description message.</value>
		public string DescriptionMessage
		{
			get
			{
				return _descriptionMessage;
			}

			set
			{
				if (value.Equals(_descriptionMessage))
				{
					return;
				}

				_descriptionMessage = value;
				OnPropertyChanged("DescriptionMessage");
			}
		}

		/// <summary>
		/// Gets or sets the location title.
		/// </summary>
		/// <value>The location title.</value>
		public string FilesTitle
		{
			get
			{
				return _FilesTitle;
			}

			set
			{
				if (value.Equals(_FilesTitle))
				{
					return;
				}

				_FilesTitle = value;
				OnPropertyChanged("FilesTitle");
			}
		}

		/// <summary>
		/// Gets or sets the exit title.
		/// </summary>
		/// <value>The exit title.</value>
		public string ExitTitle
		{
			get
			{
				return _exitTitle;
			}

			set
			{
				if (value.Equals(_exitTitle))
				{
					return;
				}

				_exitTitle = value;
				OnPropertyChanged("ExitTitle");
			}
		}

		/// <summary>
		/// Gets or sets the location command.
		/// </summary>
		/// <value>The location command.</value>
		public ICommand LocationCommand
		{
			get
			{
				return _locationCommand;
			}

			set
			{
				if (value.Equals(_locationCommand))
				{
					return;
				}

				_locationCommand = value;
				OnPropertyChanged("LocationCommand");
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
		/// Initializes a new instance of the <see cref="T:FileStorage.Portable.ViewModels.MainPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="commandFactory">Command factory.</param>
		/// <param name="methods">Methods.</param>
		public MainPageViewModel (INavigationService navigation, Func<Action, ICommand> commandFactory,
			IMethods methods, ISQLiteStorage storage) : base (navigation, methods)
		{
			_exitCommand = commandFactory (() => methods.Exit());
			_locationCommand = commandFactory (async () => await Navigation.Navigate(PageNames.FilesPage, null));

			_storage = storage;

			SetupSQLite().ConfigureAwait(false);
		}

		#endregion

		/// <summary>
		/// Setups the SQL ite.
		/// </summary>
		/// <returns>The SQL ite.</returns>
		private async Task SetupSQLite()
		{
			// create Sqlite connection
			_storage.CreateSQLiteAsyncConnection();

			// create DB tables
			await _storage.CreateTable<FileStorable>();
		}
	}
}