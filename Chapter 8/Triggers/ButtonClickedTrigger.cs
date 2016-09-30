// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonClickedTrigger.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Triggers
{
	using Xamarin.Forms;

	/// <summary>
	/// Button clicked trigger.
	/// </summary>
	public class ButtonClickedTrigger : TriggerAction<Button>
	{
		#region Protected Methods

		/// <summary>
		/// Invoke the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		protected override void Invoke(Button sender)
		{
			sender.TextColor = Color.Blue;
			sender.BackgroundColor = Color.Aqua;
		}

		#endregion
	}
}