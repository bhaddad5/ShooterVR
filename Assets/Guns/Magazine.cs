using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
	public int bulletCount;

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.GetComponent<Gun>())
		{
			transform.SetParent(other.transform);
			transform.localPosition = new Vector3(0f, 0f, 0f);
			transform.localEulerAngles = Vector3.zero;
			other.transform.parent.GetComponent<Gun>().currMag = this;
		}
	}
}
