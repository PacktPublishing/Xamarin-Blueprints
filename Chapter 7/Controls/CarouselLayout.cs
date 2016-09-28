// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarouselView.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reactive.Linq;

	using Xamarin.Forms;

	using FileStorage.Portable.UI;

	/// <summary>
	/// Carousel layout.
	/// </summary>
	public class CarouselLayout : Layout<View>
	{
		#region Private Properties

		/// <summary>
		/// The data changes subscription.
		/// </summary>
		private IDisposable dataChangesSubscription;

		/// <summary>
		/// The width of the layout.
		/// </summary>
		public double LayoutWidth;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the <see cref="T:FileStorage.Controls.CarouselLayout"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public object this[int index]
		{
			get
			{
				return index < ItemsSource.Count() ? ItemsSource.ToList()[index] : null;
			}
		}

		/// <summary>
		/// Gets or sets the item template.
		/// </summary>
		/// <value>The item template.</value>
		public DataTemplate ItemTemplate
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable<object> ItemsSource { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Subscribes the data changes.
		/// </summary>
		/// <returns>The data changes.</returns>
		public void SubscribeDataChanges(IObservable<DataChange> dataChanges)
		{
			if (dataChangesSubscription != null)
			{
				dataChangesSubscription.Dispose();
			}

			dataChanges.Subscribe(x =>
			{
				PackViews();
			});
		}

		/// <summary>
		/// Computes the layout.
		/// </summary>
		/// <returns>The layout.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		public IEnumerable<Rectangle> ComputeLayout(double widthConstraint, double heightConstraint)
		{
			List<Row> layout = ComputeNiaveLayout(widthConstraint, heightConstraint);

			return layout.SelectMany(s => s);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Ons the child measure invalidated.
		/// </summary>
		/// <returns>The child measure invalidated.</returns>
		protected override void OnChildMeasureInvalidated()
		{
			base.OnChildMeasureInvalidated();
		}

		/// <summary>
		/// Ons the measure.
		/// </summary>
		/// <returns>The measure.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
		{
			List<Row> layout = ComputeNiaveLayout(widthConstraint, heightConstraint);

			var last = layout[layout.Count - 1];

			var width = (last.Count > 0) ? last[0].X + last.Width : 0;
			var height = (last.Count > 0) ? last[0].Y + last.Height : 0;

			return new SizeRequest(new Size(width, height));
		}

		/// <summary>
		/// Layouts the children.
		/// </summary>
		/// <returns>The children.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			var layout = ComputeLayout(width, height);
			var i = 0;

			foreach (var region in layout)
			{
				var child = Children[i];
				i++;
				LayoutChildIntoBoundingRegion(child, region);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Packs the views.
		/// </summary>
		/// <returns>The views.</returns>
		private void PackViews()
		{
			Children.Clear();

			if (ItemsSource != null)
			{
				foreach (var cell in ItemsSource)
				{
					// create specified view from func and add to the children
					var view = (View)ItemTemplate.CreateContent();
					view.BindingContext = cell;
					if (view != null)
					{
						Children.Add(view);
					}
				}
			}
		}

		/// <summary>
		/// Computes the niave layout.
		/// </summary>
		/// <returns>The niave layout.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		private List<Row> ComputeNiaveLayout(double widthConstraint, double heightConstraint)
		{
			var result = new List<Row>();
			var row = new Row();
			result.Add(row);

			var spacing = 20;
			double y = 0;

			foreach (var child in Children)
			{
				var request = child.Measure(double.PositiveInfinity, double.PositiveInfinity);

				if (row.Count == 0)
				{
					row.Add(new Rectangle(0, y, LayoutWidth, Height));
					row.Height = request.Request.Height;
					continue;
				}

				var last = row[row.Count - 1];
				var x = last.Right + spacing;
				var childWidth = LayoutWidth;
				var childHeight = request.Request.Height;

				row.Add(new Rectangle(x, y, childWidth, Height));
				row.Width = x + childWidth;
				row.Height = Math.Max(row.Height, Height);
			}

			return result;
		}

		#endregion
	}

	/// <summary>
	/// Row.
	/// </summary>
	sealed class Row : List<Rectangle>
	{
		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public double Height { get; set; }

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public double Width { get; set; }
	}
}