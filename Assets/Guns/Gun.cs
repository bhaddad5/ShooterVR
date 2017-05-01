using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject MagazinePrefab;
	public GameObject BulletPrefab;
	public GameObject CasingPrefab;
	public Transform BulletSpawnPoint;
	public Transform CasingSpawnPoint;

	public float bulletDamage;
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
			FireBullet(BulletPrefab, BulletSpawnPoint, bulletDamage, bulletSpeed);
			FireCasing(CasingPrefab, CasingSpawnPoint);
			currMag.bulletCount--;
			gunHand.TriggerHaptic();
			yield return new WaitForSeconds(bulletWaitTimeS);
		}
	}

	public static void FireBullet(GameObject bulletPrefab, Transform bulletSpawnPoint, float dmg, float speed)
	{
		Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
		bullet.transform.position = bulletSpawnPoint.position;
		bullet.transform.eulerAngles = bulletSpawnPoint.eulerAngles;
		bullet.damage = dmg;
		bullet.speed = speed;
	}

	public static void FireCasing(GameObject casingPrefab, Transform casingSpawnPoint)
	{
		float casingSpeed = 100f;
		Casing casing = Instantiate(casingPrefab).GetComponent<Casing>();
		casing.transform.position = casingSpawnPoint.position;
		casing.transform.eulerAngles = casingSpawnPoint.eulerAngles;
		casing.GetComponent<Rigidbody>().AddForce(casing.transform.right.normalized * casingSpeed);
	}
}
