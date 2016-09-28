// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileItemViewModel.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.ViewModels
{
	using FileStorage.Portable.UI;
	using FileStorage.Portable.Extras;
	using FileStorage.Portable.DataAccess.Storable;

	/// <summary>
	/// File item view model.
	/// </summary>
	public class FileItemViewModel : ViewModelBase
	{
		#region Private Properties

		/// <summary>
		/// The name.
		/// </summary>
		private string _fileName;

		/// <summary>
		/// The identifier.
		/// </summary>
		private string _contents;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
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

		#endregion

		#region Public Methods

		/// <summary>
		/// Apply the specified file.
		/// </summary>
		/// <param name="file">File.</param>
		public void Apply(FileStorable file)
		{
			FileName = file.Key ?? string.Empty;
			Contents = file.Contents ?? string.Empty;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Portable.ViewModels.FileItemViewModel"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		public FileItemViewModel(INavigationService navigation, IMethods methods) 
			: base(navigation, methods)
		{
		}

		#endregion
	}
}