using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : HoldableObject
{
	public int bulletCount;
	public AudioClip ReloadSound;

	protected override void HandleSnap(ObjectSnapArea objSnappedTo)
	{
		GetComponent<AudioSource>().clip = ReloadSound;
		GetComponent<AudioSource>().Play();
	}
}
