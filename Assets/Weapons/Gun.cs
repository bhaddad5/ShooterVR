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
	public float bulletInaccuracy;

	public Magazine currMag { get; set; }

	private bool firing = false;
	private AudioSource source;
	private List<Gun> hoveredGuns = new List<Gun>();

	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource>();
	}

	public void PickUp()
	{
		Singletons.GunHand().triggerDown += HandleTriggerDown;
		Singletons.GunHand().triggerUp += HandleTriggerUp;
	}

	public void PutDown()
	{
		Singletons.GunHand().triggerDown -= HandleTriggerDown;
		Singletons.GunHand().triggerUp -= HandleTriggerUp;
	}

	private void HandleTriggerDown()
	{
		if (hoveredGuns.Count > 0)
		{
			Singletons.GunHand().PutDownGun(this);
			Singletons.GunHand().PickUpGun(hoveredGuns[0]);
		}
		else
		{
			firing = true;
			StartCoroutine(fireBullets());
		}
	}

	private void HandleTriggerUp()
	{
		firing = false;

	}

	private IEnumerator fireBullets()
	{
		while (firing && currMag != null && currMag.bulletCount > 0)
		{
			FireBullet(BulletPrefab, BulletSpawnPoint, bulletDamage, bulletSpeed, bulletInaccuracy);
			FireCasing(CasingPrefab, CasingSpawnPoint);
			source.Play();
			currMag.bulletCount--;
			Singletons.GunHand().TriggerHaptic();
			yield return new WaitForSeconds(bulletWaitTimeS);
		}
	}

	public static void FireBullet(GameObject bulletPrefab, Transform bulletSpawnPoint, float dmg, float speed, float inaccuracy)
	{
		Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
		bullet.transform.position = bulletSpawnPoint.position;
		Vector3 randOffset = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));
		bullet.transform.eulerAngles = bulletSpawnPoint.eulerAngles + randOffset;
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

	void OnTriggerEnter(Collider other)
	{
		var gun = other.GetComponent<Gun>();
		if (gun != null)
		{
			hoveredGuns.Add(gun);
		}
	}

	void OnTriggerExit(Collider other)
	{
		var gun = other.GetComponent<Gun>();
		if (gun != null && hoveredGuns.Contains(gun))
		{
			hoveredGuns.Remove(gun);
		}
	}
}
