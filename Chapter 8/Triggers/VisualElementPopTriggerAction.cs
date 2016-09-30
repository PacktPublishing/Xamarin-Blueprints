// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisualElementPopTriggerAction.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Camera.Triggers
{
	using Xamarin.Forms;

	/// <summary>
	/// Visual element pop trigger action.
	/// </summary>
	public class VisualElementPopTriggerAction : TriggerAction<VisualElement>
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the anchor.
		/// </summary>
		/// <value>The anchor.</value>
		public Point Anchor { set; get; }

		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		/// <value>The scale.</value>
		public double Scale { set; get; }

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>The length.</value>
		public uint Length { set; get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Camera.Triggers.VisualElementPopTriggerAction"/> class.
		/// </summary>
		public VisualElementPopTriggerAction()
		{
			Anchor = new Point(0.5, 0.5);
			Scale = 2;
			Length = 500;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Invoke the specified visual.
		/// </summary>
		/// <param name="visual">Visual.</param>
		protected override async void Invoke(VisualElement visual)
		{
			visual.AnchorX = Anchor.X;
			visual.AnchorY = Anchor.Y;

			await visual.ScaleTo(Scale, Length / 2, Easing.SinOut);
			await visual.ScaleTo(1, Length / 2, Easing.SinIn);
		}

		#endregion
	}
}