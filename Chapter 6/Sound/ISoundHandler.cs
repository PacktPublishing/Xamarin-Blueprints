// --------------------------------------------------------------------------------------------------
//  <copyright file="ISoundHandler.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace Chat.Portable.Sound
{
	using System;
	using System.Diagnostics;

	public interface ISoundHandler
	{
		bool IsPlaying { get; set; }

		void Load();

		void PlayPause();

		void Stop();

		double Duration();

		void SetPosition(double value);

		double CurrentPosition();

		void Forward();

		void Rewind();
	}
}