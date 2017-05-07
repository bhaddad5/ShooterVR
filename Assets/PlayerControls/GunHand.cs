using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHand : Hand
{
	public GameObject StartingGunPrefab;
	
	private Gun currGun;
	// Use this for initialization
	protected override void Setup()
	{
		gripDown += () =>
		{
			if(currGun != null)
			{
				currGun.TryEjectClip();
			}
		};

		gripUp += () =>	{};

		var startGun = Instantiate(StartingGunPrefab);
		PickUpGun(startGun.GetComponent<Gun>());
	}

	public void PickUpGun(Gun gun)
	{
		currGun = gun;
		currGun.PickUp();
		currGun.transform.GetComponent<Rigidbody>().isKinematic = true;
		currGun.transform.GetComponent<Rigidbody>().useGravity = false;
		currGun.transform.SetParent(transform);
		currGun.transform.localPosition = Vector3.zero;
		currGun.transform.localEulerAngles = Vector3.zero;
	}

	public void PutDownGun(Gun gun)
	{
		currGun.transform.GetComponent<Rigidbody>().isKinematic = false;
		currGun.transform.GetComponent<Rigidbody>().useGravity = true;
		currGun.transform.parent = null;
		currGun.PutDown();
		currGun = null;
	}

	public Gun getCurrGun()
	{
		return currGun;
	}
}
