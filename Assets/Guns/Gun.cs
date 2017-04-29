using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject MagazinePrefab;

	public Magazine currMag { get; set; }
	private GunHand gunHand;
	private float bulletWaitTileS = 0.1f;

	// Use this for initialization
	void Start ()
	{
		gunHand = transform.parent.GetComponent<GunHand>();
		gunHand.triggerDown += () => StartCoroutine(fireBullets());
		gunHand.triggerUp += () => StopCoroutine(fireBullets());
	}

	private IEnumerator fireBullets()
	{
		while (currMag != null && currMag.bulletCount > 0)
		{
			FireBullet();
			yield return new WaitForSeconds(bulletWaitTileS);
		}
	}

	private void FireBullet()
	{
		Debug.Log("fire");
		currMag.bulletCount--;
	}
}
