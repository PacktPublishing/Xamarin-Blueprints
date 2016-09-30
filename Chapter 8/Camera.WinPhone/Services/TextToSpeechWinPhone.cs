// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextToSpeechWinPhone.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.WinPhone.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Windows.Media.SpeechSynthesis;
    using Windows.UI.Xaml.Controls;

	/// <summary>
	/// Text to speech win phone.
	/// </summary>
    public class TextToSpeechWinPhone : ITextToSpeech
    {
		#region Public Methods

		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="text">string.</param>
        public async void Speak(string text)
        {
            MediaElement mediaElement = new MediaElement();

            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

		#endregion
	}
}