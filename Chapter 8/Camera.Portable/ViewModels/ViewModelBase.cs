// <copyright file="ViewModelBase.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Portable.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Reactive.Threading.Tasks;
	using System.Runtime.CompilerServices;

	using Camera.Portable.UI;
	using Camera.Portable.Extras;

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
		public event EventHandler<AlertArgs> Alert;

		#endregion

		#region Public Properties

		/// <summary>
		/// The navigation.
		/// </summary>
		public INavigationService Navigation;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.Portable.ViewModels.ViewModelBase"/> class.
		/// </summary>
		/// <param name="navigation">Navigation.</param>
		public ViewModelBase(INavigationService navigation)
		{
			Navigation = navigation;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Sets the property.
		/// </summary>
		/// <returns>The property.</returns>
		/// <param name="propertyName">Property name.</param>
		/// <param name="referenceProperty">Reference property.</param>
		/// <param name="newProperty">New property.</param>
		protected void SetProperty<T>(string propertyName, ref T referenceProperty, T newProperty)
		{
			if (!newProperty.Equals(referenceProperty))
			{
				referenceProperty = newProperty;
			}

			OnPropertyChanged(propertyName);
		}

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
		/// Notifies the alert.
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="message">Message.</param>
		public Task<bool> NotifyAlert(string message)
		{
			var tcs = new TaskCompletionSource<bool>();

			Alert?.Invoke(this, new AlertArgs()
			{
				Message = message,
				Tcs = tcs
			});

			return tcs.Task;
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