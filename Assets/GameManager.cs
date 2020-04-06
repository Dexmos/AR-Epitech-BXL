using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public ARSessionOrigin ARSessionOriginScript = default;
    [SerializeField]
    public ARPlaneManager ARPlaneManagerScript = default;
    [SerializeField]
    public ARRaycastManager ARRaycastManagerScript = default;
    [SerializeField]
    public PlaceBallOnPlane PlaceBallOnPlaneScript = default;
    [SerializeField]
    public PlaceOnPlane PlaceOnPlaneScript = default;

    [SerializeField]
    public TextMeshProUGUI MainText = default;
    [SerializeField]
    public Button LaunchGameButton = default;
    [SerializeField]
    public MyButtonScript LaunchGameMyButtonScript = default;
    [SerializeField]
    public Canvas MainCanvas = default;
    [SerializeField]
    public Button FIREButton = default;
    [SerializeField]
    public Slider RotateBallSliderZ = default;
    
    [SerializeField]
    public GameObject BallPrefab = default;
    [SerializeField]
    public Camera MainCamera = default;
    public Vector3 PositionToInstantiateBalls = Vector3.zero;

    private GameObject currentBall = default;
    private GameObject target = default;

    private void Start()
    {
        LaunchGameMyButtonScript.SetGameManager(this);
    }

    public void StartGame()
    {
        SetLaunchGameButtonStatus(false);
        FIREButton.gameObject.SetActive(true);
        RotateBallSliderZ.gameObject.SetActive(true);
    }

    public void SetLaunchGameButtonStatus(bool status)
    {
        LaunchGameButton.gameObject.SetActive(status);
    }

    public void FireTheball()
    {
        target = PlaceBallOnPlaneScript.GetSpawedObject();
        currentBall.GetComponent<SwipeScript>().Fire(target);
        FIREButton.interactable = false;
        RotateBallSliderZ.gameObject.SetActive(false);
        //Invoke("MakeFireButtonInteractable", 2.0f);
    }

    private void MakeFireButtonInteractable()
    {
        FIREButton.interactable = true;
        RotateBallSliderZ.gameObject.SetActive(true);
        currentBall.GetComponent<SwipeScript>().CanTrowBall(false);
    }

    public void SetPlaceBall()
    {
        PlaceBallOnPlaneScript.SetPlaceBall(true);
    }

    /// <summary>
    /// Called by slider in scene
    /// </summary>
    public void OnBallZSliderValueChanged()
    {
        Vector3 newRotation = Vector3.zero;

        newRotation.x = currentBall.transform.localRotation.x;
        newRotation.y = (currentBall.transform.localRotation.y + RotateBallSliderZ.value);
        newRotation.z = (currentBall.transform.localRotation.z);
        currentBall.transform.localEulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);
        //currentBall.transform.Rotate(new Vector3(newRotation.x, newRotation.y, newRotation.z), Space.Self);

        currentBall.GetComponent<SwipeScript>().SetLineRenderer();
    }

    public void SetBallPlacedOnPlan(GameObject ball)
    {
        currentBall = ball;
        currentBall.GetComponent<SwipeScript>().SetCamera(MainCamera);
        currentBall.GetComponent<SwipeScript>().SetText(MainText);
        currentBall.GetComponent<SwipeScript>().SetARRaycastManager(ARRaycastManagerScript);

        SetLaunchGameButtonStatus(true);
        LaunchGameMyButtonScript.SetTMProtext(MainText);
    }
}
