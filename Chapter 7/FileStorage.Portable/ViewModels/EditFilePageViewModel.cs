// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditFilePageViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.ViewModels
{
	using System;
	using System.Threading.Tasks;
	using System.Windows.Input;
	using System.Collections.Generic;
	using System.Threading;

	using FileStorage.Portable.UI;
	using FileStorage.Portable.Extras;
	using FileStorage.Portable.DataAccess.Storage;
	using FileStorage.Portable.DataAccess.Storable;

	/// <summary>
	/// Edit file page view model.
	/// </summary>
	public class EditFilePageViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The storage.
		/// </summary>
		private readonly ISQLiteStorage _storage;

		/// <summary>
		/// The save file command.
		/// </summary>
		private ICommand _saveFileCommand;

		/// <summary>
		/// The delete file command.
		/// </summary>
		private ICommand _deleteFileCommand;

		/// <summary>
		/// The contents.
		/// </summary>
		private string _contents;

		/// <summary>
		/// The name of the file.
		/// </summary>
		private string _fileName;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the save file command.
		/// </summary>
		/// <value>The save file command.</value>
		public ICommand SaveFileCommand
		{
			get
			{
				return _saveFileCommand;
			}

			set
			{
				if (value.Equals(_saveFileCommand))
				{
					return;
				}

				_saveFileCommand = value;
				OnPropertyChanged("FileEditCommand");
			}
		}

		/// <summary>
		/// Gets or sets the delete file command.
		/// </summary>
		/// <value>The delete file command.</value>
		public ICommand DeleteFileCommand
		{
			get
			{
				return _deleteFileCommand;
			}

			set
			{
				if (value.Equals(_deleteFileCommand))
				{
					return;
				}

				_deleteFileCommand = value;
				OnPropertyChanged("CreateFileCommand");
			}
		}

		/// <summary>
		/// Gets or sets the contents.
		/// </summary>
		/// <value>The contents.</value>
		public string Contents
		{
			get
			{
				return _contents;
			}

			set
			{
				if (value.Equals(_contents))
				{
					return;
				}

				_contents = value;
				OnPropertyChanged("Contents");
			}
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get
			{
				return _fileName;
			}

			set
			{
				if (value.Equals(_fileName))
				{
					return;
				}

				_fileName = value;
				OnPropertyChanged("FileName");
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Portable.ViewModels.MapPageViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		/// <param name="commandFactory">Command factory.</param>
		public EditFilePageViewModel (INavigationService navigation, Func<Action, ICommand> commandFactory, 
		                              IMethods methods, ISQLiteStorage storage) 
			: base (navigation, methods)
		{
			_storage = storage;

			_saveFileCommand = commandFactory(async () =>
			{
				await _storage.InsertObject(new FileStorable()
				{
					Key = FileName,
					Contents = Contents
				});

				NotifyAlert("File saved.");
			});

			_deleteFileCommand = commandFactory(async () =>
			{
				await _storage.DeleteObjectByKey<FileStorable>(FileName);
				await Navigation.Pop();
			});
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Ons the appear.
		/// </summary>
		/// <returns>The appear.</returns>
		public void OnAppear()
		{

		}

		/// <summary>
		/// Ons the disppear.
		/// </summary>
		/// <returns>The disppear.</returns>
		public void OnDisppear()
		{
			FileName = string.Empty;
			Contents = string.Empty;
		}

		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="parameters">Parameters.</param>
		protected override async Task LoadAsync (IDictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("filename"))
			{
				FileName = (parameters["filename"] as string).ToLower();
			}

			if (parameters.ContainsKey("contents"))
			{
				Contents = parameters["contents"] as string;
			}
		}

		#endregion
	}
}