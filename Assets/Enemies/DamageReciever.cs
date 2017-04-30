using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
	public float health;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		var bullet = other.GetComponent<Bullet>();
		if (bullet != null)
		{
			health -= bullet.damage;
			Destroy(bullet.gameObject);
			if(health <=0)
				Destroy(gameObject);
		}
	}
}
