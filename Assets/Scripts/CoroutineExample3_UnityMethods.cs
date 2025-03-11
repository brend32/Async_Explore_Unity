using System;
using System.Collections;
using UnityEngine;

public class CoroutineExample3_UnityMethods : MonoBehaviour
{
	// Some methods can return IEnumarator
	// and they behave like coroutines
	public IEnumerator Start()
	{
		Debug.Log("Start - called");
		yield return new WaitForSeconds(3);
		Debug.Log("Start - ended");
	}
}