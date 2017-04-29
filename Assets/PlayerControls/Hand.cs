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

	void OnEnable()
	{
		//ctrl = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);

		ctrl = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
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
}

