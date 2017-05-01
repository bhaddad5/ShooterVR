using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
	public float health;

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			if (!gameObject.GetComponent<Camera>())
				Destroy(gameObject);
		}
	}
}
