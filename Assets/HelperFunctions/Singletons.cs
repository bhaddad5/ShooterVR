using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletons
{
	private static AmmoHand ammoHand;
	public static AmmoHand AmmoHand()
	{
		if (ammoHand == null)
		{
			ammoHand = GameObject.Find("[CameraRig]").GetComponentInChildren<AmmoHand>();
		}
		return ammoHand;
	}

	private static GunHand gunHand;
	public static GunHand GunHand()
	{
		if (gunHand == null)
		{
			gunHand = GameObject.Find("[CameraRig]").GetComponentInChildren<GunHand>();
		}
		return gunHand;
	}
}
