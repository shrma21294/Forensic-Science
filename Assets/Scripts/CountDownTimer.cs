using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    private float countdownTimerStartTime;
    private int coutndownTimerDuration;


    public int GetTotalSeconds()
    {
        return coutndownTimerDuration;
    }

    public void ResetTimer(int seconds)
    {
        countdownTimerStartTime = Time.time;
        coutndownTimerDuration = seconds;
    }

    public int GetSecondsRemaining()
    {
        int elapsedSeconds = (int)(Time.time - countdownTimerStartTime);
        int secondsLeft = (coutndownTimerDuration - elapsedSeconds);
        return secondsLeft;
    }

    public float GetFractionSecondsRemaining()
    {
        float elapsedSeconds = (int)(Time.time - countdownTimerStartTime);
        float secondsLeft = (coutndownTimerDuration - elapsedSeconds);
        return secondsLeft;
    }

    public float GetProportionTimeRemaining()
    {
        float proportionLeft = (float)GetFractionSecondsRemaining() / (float)GetTotalSeconds();
        return proportionLeft;
    }
	
}
