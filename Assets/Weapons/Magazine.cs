using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : HoldableObject
{
	public int bulletCount;
	public AudioClip ReloadSound;

	protected override void HandleSnap(ObjectSnapArea objSnappedTo)
	{
		objSnappedTo.transform.parent.parent.GetComponent<Gun>().currMag = this;
		GetComponent<AudioSource>().clip = ReloadSound;
		GetComponent<AudioSource>().Play();
	}

	protected override void HandlePickup()
	{
		if (Singletons.GunHand().getCurrGun().currMag != null && Singletons.GunHand().getCurrGun().currMag.Equals(this))
		{
			Singletons.GunHand().getCurrGun().currMag = null;
		}
	}
}
