// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextToSpeechDroid.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.Droid
{
	using System.Collections.Generic;

	using Android.Speech.Tts;

	using Java.Lang;

	using Xamarin.Forms;

	/// <summary>
	/// Text to speech droid.
	/// </summary>
	public class TextToSpeechDroid : Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		#region Private Properties

		/// <summary>
		/// The speaker.
		/// </summary>
		private TextToSpeech speaker;

		/// <summary>
		/// To speak.
		/// </summary>
		private string toSpeak;

		#endregion

		#region Public Methods

		/// <summary>
		/// Speak the specified msg.
		/// </summary>
		/// <param name="msg">Message.</param>
		public void Speak (string msg)
		{
			var ctx = Forms.Context; // useful for many Android SDK features
			toSpeak = msg;

			if (speaker == null) 
			{
				speaker = new TextToSpeech (ctx, this);
			} 
			else 
			{
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
			}
		}

		#endregion

		#region TextToSpeech.IOnInitListener implementation

		/// <summary>
		/// Called when IOnInitListener is initialized.
		/// </summary>
		/// <returns>The init.</returns>
		/// <param name="status">Status.</param>
		public void OnInit (OperationResult status)
		{
			/// <summary>
			/// Text to speech droid.
			/// </summary>
			if (status.Equals (OperationResult.Success)) 
			{
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
			}
		}

		#endregion
	}
}