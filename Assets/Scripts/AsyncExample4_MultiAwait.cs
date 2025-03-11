using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample4_MultiAwait : MonoBehaviour
{
	private Task<int> _task;
	
	[EasyButtons.Button]
	public async void StartTask()
	{
		Debug.Log("Start");

		if (_task == null)
		{
			Debug.Log("Starting task...");
			// Saving task
			_task = SomeLongTask();
		}
	
		// You can check task status to find out
		// if it is completed or still running
		Debug.Log($"Task status: {_task.Status}");
		Debug.Log($"Result: {await _task}");
	}

	[EasyButtons.Button]
	public void ClearField()
	{
		_task = null;
	}

	// Task will run once and then
	// you nearly (can be delayed to one frame see docs https://docs.unity3d.com/Manual/async-awaitable-introduction.html) instantly 
	public async Task<int> SomeLongTask()
	{
		int sum = 0;
		for (int i = 0; i < 10; i++)
		{
			sum += i;
			Debug.Log($"Work {i}");
			await Task.Delay(1000);
		}
		
		Debug.Log("Finished");
		return sum;
	}
}