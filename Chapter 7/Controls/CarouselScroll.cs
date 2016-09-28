// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarouselScroll.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Controls
{
	using System;
	using System.Collections.Generic;

	using Xamarin.Forms;

	using FileStorage.Portable.UI;

	/// <summary>
	/// Tetrix grid.
	/// </summary>
	public class CarouselScroll : ScrollView
	{
		#region Private Properties

		/// <summary>
		/// The wrap layout.
		/// </summary>
		private CarouselLayout _carouselLayout;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the item template.
		/// </summary>
		/// <value>The item template.</value>
		public DataTemplate ItemTemplate
		{
			set
			{
				_carouselLayout.ItemTemplate = value;
			}
		}

		/// <summary>
		/// The items source property.
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<CarouselLayout, IEnumerable<object>>(o => o.ItemsSource,
			default(IEnumerable<object>), propertyChanged: (bindable, oldvalues, newValues) =>
		{
			((CarouselScroll)bindable)._carouselLayout.ItemsSource = newValues;
		});

		/// <summary>
		/// The stocklist command property.
		/// </summary>
		public static readonly BindableProperty DataChangesProperty = BindableProperty.Create("DataChanges",
																			  typeof(IObservable<DataChange>),
																			  typeof(CarouselLayout),
																			  null,
			propertyChanged: (bindable, oldvalue, newValue) =>
			{
				((CarouselScroll)bindable)._carouselLayout.SubscribeDataChanges((IObservable<DataChange>)newValue);
			});

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MyCareManager.XamForms.Controls.CarouselScroll.WrapGrid"/> class.
		/// </summary>
		public CarouselScroll()
		{
			Orientation = ScrollOrientation.Horizontal;

			_carouselLayout = new CarouselLayout();
			Content = _carouselLayout;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the selected item.
		/// </summary>
		/// <returns>The selected item.</returns>
		/// <param name="selected">Selected.</param>
		public object GetSelectedItem(int selected)
		{
			return _carouselLayout[selected];
		}

		#endregion

		#region Protected Methods

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
			base.LayoutChildren(x, y, width, height);

			if (_carouselLayout != null)
			{
				if (width > _carouselLayout.LayoutWidth)
				{
					_carouselLayout.LayoutWidth = width;
				}

				_carouselLayout.ComputeLayout(width, height);
			}
		}

		#endregion
	}
}