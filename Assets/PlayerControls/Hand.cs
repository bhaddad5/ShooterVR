using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public Action triggerDown;
	public Action triggerUp;
	public Action gripDown;
	public Action gripUp;

	protected SteamVR_TrackedController ctrl;

	void Start()
	{
		ctrl = GetComponent<SteamVR_TrackedController>();
		ctrl.TriggerClicked += (sender, args) => triggerDown.Invoke();
		ctrl.TriggerUnclicked += (sender, args) => triggerUp.Invoke();
		ctrl.Gripped += (sender, args) => gripDown.Invoke();
		ctrl.Ungripped += (sender, args) => gripUp.Invoke();
		Setup();
	}
	protected virtual void Setup(){}

	void Update()
	{
		OnUpdate();
	}
	protected virtual void OnUpdate(){}

	public void TriggerHaptic()
	{
		SteamVR_Controller.Input((int)ctrl.controllerIndex).TriggerHapticPulse(2000);
	}
}

