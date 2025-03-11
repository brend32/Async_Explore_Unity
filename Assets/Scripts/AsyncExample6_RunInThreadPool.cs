using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample6_RunInThreadPool : MonoBehaviour
{
	private void Start()
	{
		Application.targetFrameRate = -1;
	}

	[EasyButtons.Button]
	public async void MainThread()
	{
		Debug.Log("Start");
		await HeavyLoadTask();
		Debug.Log("End");
	}

	[EasyButtons.Button]
	public void ThreadPool()
	{
		// Task.Run starts a task in the thread pool
		Task.Run(async () =>
		{
			Debug.Log("Start");
			await HeavyLoadTask();
			Debug.Log("End");	
		});
	}

	public async Task HeavyLoadTask()
	{
		// Outputs current thread id
		// Most of the Unities APIs can only be called from 
		// the main thread
		// If you don't force a thread switch, the code will be executed
		// in the same thread as a caller
		Debug.Log(Thread.CurrentThread.ManagedThreadId);
		await Task.Delay(1000);
		HeavyLoad();

		// Wait for thread to switch to the main thread
		// The code after await will be called in the main thread
		// Avoid heavy calculations in the main thread
		// to avoid blocking and frame rate drops
		await Awaitable.MainThreadAsync();
		Debug.Log(Thread.CurrentThread.ManagedThreadId);
		await Task.Delay(1000);
		HeavyLoad();

		await Awaitable.BackgroundThreadAsync();
		Debug.Log(Thread.CurrentThread.ManagedThreadId);
		await Task.Delay(1000);
		HeavyLoad();
	}

	private void HeavyLoad()
	{
		// Some expensive callculations
		long sum = 0;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 1000000000; j++)
			{
				sum++;
			}	
		}
	}

}