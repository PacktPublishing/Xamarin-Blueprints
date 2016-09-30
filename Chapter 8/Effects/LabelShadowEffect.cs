// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelShadowEffect.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Effects
{
	using Xamarin.Forms;

	/// <summary>
	/// Shadow effect.
	/// </summary>
	public class LabelShadowEffect : RoutingEffect
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the radius.
		/// </summary>
		/// <value>The radius.</value>
		public float Radius { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get; set; }

		/// <summary>
		/// Gets or sets the distance x.
		/// </summary>
		/// <value>The distance x.</value>
		public float DistanceX { get; set; }

		/// <summary>
		/// Gets or sets the distance y.
		/// </summary>
		/// <value>The distance y.</value>
		public float DistanceY { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.Effects.ShadowEffect"/> class.
		/// </summary>
		public LabelShadowEffect() : base("Camera.LabelShadowEffect")
		{
		}

		#endregion
	}
}