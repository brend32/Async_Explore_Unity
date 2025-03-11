using System;
using System.Threading.Tasks;
using UnityEngine;

public class CallbackExample : MonoBehaviour
{
	[EasyButtons.Button]
	public void StartTask()
	{
		// Can go pretty deep
		// Unable to easily wait multiple tasks
		// and cancel any of them
		// Complex error handling
		WaitForUserAction(() =>
		{
			Debug.Log("User made action");
			WebRequest(resourceId =>
			{
				Debug.Log($"Request result: {resourceId}");
				LoadAssets(resourceId, asset =>
				{
					Debug.Log($"Your asset: {asset}");
				}, e =>
				{
					Debug.LogError(e);
				});
			}, e =>
			{
				Debug.LogError(e);
			});
		});
	}

	public async void WaitForUserAction(Action onAction)
	{
		// Faking user reaction
		await Task.Delay(1000);
		onAction.Invoke();
	}

	// Async to fake delay
	public async void WebRequest(Action<string> success, Action<string> fail)
	{
		await Task.Delay(4000);
		success.Invoke("Web data");
	}

	// Can load assets and block the main thread
	// but image we don't want it
	public async void LoadAssets(string resourceName, Action<string> success, Action<string> fail)
	{
		await Task.Delay(1000);
		success.Invoke("Your super object");
	}
}