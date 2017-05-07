using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage { get; set; }
	public float speed { get; set; }

	private const float bulletMaxTime = 10f;
	private float startTime;

	void Start()
	{
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, speed))
		{
			if(hit.transform.GetComponent<DamageReciever>())
				hit.transform.GetComponent<DamageReciever>().TakeDamage(damage);
			Destroy(gameObject);
		}

		transform.position += transform.forward.normalized*speed;

		if(Time.time >= startTime + bulletMaxTime)
			Destroy(gameObject);
	}
}
