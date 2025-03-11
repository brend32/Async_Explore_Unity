using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TaskExample : MonoBehaviour
{
	[EasyButtons.Button]
	public async void StartTask()
	{
		// Easy to use
		// Easy to chain or wait multiple at a time
		// Can be run in thread pool
		// Exception handling like in normal methods
		// Can be canceled
		// Can be awaited from different sources
		CancellationToken token = Application.exitCancellationToken;

		try
		{
			await WaitForUserAction(token);
			Debug.Log("User made action");

			var resourceId = await WebRequest(token);

			Debug.Log($"Request result: {resourceId}");
			var asset = await LoadAssets(resourceId, token);

			Debug.Log($"Your asset: {asset}");
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	public async Task WaitForUserAction(CancellationToken cancellationToken = default)
	{
		// Faking user reaction
		await Task.Delay(1000, cancellationToken);
	}

	// Async to fake delay
	public async Task<string> WebRequest(CancellationToken cancellationToken = default)
	{
		await Task.Delay(4000, cancellationToken);
		return "Your super object";
	}

	public async Task<string> LoadAssets(string resourceName, CancellationToken cancellationToken = default)
	{
		await Task.Delay(1000, cancellationToken);
		return "Your super object";
	}
}