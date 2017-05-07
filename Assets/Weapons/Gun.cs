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
	public AudioClip BulletSound;
	public AudioClip OutOfAmmoSound;
	public ObjectSnapArea magSnapArea;

	public float bulletDamage;
	public float bulletWaitTimeS;
	public float bulletSpeed;
	public float bulletInaccuracy;

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
		var currMag = magSnapArea.currSnappedObj as Magazine;
		while (firing && currMag != null && currMag.bulletCount > 0)
		{
			FireBullet(BulletPrefab, BulletSpawnPoint, bulletDamage, bulletSpeed, bulletInaccuracy);
			FireCasing(CasingPrefab, CasingSpawnPoint);
			source.clip = BulletSound;
			source.Play();
			currMag.bulletCount--;
			Singletons.GunHand().TriggerHaptic();
			yield return new WaitForSeconds(bulletWaitTimeS);
		}
		if (currMag != null && currMag.bulletCount == 0)
		{
			source.clip = OutOfAmmoSound;
			source.Play();
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

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "PlayerInteractible")
		{
			Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
		}
	}

	public void TryEjectClip()
	{
		if (magSnapArea.currSnappedObj != null)
		{
			magSnapArea.currSnappedObj.DropObject();
			magSnapArea.currSnappedObj = null;
		}
	}
}
