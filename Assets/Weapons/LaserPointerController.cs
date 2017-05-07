using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour
{
	Vector3 startScale;
	float globalStartScale;
	void Start()
	{
		startScale = transform.localScale;
		globalStartScale = transform.lossyScale.z;
	}
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		if(Physics.Raycast(new Ray(transform.position, transform.forward), out hit, float.MaxValue))
		{
			//TODO: Why x10?
			transform.localScale = new Vector3(startScale.x, startScale.y, (hit.distance)*10);
		}
		else transform.localScale = new Vector3(startScale.x, startScale.y, 1000f);
	}
}
