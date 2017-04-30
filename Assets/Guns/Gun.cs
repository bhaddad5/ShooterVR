using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject MagazinePrefab;
	public GameObject BulletPrefab;
	public Transform bulletSpawnPoint;

	public float damage;
	public float bulletWaitTimeS;
	public float bulletSpeed;

	public Magazine currMag { get; set; }
	private GunHand gunHand;

	private bool firing = false;

	// Use this for initialization
	void Start ()
	{
		gunHand = transform.parent.GetComponent<GunHand>();
		gunHand.triggerDown += () =>
		{
			firing = true;
			StartCoroutine(fireBullets());
		};
		gunHand.triggerUp += () => firing = false;
	}

	private IEnumerator fireBullets()
	{
		while (firing && currMag != null && currMag.bulletCount > 0)
		{
			FireBullet();
			yield return new WaitForSeconds(bulletWaitTimeS);
		}
	}

	private void FireBullet()
	{
		currMag.bulletCount--;
		gunHand.TriggerHaptic();
		Bullet bullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
		bullet.transform.position = bulletSpawnPoint.position;
		bullet.transform.eulerAngles = bulletSpawnPoint.eulerAngles;
		bullet.damage = damage;
		bullet.speed = bulletSpeed;
	}
}
