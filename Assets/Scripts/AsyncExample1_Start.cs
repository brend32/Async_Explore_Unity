using System.Threading.Tasks;
using UnityEngine;

public class AsyncExample1_Start : MonoBehaviour
{
	// Async method must be marked with
	// async keyword
	// If async method should not be awaitable
	// return void
	// Instead use Task or Task<T>
	[EasyButtons.Button]
	public async void StartTask()
	{
		Debug.Log("Start");
		// Return control back and 
		// wait for other task to finish
		await Task.Delay(3000);
		Debug.Log("Ended");
	}
}