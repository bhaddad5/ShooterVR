using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloMo : Consumable
{
	public float affectDuration;
	public Color playerViewColor;

	protected override void ExecuteConsumptionEffect()
	{
		StartCoroutine(ExecuteSloMo());
	}

	private IEnumerator ExecuteSloMo()
	{
		float startTime = Time.realtimeSinceStartup;
		while (startTime + affectDuration >= Time.realtimeSinceStartup)
		{
			float percentDone = ((Time.realtimeSinceStartup - startTime)/affectDuration);

			if (percentDone < .75f)
				Time.timeScale = 0.3f;
			else Time.timeScale = ((Time.realtimeSinceStartup - startTime)/affectDuration);

			Color c = playerViewColor;
			c.a = 1 - Time.timeScale;
			Singletons.PlayerAffectsVisualizer().playerAffectsOverlay.material.color = c;

			yield return null;
		}
		Time.timeScale = 1f;
		Destroy(gameObject);
	}
}
