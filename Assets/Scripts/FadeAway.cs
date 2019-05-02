using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CountDownTimer))]
public class FadeAway : MonoBehaviour
{
    private CountDownTimer countdowntimer;
    private Text textUI;

    private int startSeconds = 6;
    private bool fading = false;


    // Use this for initialization
    void Start()
    {
        textUI = GetComponent<Text>();
        countdowntimer = GetComponent<CountDownTimer>();

        StartFading(startSeconds);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            float alphaRemaining = countdowntimer.GetProportionTimeRemaining();
            print(alphaRemaining);
            Color c = textUI.material.color;

            c.a = alphaRemaining;
            textUI.material.color = c;

            if (alphaRemaining < 0.01)
            {
                fading = false;
            }
        }
    }

    private void StartFading(int timerTotal)
    {
        countdowntimer.ResetTimer(timerTotal);

        fading = true;

        StartCoroutine("Fade");

    }
}
