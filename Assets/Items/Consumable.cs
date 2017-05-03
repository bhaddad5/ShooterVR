using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : HoldableObject
{
	protected override void HandleTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerHPScript>())
		{
			foreach (BoxCollider col in GetComponentsInChildren<BoxCollider>())
			{
				col.enabled = false;
			}
			foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			{
				mr.enabled = false;
			}

			ExecuteConsumptionEffect();
		}
	}

	protected virtual void ExecuteConsumptionEffect() { }
}
