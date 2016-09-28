// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSemaphore.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.Threading
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	/// <summary>
	/// Async semaphore.
	/// </summary>
	public class AsyncSemaphore
	{
		#region Private Properties

		/// <summary>
		/// The s completed.
		/// </summary>
		private readonly static Task s_completed = Task.FromResult(true);

		/// <summary>
		/// The m waiters.
		/// </summary>
		private readonly Queue<TaskCompletionSource<bool>> m_waiters = new Queue<TaskCompletionSource<bool>>();

		/// <summary>
		/// The m current count.
		/// </summary>
		private int m_currentCount;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TelstraHealth.Portable.Threading.AsyncSemaphore"/> class.
		/// </summary>
		/// <param name="initialCount">Initial count.</param>
		public AsyncSemaphore(int initialCount)
		{
			if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
			m_currentCount = initialCount;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Waits the async.
		/// </summary>
		/// <returns>The async.</returns>
		public Task WaitAsync()
		{
			lock (m_waiters)
			{
				if (m_currentCount > 0)
				{
					--m_currentCount;
					return s_completed;
				}
				else
				{
					var waiter = new TaskCompletionSource<bool>();
					m_waiters.Enqueue(waiter);
					return waiter.Task;
				}
			}
		}

		/// <summary>
		/// Release this instance.
		/// </summary>
		public void Release()
		{
			TaskCompletionSource<bool> toRelease = null;

			lock (m_waiters)
			{
				if (m_waiters.Count > 0)
					toRelease = m_waiters.Dequeue();
				else
					++m_currentCount;
			}

			if (toRelease != null)
				toRelease.SetResult(true);
		}

		#endregion
	}
}