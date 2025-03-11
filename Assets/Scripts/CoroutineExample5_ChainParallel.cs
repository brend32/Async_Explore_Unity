using System;
using System.Collections;
using UnityEngine;

public class CoroutineExample5_ChainParallel : MonoBehaviour
{
	private void Awake()
	{
		Application.targetFrameRate = 5;
	}

	[EasyButtons.Button]
	public void StartChain()
	{
		StartCoroutine(Chain());
	}
	
	[EasyButtons.Button]
	public void StartParallel()
	{
		StartCoroutine(Parallel());
	}

	public IEnumerator Chain()
	{
		// Return task you want to wait
		// Execution resumes when task is finished
		Debug.Log("Chain - started");
		yield return Task(1, 2);
		yield return Task(2, 1);
		yield return Task(3, 4);
		Debug.Log("Chain - ended");
	}

	public IEnumerator Parallel()
	{
		// No easy way to wait for all of them
		Debug.Log("Parallel - started");
		StartCoroutine(Task(1, 2));
		StartCoroutine(Task(2, 1));
		StartCoroutine(Task(3, 4));
		Debug.Log("Parallel - ended");
		
		yield return null;
	}

	public IEnumerator Task(int i, int delay)
	{
		Debug.Log($"Task {i} - <color=green>started</color>");
		for (int j = 0; j < delay; j++)
		{
			yield return new WaitForSeconds(1);
			Debug.Log($"Task {i} - <color=yellow>work</color>");
		}
		Debug.Log($"Task {i} - <color=red>ended</color>");
	}
}