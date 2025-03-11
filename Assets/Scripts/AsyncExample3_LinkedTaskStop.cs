using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample3_LinkedTaskStop : MonoBehaviour
{
	private CancellationTokenSource _tokenSourceTop;
	private CancellationTokenSource _tokenSourceInner;
	
	[EasyButtons.Button]
	public async void StartTask()
	{
		await TopTask();
	}

	public async Task TopTask()
	{
		_tokenSourceTop = new CancellationTokenSource();

		CancellationToken token = _tokenSourceTop.Token;
		
		Debug.Log("Top task - started");
		await InnerTask(token);
		// Throw exception or stop execution
		// if cancel is requested
		token.ThrowIfCancellationRequested();
		Debug.Log("Top task - ended");
	}

	public async Task InnerTask(CancellationToken cancellationToken = default)
	{
		// Dispose linked token source to avoid memory leaks
		using (_tokenSourceInner = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
		{
			Debug.Log("Inner task - started");
			try
			{
				for (int i = 0; i < 5; i++)
				{
					Debug.Log($"Inner task - {i}");
					await Task.Delay(1000, _tokenSourceInner.Token);
				}
			}
			// Exception are propagated upwards like in regular methods
			catch (OperationCanceledException)
			{
				Debug.Log($"Inner task - cancelled");
			}
			Debug.Log("Inner task - ended");
		}
	}

	[EasyButtons.Button]
	public void StopTaskTop()
	{
		// Fail top task
		// Inner task is cancelled with it
		_tokenSourceTop?.Cancel();
		_tokenSourceTop = null;
	}

	[EasyButtons.Button]
	public void StopTaskInner()
	{
		// Fail inner task
		// Top task is not affected
		// And can continue it execution
		// if it is possible
		_tokenSourceInner?.Cancel();
		_tokenSourceInner = null;
	}
}