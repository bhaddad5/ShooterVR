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
		if (transform.parent.GetComponent<ObjectSnapArea>())
			transform.parent.GetComponent<ObjectSnapArea>().currSnappedObj = null;
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().isKinematic = true;
		transform.SetParent(Singletons.GrabbingHand().transform);
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
		if (snp != null && snp.snapType==snapType && snp.currSnappedObj == null && !transform.parent.GetComponent<ObjectSnapArea>())
		{
			snp.currSnappedObj = this;
			transform.SetParent(other.transform, true);
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = new Vector3(1/transform.parent.lossyScale.x, 1 / transform.parent.lossyScale.y, 1 / transform.parent.lossyScale.z);
			HandleSnap(snp);

			Singletons.GrabbingHand().HandleSnapAway(this);
		}
	}

	protected virtual void HandleSnap(ObjectSnapArea objSnappedTo) {}

	protected virtual void HandlePickup() { }

	protected virtual void HandleDrop() { }
}

