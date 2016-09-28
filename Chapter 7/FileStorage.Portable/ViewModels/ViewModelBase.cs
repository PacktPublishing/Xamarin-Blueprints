// <copyright file="ViewModelBase.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Reactive.Threading.Tasks;
	using System.Runtime.CompilerServices;

	using FileStorage.Portable.UI;
	using FileStorage.Portable.Extras;

	/// <summary>
	/// The base class of all view models
	/// </summary>
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region Public Events

		/// <summary>
		/// The property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Occurs when alert.
		/// </summary>
		public event EventHandler<string> Alert;

		#endregion

		#region Private Properties

		/// <summary>
		/// The navigation.
		/// </summary>
		private IMethods _methods;

		#endregion

		#region Public Properties

		/// <summary>
		/// The navigation.
		/// </summary>
		public INavigationService Navigation;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FileStorage.Portable.ViewModels.ViewModelBase"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		public ViewModelBase(INavigationService navigation, IMethods methods)
		{
			Navigation = navigation;

			_methods = methods;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="parameters">Parameters.</param>
		protected virtual async Task LoadAsync(IDictionary<string, object> parameters)
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Shows the alert.
		/// </summary>
		/// <returns>The alert.</returns>
		public Task<string> ShowEntryAlert(string message)
		{
			var tcs = new TaskCompletionSource<string>();

			_methods.DisplayEntryAlert(tcs, message);

			return tcs.Task;
		}

		/// <summary>
		/// Notifies the alert.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="message">Message.</param>
		public void NotifyAlert(string message)
		{
			if (Alert != null)
			{
				Alert(this, message);
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="parameters">
		/// </param>
		public void OnShow(IDictionary<string, object> parameters)
		{
			LoadAsync(parameters).ToObservable().Subscribe(
				result =>
				{
					// we can add things to do after we load the view model
				}, 
				ex =>
				{
					// we can handle any areas from the load async function
				});
		}

		#endregion
	}
}