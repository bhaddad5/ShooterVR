using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GrabbingHand : Hand
{
	private HoldableObject currHeldObject;
	private List<HoldableObject> hoveredObjects = new List<HoldableObject>();

	private Vector3 lastHandPos;
	private Vector3 currForceVector;

	// Use this for initialization
	protected override void Setup()
	{
		triggerDown += () =>
		{
			if (behindPlayer())
			{
				currHeldObject = Instantiate(Singletons.GunHand().getCurrGun().MagazinePrefab).GetComponent<HoldableObject>();
				currHeldObject.transform.SetParent(transform);
				currHeldObject.transform.localPosition = new Vector3();
				currHeldObject.transform.localEulerAngles = Vector3.zero;
			}
			else if (currHeldObject == null && hoveredObjects.Count > 0)
			{
				hoveredObjects[0].PickupObject();
				currHeldObject = hoveredObjects[0];
				hoveredObjects.RemoveAt(0);
			}
		};

		triggerUp += () =>
		{
			if (currHeldObject != null)
			{
				currHeldObject.DropObject();
				if (currHeldObject.GetComponent<Rigidbody>())
				{
					currHeldObject.GetComponent<Rigidbody>().AddForce(currForceVector);
				}
				currHeldObject = null;
			}
		};
	}

	protected override void OnUpdate()
	{
		currForceVector = (transform.position - lastHandPos) * 30000;
		lastHandPos = transform.position;
	}

	private bool behindPlayer()
	{
		Vector3 headToHand = transform.position - Camera.main.transform.position;
		return Vector3.Angle(headToHand, Camera.main.transform.forward) > 70;
	}

	void OnTriggerEnter(Collider other)
	{
		var ho = other.GetComponent<HoldableObject>();
		if (ho != null && !hoveredObjects.Contains(ho))
		{
			hoveredObjects.Add(ho);
		}
	}

	void OnTriggerExit(Collider other)
	{
		var ho = other.GetComponent<HoldableObject>();
		if (ho != null && hoveredObjects.Contains(ho))
		{
			hoveredObjects.Remove(ho);
		}
	}

	public void HandleSnapAway(HoldableObject obj)
	{
		if (currHeldObject != null && currHeldObject.Equals(obj))
		{
			currHeldObject = null;
		}
	}
}