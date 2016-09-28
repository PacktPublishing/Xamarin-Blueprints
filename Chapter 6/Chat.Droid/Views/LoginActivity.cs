// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Droid.Views
{
	using System;

	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Widget;

	using Chat.Droid.Services;

	using Chat.Common.Presenter;

	/// <summary>
	/// Login activity.
	/// </summary>
	[Activity(MainLauncher = true, Label = "Chat", ScreenOrientation = ScreenOrientation.Portrait)]
	public class LoginActivity : Activity, LoginPresenter.ILoginView
	{
		#region Private Properties

		/// <summary>
		/// The is in progress.
		/// </summary>
		private bool _isInProgress = false;

		/// <summary>
		/// The dialog shown.
		/// </summary>
		private bool _dialogShown = false;

		/// <summary>
		/// The presenter.
		/// </summary>
		private LoginPresenter _presenter;

		/// <summary>
		/// The login field.
		/// </summary>
		private EditText _loginField;

		/// <summary>
		/// The password field.
		/// </summary>
		private EditText _passwordField;

		/// <summary>
		/// The progress dialog.
		/// </summary>
		private ProgressDialog progressDialog;

		#endregion

		#region Protected Methods

		/// <summary>
		/// Ons the create.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.LoginView);

			progressDialog = new ProgressDialog(this);
			progressDialog.SetMessage("Loading...");
			progressDialog.SetCancelable(false);

			_loginField = FindViewById<EditText>(Resource.Id.usernameField);
			_passwordField = FindViewById<EditText>(Resource.Id.passwordField);

			var registerButton = FindViewById<Button>(Resource.Id.registerButton);
			registerButton.Touch += (sender, e) =>
				Register(this, new Tuple<string, string>(_loginField.Text, _passwordField.Text));
			
			var loginButton = FindViewById<Button>(Resource.Id.loginButton);
			loginButton.Touch += (sender, e) =>
				Login(this, new Tuple<string, string>(_loginField.Text, _passwordField.Text));

			var app = ChatApplication.GetApplication(this);

			var state = new ApplicationState();
			_presenter = new LoginPresenter(state, new NavigationService(app));
			_presenter.SetView(this);

			app.CurrentActivity = this;
		}

		/// <summary>
		/// Ons the resume.
		/// </summary>
		/// <returns>The resume.</returns>
		protected override void OnResume()
		{
			base.OnResume();

			var app = ChatApplication.GetApplication(this);
			app.CurrentActivity = this;

			if (_presenter != null)
			{
				_presenter.SetView(this);
			}
		}

		#endregion

		#region ILoginView implementation

		/// <summary>
		/// Occurs when login.
		/// </summary>
		public event EventHandler<Tuple<string, string>> Login;

		/// <summary>
		/// Occurs when register.
		/// </summary>
		public event EventHandler<Tuple<string, string>> Register;

		#endregion

		#region IView implementation

		/// <summary>
		/// Sets the error message.
		/// </summary>
		/// <returns>The error message.</returns>
		/// <param name="message">Message.</param>
		public void SetErrorMessage(string message)
		{
			if (!_dialogShown)
			{
				_dialogShown = true;

				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder
					.SetTitle("Chat")
					.SetMessage(message)
					.SetNeutralButton("Ok", (sender, e) => { _dialogShown = false ;})
					.Show();
			}
		}

		/// <summary>
		/// Gets or sets the is in progress.
		/// </summary>
		/// <value>The is in progress.</value>
		public bool IsInProgress
		{
			get
			{
				return _isInProgress;
			}

			set
			{
				if (value == _isInProgress)
				{
					return;
				}

				// we control the activity view when we set 'IsInProgress'
				if (value)
				{
					progressDialog.Show();
				}
				else
				{
					progressDialog.Dismiss();
				}

				_isInProgress = value;
			}
		}

		#endregion
	}
}