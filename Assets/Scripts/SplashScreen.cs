using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private int index = 0;
    public bool controlable = false;
    public float moveSpeed = 1;
    public bool resetIndex = false;
    Canvas splash;
    GameObject tempObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LateUpdate()
    {

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) || Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || Controller.GetHairTriggerDown())
        {
            SceneManager.LoadScene(2);
        }


    }
}

