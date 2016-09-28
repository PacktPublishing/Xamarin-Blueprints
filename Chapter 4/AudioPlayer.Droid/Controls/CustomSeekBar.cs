// --------------------------------------------------------------------------------------------------
//  <copyright file="CustomSeekBar.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.Droid.Controls
{
	using System;

	using Android.Content;
	using Android.Util;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

	/// <summary>
	/// Custom seek bar.
	/// </summary>
	public class CustomSeekBar : SeekBar
	{
		#region Events

		/// <summary>
		/// Occurs when value changed.
		/// </summary>
		public event EventHandler ValueChanged;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.Controls.CustomSeekBar"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		protected CustomSeekBar(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.Controls.CustomSeekBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public CustomSeekBar(Context context)
			: base(context)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.Controls.CustomSeekBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public CustomSeekBar(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.Droid.Controls.CustomSeekBar"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public CustomSeekBar(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Called when there is a touch event.
		/// </summary>
		/// <returns>The touch event.</returns>
		/// <param name="evt">Evt.</param>
		public override bool OnTouchEvent(MotionEvent evt)
		{
			if (!Enabled)
				return false;

			switch (evt.Action)
			{
				// only fire value change events when the touch is released
				case MotionEventActions.Up:
					{
						if (this.ValueChanged != null)
						{
							this.ValueChanged(this, EventArgs.Empty);
						}
					}
					break;
			}

			// we also want to fire all base motion events
			base.OnTouchEvent(evt);

			return true;
		}

		#endregion
	}
}