// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextToSpeech.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk
{
	using AVFoundation;

	/// <summary>
	/// Text to speech.
	/// </summary>
	public class TextToSpeech : ITextToSpeech
	{
		#region Public Methods

		/// <summary>
		/// Speak the specified msg.
		/// </summary>
		/// <param name="msg">Message.</param>
		public void Speak (string msg)
		{
			var speechSynthesizer = new AVSpeechSynthesizer ();

			var speechUtterance = new AVSpeechUtterance (msg) 
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
				Voice = AVSpeechSynthesisVoice.FromLanguage ("en-US"),
				Volume = 0.5f,
				PitchMultiplier = 1.0f
			};

			speechSynthesizer.SpeakUtterance (speechUtterance);
		}

		#endregion
	}
}