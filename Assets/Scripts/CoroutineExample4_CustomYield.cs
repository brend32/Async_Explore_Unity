using System;
using System.Collections;
using UnityEngine;

public class CoroutineExample4_CustomYield : MonoBehaviour
{
	private CustomYield _yield;

	[EasyButtons.Button]
	public void StartCoroutine()
	{
		// Reduce fps to reduce number of updates per second
		Application.targetFrameRate = 5;
		StartCoroutine(Work());
	}
	
	public IEnumerator Work()
	{
		Debug.Log("Work - called");
		// Same usage as a WaitForSeconds or similar
		yield return _yield = new CustomYield();
		Debug.Log("Work - ended");
	}

	[EasyButtons.Button]
	public void StopWaiting()
	{
		_yield.Waiting = false;
	}
	
	public class CustomYield : CustomYieldInstruction
	{
		public bool Waiting = true;
		
		public override bool keepWaiting
		{
			get
			{
				// Return false to end waiting
				// Reset yield before you return false
				// to allow reuse
				// Called every update
				Debug.Log("keepWaiting called");
				return Waiting;
			}
		}

		public override void Reset()
		{
			// Reset not called by unity side
			Debug.Log("Reset called");
		}
	}
}