using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
	public float Health;
	private bool destroying = false;

	public void TakeDamage(float damage)
	{
		Health = Mathf.Max(Health - damage, 0);
		if (Health <= 0)
		{
			if(!destroying && !gameObject.GetComponent<Camera>())
			{
				destroying = true;
				GetComponent<ParticleSystem>().Play();
				GetComponent<AudioSource>().Play();
				StartCoroutine(DestroyAfterParticles());
			}
		}
	}

	private IEnumerator DestroyAfterParticles()
	{
		while (GetComponent<ParticleSystem>().isPlaying && GetComponent<AudioSource>().isPlaying)
		{
			yield return null;
		}
		Destroy(gameObject);
	}
}
