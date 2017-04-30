using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
	public float health;

	void OnTriggerEnter(Collider other)
	{
		var bullet = other.GetComponent<Bullet>();
		if (bullet != null)
		{
			health -= bullet.damage;
			Destroy(bullet.gameObject);
			if (health <= 0)
			{
				if(!gameObject.GetComponent<Camera>())
					Destroy(gameObject);
				else Debug.Log("DEATH!!!");
			}
		}
	}
}
