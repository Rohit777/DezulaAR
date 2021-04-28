using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVideo : MonoBehaviour
{
    public bool TimerIsRunnig = false;
    public GameObject firstVideo;
    public GameObject seconfVideo;
    public GameObject thirdVideo;
    public GameObject fourthVideo;

    public GameObject pillFirstVideo;
    public GameObject pillSecondVideo;
    public GameObject pillThirdVideo;

    public void PlayTimer()
    {
        TimerIsRunnig = true;
    }

    public void PlayFirstVideo()
    {
        firstVideo.SetActive(true);
    }

    public void PlaySecondVideo()
    {
        seconfVideo.SetActive(true);
    }

    public void PlayThirdVideo()
    {
        thirdVideo.SetActive(true);
    }

    public void PlayFourthVideo()
    {
        fourthVideo.SetActive(true);
        pillThirdVideo.SetActive(true);
    }

    public void PlayPillFirstVideo()
    {
        pillFirstVideo.SetActive(true);
    }

    public void PlayPillSecondVideo()
    {
        pillSecondVideo.SetActive(true);
    }
}
