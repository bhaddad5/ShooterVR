using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletons
{
	private static GrabbingHand grabbingHand;
	public static GrabbingHand GrabbingHand()
	{
		if (grabbingHand == null)
		{
			grabbingHand = GameObject.Find("[CameraRig]").GetComponentInChildren<GrabbingHand>();
		}
		return grabbingHand;
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
