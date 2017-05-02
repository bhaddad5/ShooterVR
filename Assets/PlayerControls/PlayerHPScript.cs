using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : DamageReciever
{
	public float HPRegenPerSecond;
	public MeshRenderer playerHPOverlay;

	private float maxHP;
	// Use this for initialization
	void Start ()
	{
		maxHP = Health;
		StartCoroutine(RegenHealth());
	}
	
	// Update is called once per frame
	void Update ()
	{
		Color c = playerHPOverlay.material.color;
		c.a = Mathf.Min(1f - Health/maxHP, .8f);
		playerHPOverlay.material.color = c;
	}

	private IEnumerator RegenHealth()
	{
		while (true)
		{
			if (Health < maxHP)
			{
				Health = Mathf.Min(Health + HPRegenPerSecond, maxHP);
			}
			yield return new WaitForSeconds(1);
		}
	}
}
