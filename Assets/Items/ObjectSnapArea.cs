using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapArea : MonoBehaviour
{
	public HoldableObject.SnapType snapType;
	public HoldableObject currSnappedObj { get; set; }

	public HoldableObject startingAttachedObject;

	void Start()
	{
		if (startingAttachedObject != null)
		{
			GameObject startingObj = Instantiate(startingAttachedObject.gameObject);
			startingObj.GetComponent<HoldableObject>().SnapToObject(this);
		}
	}
}
