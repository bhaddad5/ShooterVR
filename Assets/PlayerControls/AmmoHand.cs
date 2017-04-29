using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AmmoHand : Hand
{
	public GunHand gunHand;

	public GameObject currHeldMag { get; set; }

	// Use this for initialization
	void Start()
	{
		triggerDown += () =>
		{
			Debug.Log("trigger down");
			if (behindPlayer())
			{
				currHeldMag = Instantiate(gunHand.StartingGunPrefab.GetComponent<Gun>().MagazinePrefab);
				currHeldMag.transform.SetParent(transform);
				currHeldMag.transform.localPosition = new Vector3();
				currHeldMag.transform.localEulerAngles = Vector3.zero;
			}
		};
	}

	private bool behindPlayer()
	{
		Vector3 headToHand = transform.position - Camera.main.transform.position;
		return Vector3.Angle(headToHand, Camera.main.transform.forward) > 70;
	}
}