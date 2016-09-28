// --------------------------------------------------------------------------------------------------
//  <copyright file="DictionaryViews.cs" company="Flush Arcade.">
//    Copyright (c) 2014 Flush Arcade. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace AudioPlayer.iOS.Extras
{
	using System.Collections;

	using Foundation;
	using UIKit;

	/// <summary>
	/// Dictionary views.
	/// </summary>
	public class DictionaryViews : IEnumerable
	{
		#region Private Properties

		/// <summary>
		/// The ns dictionary.
		/// </summary>
		private readonly NSMutableDictionary nsDictionary;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AudioPlayer.iOS.Extras.DictionaryViews"/> class.
		/// </summary>
		public DictionaryViews()
		{
			nsDictionary = new NSMutableDictionary();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Add the specified name and view.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="view">View.</param>
		public void Add(string name, UIView view)
		{
			nsDictionary.Add(new NSString(name), view);
		}

		/// <summary>
		/// Ops the implicit.
		/// </summary>
		/// <returns>The implicit.</returns>
		/// <param name="us">Us.</param>
		public static implicit operator NSDictionary(DictionaryViews us)
		{
			return us.ToNSDictionary();
		}

		/// <summary>
		/// Returns the NS Dictionary.
		/// </summary>
		/// <returns>The NSD ictionary.</returns>
		public NSDictionary ToNSDictionary()
		{
			return nsDictionary;
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)nsDictionary).GetEnumerator();
		}

		#endregion
	}
}