using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample7_NonAsyncToAsync : MonoBehaviour
{
	private TaskCompletionSource<int> _taskCompletion;
	
	[EasyButtons.Button]
	public async void StartTask()
	{
		Debug.Log("Start");
		await NonAsyncTask();
		Debug.Log("End");
	}

	public Task NonAsyncTask()
	{
		// Creates source that controls task
		// which can be resolved later
		_taskCompletion = new TaskCompletionSource<int>();

		return _taskCompletion.Task;
	}

	[EasyButtons.Button]
	public void FinishWithValue(int value = 10)
	{
		_taskCompletion.SetResult(value);
	}

	[EasyButtons.Button]
	public void FinishCanceled()
	{
		_taskCompletion.SetCanceled();
	}

	[EasyButtons.Button]
	public void FinishException()
	{
		_taskCompletion.SetException(new Exception("User wanted it to crash"));
	}

}