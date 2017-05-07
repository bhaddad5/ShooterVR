using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : HoldableObject
{
	public float damage;
	public float damageRadius;
	public int timeToDetination;
	public Rigidbody pin;
	public ParticleSystem particles;

	protected override void HandleDrop()
	{
		pin.transform.parent = null;
		pin.useGravity = true;
		pin.isKinematic = false;
		pin.AddForce(pin.transform.up.normalized * 100f);

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
		GetComponent<AudioSource>().Play();
		particles.Play();

		StartCoroutine(DestroyAfterParticles());
	}

	private IEnumerator DestroyAfterParticles()
	{
		while (particles.isPlaying)
			yield return null;
		while (GetComponent<AudioSource>().isPlaying)
			yield return null;
		Destroy(gameObject);
	}
}
