using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
	public enum SnapType
	{
		None,
		Magazine,
		WeaponInventory
	}

	public SnapType snapType;

	void Start()
	{
		Setup();
	}

	protected virtual void Setup() { }

	public void PickupObject()
	{
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;
		transform.SetParent(Singletons.AmmoHand().transform);
		HandlePickup();
	}

	public void DropObject()
	{
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;
		transform.SetParent(null);
		HandleDrop();
	}

	void OnTriggerEnter(Collider other)
	{
		var snp = other.GetComponent<ObjectSnapArea>();
		if (snp != null && snp.snapType==snapType)
		{
			transform.SetParent(other.transform);
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			HandleSnap(snp);
		}
	}

	protected virtual void HandleSnap(ObjectSnapArea objSnappedTo) {}

	protected virtual void HandlePickup() { }

	protected virtual void HandleDrop() { }
}

