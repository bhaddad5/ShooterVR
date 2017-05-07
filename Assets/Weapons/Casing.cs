using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
	private const float casingDestroyTime = 30f;

	void Start()
	{
		StartCoroutine(destoryCasing());
	}
	
	private IEnumerator destoryCasing()
	{
		yield return new WaitForSeconds(casingDestroyTime);
		Destroy(gameObject);
	}
}
