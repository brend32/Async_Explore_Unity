using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample2_Stop : MonoBehaviour
{
	private CancellationTokenSource _tokenSource;
	
	[EasyButtons.Button]
	public async void StartTask()
	{
		// To stop a task you need a CancellationToken
		// Use CancellationTokenSource to acquire one 
		// Tasks not stop their execution when unity exits play mode
		// Use Application.exitCancellationToken, to do so
		_tokenSource = new CancellationTokenSource();
		
		// Pass token to all child tasks
		// to be able to cancel execution
		CancellationToken token = _tokenSource.Token;
		
		Debug.Log("Start");
		for (int i = 0; i < 10; i++)
		{
			Debug.Log(i);
			await Task.Delay(1000, token);
		}
		Debug.Log("Ended");
	}

	[EasyButtons.Button]
	public void StopTask()
	{
		// Trigger cancellation
		_tokenSource?.Cancel();
		_tokenSource = null;
	}
}