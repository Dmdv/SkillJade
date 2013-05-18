﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using TaskExtensions = DataAccess.Exceptions.TaskExtensions;

namespace DataAccess.Helpers
{
	internal static class Retry
	{
		public static Task<T> Do<T>(Func<T> action, IRetryPolicy retryPolicy, CancellationToken cancellationToken)
		{
			return Do(() => Task<T>.Factory.StartNew(action), retryPolicy, cancellationToken);
		}

		public static Task<T> Do<T>(Func<Task<T>> taskGetter, IRetryPolicy retryPolicy, CancellationToken cancellationToken)
		{
			const int RetryCount = 0;
			var tcs = new TaskCompletionSource<T>();
			var task = tcs.Task;
			taskGetter()
				.ContinueWith(finishedTask => TaskRetry(tcs, taskGetter, finishedTask, cancellationToken, retryPolicy, RetryCount));
			return task;
		}

		private static void TaskRetry<T>(
			TaskCompletionSource<T> taskCompletionSource,
			Func<Task<T>> taskGetter,
			Task<T> finishedTask,
			CancellationToken cancellationToken,
			IRetryPolicy policy,
			int retryCount)
		{
			if (finishedTask.Exception != null)
			{
				TimeSpan delay;

				// TODO: Init
				var statusCode = 0;
				// TODO: Init
				var operationContext = new OperationContext();

				if (policy.ShouldRetry(retryCount, 0, finishedTask.Exception.InnerExceptions[0], out delay, operationContext))
				{
					retryCount++;

					cancellationToken.ThrowIfCancellationRequested();

					TaskExtensions.Delay(delay)
					              .ContinueWith(_ =>
					                            taskGetter()
						                            .ContinueWith(x =>
						                                          TaskRetry(taskCompletionSource, taskGetter, x, cancellationToken, policy, retryCount)));
					return;
				}

				taskCompletionSource.SetException(finishedTask.Exception.InnerExceptions);
				return;
			}

			taskCompletionSource.SetResult(finishedTask.Result);
		}
	}
}