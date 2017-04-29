using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHand : Hand
{
	public GameObject StartingGunPrefab;

	
	private GameObject currGun;
	// Use this for initialization
	void Start ()
	{
		currGun = Instantiate(StartingGunPrefab);
		currGun.transform.SetParent(transform, true);
		currGun.transform.localEulerAngles = new Vector3(-40f, 0f, 0f);
	}

	
}
