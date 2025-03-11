using System.Collections;
using UnityEngine;

public class CoroutineExample1_Start : MonoBehaviour
{
	[EasyButtons.Button]
	public void StartCoroutine()
	{
		// Coroutines require a host to operate
		// StartCoroutine start coroutine
		// and attaches it to the game object 
		// to which the mono is attached
		StartCoroutine(Work());
	}

	public IEnumerator Work()
	{
		Debug.Log("Started");
		
		for (int i = 0; i < 10; i++)
		{
			Debug.Log(i);
			// Use yield return
			// to return control back
			yield return new WaitForSeconds(1);
		}
		
		Debug.Log("Finished");
	}
}