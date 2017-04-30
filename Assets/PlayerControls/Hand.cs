using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public Action triggerDown;
	public Action triggerUp;

	protected SteamVR_TrackedController ctrl;

	void Start()
	{
		ctrl = GetComponent<SteamVR_TrackedController>();
		ctrl.TriggerClicked += (sender, args) => triggerDown.Invoke();
		ctrl.TriggerUnclicked += (sender, args) => triggerUp.Invoke();
		Setup();
	}

	protected virtual void Setup(){}

	public void TriggerHaptic()
	{
		SteamVR_Controller.Input((int)ctrl.controllerIndex).TriggerHapticPulse(1000);
	}
}

