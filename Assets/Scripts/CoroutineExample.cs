using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
	[EasyButtons.Button]
	public void StartTask()
	{
		// Can be easily chained
		// Can't return result that leads to wrappers
		// Work only in main thread
		// Can't easily handle exceptions
		StartCoroutine(Work());
		
		IEnumerator Work()
		{
			yield return WaitForUserAction();
			Debug.Log("User made action");

			var request = WebRequest();
			yield return request.Wait;

			if (string.IsNullOrEmpty(request.Error))
			{
				Debug.LogError(request.Error);
				yield break;
			}

			Debug.Log($"Request result: {request.Result}");
			request = LoadAssets(request.Result);
			
			if (string.IsNullOrEmpty(request.Error))
			{
				Debug.LogError(request.Error);
				yield break;
			}
			Debug.Log($"Your asset: {request.Result}");
		}
	}

	public IEnumerator WaitForUserAction()
	{
		// Faking user reaction
		yield return new WaitForSeconds(1);
	}

	// Async to fake delay
	public Request WebRequest()
	{
		// No way to return value
		// Need to write a wrapper class
		return new Request(Work);

		IEnumerator Work(Request request)
		{
			yield return new WaitForSeconds(1);
			request.Result = "Your super object";
		}
	}

	// Can load assets and block the main thread
	// but image we don't want it
	public Request LoadAssets(string resourceName)
	{
		// No way to return value
		// Need to write a wrapper class
		return new Request(Work);

		IEnumerator Work(Request request)
		{
			yield return new WaitForSeconds(1);
			request.Result = "Your super object";
		}
	}

	public class Request
	{
		public IEnumerator Wait { get; }
		public string Result { get; set; }
		public string Error { get; set; }

		public Request(Func<Request, IEnumerator> work)
		{
			Wait = work(this);
		}
	}
}