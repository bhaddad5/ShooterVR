using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : HoldableObject
{
	public int bulletCount;

	protected override void HandleSnap(ObjectSnapArea objSnappedTo)
	{
		objSnappedTo.transform.parent.GetComponent<Gun>().currMag = this;
	}

	protected override void HandlePickup()
	{
		if (Singletons.GunHand().getCurrGun().currMag.Equals(this))
		{
			Singletons.GunHand().getCurrGun().currMag = null;
		}
	}
}
