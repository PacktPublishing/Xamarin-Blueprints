// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncLock.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FileStorage.Portable.Threading
{
	using System;
	using System.Threading.Tasks;
	using System.Threading;

	/// <summary>
	/// Async lock.
	/// </summary>
	public class AsyncLock
	{
		#region Private Properties

		/// <summary>
		/// The m semaphore.
		/// </summary>
		private readonly AsyncSemaphore m_semaphore;

		/// <summary>
		/// The m releaser.
		/// </summary>
		private readonly Task<Releaser> m_releaser;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TelstraHealth.Portable.Threading.AsyncLock"/> class.
		/// </summary>
		public AsyncLock()
		{
			m_semaphore = new AsyncSemaphore(1);
			m_releaser = Task.FromResult(new Releaser(this));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Locks the async.
		/// </summary>
		/// <returns>The async.</returns>
		public Task<Releaser> LockAsync()
		{
			var wait = m_semaphore.WaitAsync();
			return wait.IsCompleted ?
				m_releaser :
				wait.ContinueWith((_, state) => new Releaser((AsyncLock)state),
					this, CancellationToken.None,
					TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		/// <summary>
		/// Releaser.
		/// </summary>
		public struct Releaser : IDisposable
		{
			private readonly AsyncLock m_toRelease;

			internal Releaser(AsyncLock toRelease) { m_toRelease = toRelease; }

			public void Dispose()
			{
				if (m_toRelease != null)
					m_toRelease.m_semaphore.Release();
			}
		}

		#endregion
	}
}