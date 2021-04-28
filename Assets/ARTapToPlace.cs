using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using UnityEngine.UI;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject capsule;
    private float Normaltime = 0;
    private float duzelatime = 0;
    public Text NormaltimeText;
    public Text DuzelatimeText;
    public GameObject placementIndicator;
    public GameObject replay;
    public Animator anim;
    public bool timerIsRunning = false;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arMangaer;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool objectPlaced = false;

    public GameObject spawnedObject { get; private set; }
    public GameObject objectToPlace;



    private void Awake()
    {
        arMangaer = FindObjectOfType<ARRaycastManager>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacemntIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if (!objectPlaced)
            {
                PlaceObject();
                objectPlaced = true;
            }
        }



        if (objectPlaced && anim)
        {
            if (spawnedObject.GetComponent<playVideo>().TimerIsRunnig == true)
            {
                Normaltime += Time.deltaTime * 900;
                duzelatime += Time.deltaTime * 257;
                DisplayNormalTime(Normaltime);
                DisplayduzelaTime(duzelatime);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                spawnedObject.GetComponent<playVideo>().TimerIsRunnig = false;
                replay.SetActive(true);
            }
            else
            {
                replay.SetActive(false);
            }
        }

    }

    private void PlaceObject()
    {
        spawnedObject = Instantiate(capsule, objectToPlace.transform.position, capsule.transform.rotation);
        spawnedObject.transform.parent = objectToPlace.transform;
        if (!anim) anim = spawnedObject.GetComponent<Animator>();
        if (anim)
        {
            anim.SetBool("capsuleanim", true);
        }
    }
    
    public void animReplay()
    {
        anim.Play("Base Layer.CINEMA_4D_Main", 0, 0f);
        Normaltime = 0;
        duzelatime = 0;
        NormaltimeText.text = string.Format("{0:00}:{1:00}:{2:00}", 00, 00, 00);
        DuzelatimeText.text = string.Format("{0:00}:{1:00}:{2:00}", 00, 00, 00);
        spawnedObject.GetComponent<playVideo>().firstVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().seconfVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().thirdVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().fourthVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().pillFirstVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().pillSecondVideo.SetActive(false);
        spawnedObject.GetComponent<playVideo>().pillThirdVideo.SetActive(false);
    }

    void DisplayNormalTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float hours = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60) % 60;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        NormaltimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        Debug.Log(hours);
    }

    void DisplayduzelaTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float hours = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60) % 60;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        DuzelatimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        Debug.Log(hours);
    }

    private void UpdatePlacemntIndicator()
    {
        if (placementPoseIsValid)
        {
            if (!objectPlaced)
            {
                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
                objectToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else
            {
                placementIndicator.SetActive(false);
            }
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCentre = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        arMangaer.Raycast(screenCentre, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
