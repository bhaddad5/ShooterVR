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
	private float pickupSnapTimeout = .2f;
	private float lastPickupTime;

	void Start()
	{
		Setup();
	}

	protected virtual void Setup() { }

	public void PickupObject()
	{
		if (transform.parent != null && transform.parent.GetComponent<ObjectSnapArea>())
			transform.parent.GetComponent<ObjectSnapArea>().currSnappedObj = null;
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().isKinematic = true;
		transform.SetParent(Singletons.GrabbingHand().transform);
		HandlePickup();
		lastPickupTime = Time.realtimeSinceStartup;
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
		if (snp != null && 
			snp.snapType==snapType && 
			snp.currSnappedObj == null && 
			transform.parent != null && 
			!transform.parent.GetComponent<ObjectSnapArea>() &&
			pickupSnapTimeout + lastPickupTime <= Time.realtimeSinceStartup)
		{
			SnapToObject(snp);
			Singletons.GrabbingHand().HandleSnapAway(this);
		}

		HandleTriggerEnter(other);
	}

	protected virtual void HandleTriggerEnter(Collider other) { }

	public void SnapToObject(ObjectSnapArea snp)
	{
		snp.currSnappedObj = this;
		transform.SetParent(snp.transform, true);
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
		transform.localScale = new Vector3(1 / transform.parent.lossyScale.x, 1 / transform.parent.lossyScale.y, 1 / transform.parent.lossyScale.z);
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().isKinematic = true;
		HandleSnap(snp);
	}

	protected virtual void HandleSnap(ObjectSnapArea objSnappedTo) {}

	protected virtual void HandlePickup() { }

	protected virtual void HandleDrop() { }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "PlayerInteractible")
		{
			Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
		}
	}
}

