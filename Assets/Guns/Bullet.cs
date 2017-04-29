using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage;

	public float speed;
	public Vector3 direction { get; set; }

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(direction*speed);
	}
}
