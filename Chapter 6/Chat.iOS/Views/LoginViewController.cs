// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewController.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.iOS.Views
{
	using System;
	using System.Linq;

	using UIKit;

	using Chat.iOS.Extras;
	using Chat.iOS.Extensions;

	using Chat.Common.Presenter;

	/// <summary>
	/// Login view controller.
	/// </summary>
	public class LoginViewController : UIViewController, LoginPresenter.ILoginView
	{
		#region Private Properties

		/// <summary>
		/// The is in progress.
		/// </summary>
		private bool _isInProgress = false;

		/// <summary>
		/// The presenter.
		/// </summary>
		private LoginPresenter _presenter;

		/// <summary>
		/// The login text field.
		/// </summary>
		private UITextField _loginTextField;

		/// <summary>
		/// The password text field.
		/// </summary>
		private UITextField _passwordTextField;

		/// <summary>
		/// The activity indicator view.
		/// </summary>
		private UIActivityIndicatorView _activityIndicatorView;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Chat.iOS.Views.LoginViewController"/> class.
		/// </summary>
		/// <param name="presenter">Presenter.</param>
		public LoginViewController(LoginPresenter presenter)
		{
			_presenter = presenter;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		/// <returns>The did load.</returns>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			_presenter.SetView(this);

			var width = View.Bounds.Width;
			var height = View.Bounds.Height;

			Title = "Welcome";

			var titleLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Text = "Chat",
				Font = UIFont.FromName("Helvetica-Bold", 22),
				TextAlignment = UITextAlignment.Center
			};

			_activityIndicatorView = new UIActivityIndicatorView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Color = UIColor.Black
			};

			var descriptionLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Text = "Enter your login name to join the chat room.",
				Font = UIFont.FromName("Helvetica", 18),
				TextAlignment = UITextAlignment.Center
			};

			_loginTextField = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Placeholder = "Username",
				Font = UIFont.FromName("Helvetica", 18),
				BackgroundColor = UIColor.Clear.FromHex("#DFE4E6"),
				TextAlignment = UITextAlignment.Center
			};

			_passwordTextField = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Placeholder = "Password",
				Font = UIFont.FromName("Helvetica", 18),
				BackgroundColor = UIColor.Clear.FromHex("#DFE4E6"),
				TextAlignment = UITextAlignment.Center
			};

			var buttonView = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			var loginButton = new UIButton(UIButtonType.RoundedRect)
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			loginButton.SetTitle("Login", UIControlState.Normal);
			loginButton.TouchUpInside += (sender, e) => 
				Login(this, new Tuple<string, string>(_loginTextField.Text, _passwordTextField.Text));

			var registerButton = new UIButton(UIButtonType.RoundedRect)
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			registerButton.SetTitle("Register", UIControlState.Normal);
			registerButton.TouchUpInside += (sender, e) =>
				Register(this, new Tuple<string, string>(_loginTextField?.Text, _passwordTextField?.Text));

			Add(titleLabel);
			Add(descriptionLabel);
			Add(_activityIndicatorView);
			Add(_loginTextField);
			Add(_passwordTextField);
			Add(buttonView);

			buttonView.Add(loginButton);
			buttonView.Add(registerButton);

			var views = new DictionaryViews()
			{
				{"titleLabel", titleLabel},
				{"descriptionLabel", descriptionLabel},
				{"loginTextField", _loginTextField},
				{"passwordTextField", _passwordTextField},
				{"loginButton", loginButton},
				{"registerButton", registerButton},
				{"activityIndicatorView", _activityIndicatorView},
				{"buttonView", buttonView}
			};

			buttonView.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|-[registerButton]-|", NSLayoutFormatOptions.DirectionLeftToRight, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-[loginButton]-|", NSLayoutFormatOptions.DirectionLeftToRight, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-[registerButton]-30-[loginButton]-|", NSLayoutFormatOptions.DirectionLeftToRight, null, views))
				.ToArray());
			
			View.AddConstraints(
				NSLayoutConstraint.FromVisualFormat("V:|-100-[titleLabel(50)]-[descriptionLabel(30)]-10-[loginTextField(30)]-10-[passwordTextField(30)]-10-[buttonView]", NSLayoutFormatOptions.DirectionLeftToRight, null, views)
				.Concat(NSLayoutConstraint.FromVisualFormat("V:|-100-[activityIndicatorView(50)]-[descriptionLabel(30)]-10-[loginTextField(30)]-10-[passwordTextField(30)]-10-[buttonView]", NSLayoutFormatOptions.DirectionLeftToRight, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-10-[titleLabel]-10-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:[activityIndicatorView(30)]-10-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-10-[descriptionLabel]-10-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-30-[loginTextField]-30-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(NSLayoutConstraint.FromVisualFormat("H:|-30-[passwordTextField]-30-|", NSLayoutFormatOptions.AlignAllTop, null, views))
				.Concat(new[] { NSLayoutConstraint.Create(buttonView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 1) })
				.ToArray());
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
			var alert = new UIAlertView()
			{
				Title = "Chat",
				Message = message
			};
			alert.AddButton("OK");
			alert.Show();
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
					_activityIndicatorView.StartAnimating();
				}
				else
				{
					_activityIndicatorView.StopAnimating();
				}

				_isInProgress = value;
			}
		}

		#endregion
	}
}