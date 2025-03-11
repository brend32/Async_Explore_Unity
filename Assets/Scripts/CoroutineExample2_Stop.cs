using System;
using System.Collections;
using UnityEngine;

public class CoroutineExample2_Stop : MonoBehaviour
{
	public bool CoroutineIsNull;
	public bool Ended;
	private Coroutine _coroutine;

	public void Update()
	{
		CoroutineIsNull = _coroutine == null;
	}

	[EasyButtons.Button]
	public void StartCoroutine()
	{
		// Save a coroutine or returned IEnumerator
		// to be able to cancel it
		_coroutine = StartCoroutine(Work());
	}

	[EasyButtons.Button]
	public void StopCoroutine()
	{
		// You can't stop null coroutine
		// so check before you do so
		if (_coroutine != null)
		{
			// A coroutine can only be stopped at
			// yield return points
			// The code before a yield return point
			// will always be executed
			StopCoroutine(_coroutine);
		}
	}

	[EasyButtons.Button]
	public void DisableObject()
	{
		// All coroutines hosted by a game object
		// are stopped when it disables
		
		// Coroutines don't resume execution after
		// the game object become active again
		
		gameObject.SetActive(false);
	}

	[EasyButtons.Button]
	public void DisableMono()
	{
		// Doesn't affect execution
		enabled = false;
	}

	[EasyButtons.Button]
	public void IsCoroutineNull()
	{
		// There are no way to check if coroutine
		// is finished execution
		// Run them on a mono that always alive
		// and handle a running state by yourself
		Debug.Log(_coroutine == null);
	}

	public IEnumerator Work()
	{
		Ended = false;
		Debug.Log("Started");
		
		for (int i = 0; i < 10; i++)
		{
			Debug.Log(i);
			yield return new WaitForSeconds(1);
		}
		
		Debug.Log("Finished");
		Ended = true;
	}
}