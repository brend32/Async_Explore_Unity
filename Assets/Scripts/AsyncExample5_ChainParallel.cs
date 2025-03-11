using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample5_ChainParallel : MonoBehaviour
{
	[EasyButtons.Button]
	public async void Parallel()
	{
		Debug.Log("Parallel - started");
		// Waits for all tasks to finish
		await Task.WhenAll(
			SomeTask(1, 1),
			SomeTask(2, 2),
			SomeTask(3, 5)
		);
		Debug.Log("Parallel - ended");
	}

	[EasyButtons.Button]
	public async void WhenAny()
	{
		Debug.Log("WhenAny - started");
		// Waits for any task to finish and returns completed one
		// Others will continue execution otherwise you cancel them
		Task<int> finishedTask = await Task.WhenAny(
			SomeTask(1, 1),
			SomeTask(2, 2),
			SomeTask(3, 5)
		);
		Debug.Log($"WhenAny - completed task id: {await finishedTask}");
		Debug.Log("WhenAny - ended");
	}

	[EasyButtons.Button]
	public async void Chain()
	{
		Debug.Log("Chain - started");
		// Wait them one by one
		await SomeTask(1, 1);
		await SomeTask(2, 2);
		await SomeTask(3, 5);
		Debug.Log("Chain - ended");
	}

	public async Task<int> SomeTask(int i, int delay)
	{
		Debug.Log($"Task {i} - <color=green>started</color>");
		for (int j = 0; j < delay; j++)
		{
			await Task.Delay(1000);
			Debug.Log($"Task {i} - <color=yellow>work</color>");
		}
		Debug.Log($"Task {i} - <color=red>ended</color>");
		return i;
	}
}