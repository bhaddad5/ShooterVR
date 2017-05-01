using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : HoldableObject
{
	public float damage;
	public float damageRadius;
	public int timeToDetination;

	protected override void HandleDrop()
	{
		Debug.Log("DROPPED GRENADE");
		StartCoroutine(GrenadeTimeout());
	}

	private IEnumerator GrenadeTimeout()
	{
		yield return new WaitForSeconds(timeToDetination);
		DetonateGrenade();
	}

	private void DetonateGrenade()
	{
		var colliders = Physics.OverlapSphere(transform.position, damageRadius);

		foreach (Collider c in colliders)
		{
			if (c.GetComponent<DamageReciever>())
			{
				c.GetComponent<DamageReciever>().TakeDamage(damage);
			}
		}

		Destroy(gameObject);
	}
}
