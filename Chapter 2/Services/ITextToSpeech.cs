// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextToSpeech.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk
{
	/// <summary>
	/// Text to speech.
	/// </summary>
	public interface ITextToSpeech
	{
		#region Methods

		/// <summary>
		/// Speak the specified msg.
		/// </summary>
		/// <param name="msg">Message.</param>
		void Speak (string msg);

		#endregion
	}
}