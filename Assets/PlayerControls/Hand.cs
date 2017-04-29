using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public Action triggerDown;
	public Action triggerUp;

	protected SteamVR_Controller.Device ctrl;

	void Start()
	{
		ctrl = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
		Debug.Log(gameObject.name + ", " + GetComponent<SteamVR_TrackedObject>().index);
		Setup();
	}

	protected virtual void Setup()
	{
		
	}

	void Update()
	{
		if (ctrl.GetHairTriggerDown() && triggerDown != null)
		{
			triggerDown.Invoke();
		}
		if (ctrl.GetHairTriggerUp() && triggerUp != null)
		{
			triggerUp.Invoke();
		}
	}

	public void TriggerHaptic()
	{
		ctrl.TriggerHapticPulse();
	}
}

