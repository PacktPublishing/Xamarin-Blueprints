// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelShadowEffectDroid.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Xamarin.Forms.ResolutionGroupName("Camera")]
[assembly: Xamarin.Forms.ExportEffect(typeof(Camera.Droid.Effects.LabelShadowEffectDroid), "LabelShadowEffect")]

namespace Camera.Droid.Effects
{
	using System;
	using System.Linq;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.Android;

	using Camera.Effects;

	/// <summary>
	/// Label shadow effect.
	/// </summary>
	public class LabelShadowEffectDroid : PlatformEffect
	{
		#region Protected Methods

		/// <summary>
		/// Ons the attached.
		/// </summary>
		protected override void OnAttached()
		{
			try
			{
				var control = Control as Android.Widget.TextView;

				var effect = (LabelShadowEffect)Element.Effects.FirstOrDefault(e => e is LabelShadowEffect);

				if (effect != null)
				{
					control.SetShadowLayer(effect.Radius, effect.DistanceX, effect.DistanceY, effect.Color.ToAndroid());
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		/// <summary>
		/// Ons the detached.
		/// </summary>
		protected override void OnDetached()
		{
		}

		#endregion
	}
}