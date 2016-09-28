// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinPhoneModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeechTalk.WinPhone.Modules
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Autofac;

    using SpeechTalk.Ioc;
    using SpeechTalk.WinPhone.Services;

	/// <summary>
	/// Win phone module.
	/// </summary>
    public class WinPhoneModule : IModule
    {
		/// <summary>
		/// Register the specified builder.
		/// </summary>
		/// <param name="builder">builder.</param>
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<TextToSpeechWinPhone>().As<ITextToSpeech>().SingleInstance();
        }
    }
}