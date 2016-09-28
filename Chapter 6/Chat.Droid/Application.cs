// --------------------------------------------------------------------------------------------------
//  <copyright file="ClientsListViewController.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Droid
{
	using System;

	using Android.App;
	using Android.Content;
	using Android.Runtime;

	/// <summary>
	/// Chat application.
	/// </summary>
	[Application]
	public class ChatApplication : Application
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the presenter.
		/// </summary>
		/// <value>The presenter.</value>
		public object Presenter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current activity.
		/// </summary>
		/// <value>The current activity.</value>
		public Activity CurrentActivity
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Droid.ChatApplication"/> class.
		/// </summary>
		public ChatApplication()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.Droid.ChatApplication"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public ChatApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the application.
		/// </summary>
		/// <returns>The application.</returns>
		/// <param name="context">Context.</param>
		public static ChatApplication GetApplication(Context context)
		{
			return (ChatApplication)context.ApplicationContext;
		}

		#endregion
	}
}