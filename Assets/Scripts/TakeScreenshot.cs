/*
Script made by: Mathijs van Sambeek
For more tutorials: https://www.youtube.com/user/0Imagineer0?sub_confirmation=1

This script can be used for free if you give credits to the maker of the script.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TakeScreenshot : MonoBehaviour
{
    public bool controlable = false;
    public float moveSpeed = 1;
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    //public string customPath = "C:/Users/default/Desktop/UnityScreenshots/"; // leave blank for project file location
    public string customPath = "C:/Users/civs_user/Desktop/UnityScreenshots/"; // leave blank for project file location

    public bool resetIndex = false;
    private int index = 0;
    public GameObject flash;

    public GameObject watermark;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0);
        if (customPath != "")
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void Start()
    {
        flash = GameObject.FindGameObjectWithTag("flash");
        watermark = GameObject.FindGameObjectWithTag("watermark");
        watermark.SetActive(false);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.2f);
        flash.GetComponent<Light>().enabled = false;
        watermark.SetActive(false);
    }


    void Update()
    {

    }

    void LateUpdate()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {

            watermark.SetActive(true);

            Debug.Log(Application.dataPath);
            //Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
            UnityEngine.ScreenCapture.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
            index++;
            Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);

            flash.GetComponent<Light>().enabled = true;
            StartCoroutine("Fade");
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index));
    }
}