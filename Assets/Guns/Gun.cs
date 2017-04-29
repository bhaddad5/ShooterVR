using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject MagazinePrefab;
	public GameObject BulletPrefab;

	public Magazine currMag { get; set; }
	private GunHand gunHand;
	private float bulletWaitTileS = 0.4f;

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
		currMag.bulletCount--;
		gunHand.TriggerHaptic();
		GameObject bullet = Instantiate(BulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.eulerAngles = transform.forward;
		bullet.GetComponent<Bullet>().direction = transform.forward.normalized;
	}
}
