using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControllerTest : MonoBehaviour {

	private SteamVR_TrackedObject trackedOj;

	private GameObject collidingObject;

	private GameObject objectInHand;

	public GameObject uvLight;

	public GameObject Lights;

	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedOj.index); }
	}

	void Awake() {
		trackedOj = GetComponent<SteamVR_TrackedObject> ();
	}

	
	// Update is called once per frame
	void Update () {
		if (Controller.GetHairTriggerDown())
		{
			uvLight.SetActive (!uvLight.activeInHierarchy);
		}

		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Lights.SetActive (!Lights.activeInHierarchy);
		}
	}
}
