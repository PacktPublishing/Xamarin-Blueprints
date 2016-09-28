// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LowercaseEntryBehaviour.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Behaviours
{
	using Xamarin.Forms;

	/// <summary>
	/// Lowercase entry behaviour.
	/// </summary>
	public class LowercaseEntryBehaviour : Behavior<Entry>
	{
		#region Protected Methods

		/// <summary>
		/// When attached to.
		/// </summary>
		/// <returns>The attached to.</returns>
		/// <param name="entry">Entry.</param>
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		/// <summary>
		/// When detaching from.
		/// </summary>
		/// <returns>The detaching from.</returns>
		/// <param name="entry">Entry.</param>
		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Ons the entry text changed.
		/// </summary>
		/// <returns>The entry text changed.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			((Entry)sender).Text = args.NewTextValue.ToLower();
		}

		#endregion
	}
}