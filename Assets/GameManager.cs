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
    public TextMeshProUGUI MainText = default;
    [SerializeField]
    public Button LaunchGameButton = default;
    [SerializeField]
    public MyButtonScript LaunchGameMyButtonScript = default;
    [SerializeField]
    public Canvas MainCanvas = default;
    
    [SerializeField]
    public GameObject BallPrefab = default;
    [SerializeField]
    public Camera MainCamera = default;
    public Vector3 PositionToInstantiateBalls = Vector3.zero;

    private GameObject currentBall = default;

    private void Start()
    {
        LaunchGameMyButtonScript.SetGameManager(this);
    }

    public void StartGame()
    {
        SetLaunchGameButtonStatus(false);
        if (currentBall != null)
        {
            Invoke("CallThrowBall", 2.0f);
        }
    }

    private void CallThrowBall()
    {
        currentBall.GetComponent<SwipeScript>().ThrowBall(true);
    }

    public void SetLaunchGameButtonStatus(bool status)
    {
        LaunchGameButton.gameObject.SetActive(status);

        // Make UI bug
        /*if (status)
        {
            MainText.text = "Place Ball";
            if (currentBall == null)
            {
                //currentBall = Instantiate(BallPrefab);
                //PlaceBallOnPlaneScript.SetPlaceBall(true);
                //currentBall = PlaceBallOnPlaneScript.PlaceBall();
                //PlaceBallOnPlaneScript.PlaceBall();
                //LaunchGameButton.gameObject.SetActive(false);
            }
            //if (currentBall != null)
            //{
              //  currentBall.GetComponent<SwipeScript>().SetCamera(MainCamera);
               // currentBall.transform.position = MainCamera.transform.position + MainCamera.transform.forward * 2.5f + MainCamera.transform.up * -0.5f;
            //}
        }*/
    }

    public void SetPlaceBall()
    {
        MainText.text = "Place Ball";
        PlaceBallOnPlaneScript.SetPlaceBall(true);
    }

    public void SetBallPlacedOnPlan(GameObject ball)
    {
        MainText.text = "Set currentBall";
        currentBall = ball;
        currentBall.GetComponent<SwipeScript>().SetCamera(MainCamera);
        currentBall.GetComponent<SwipeScript>().SetText(MainText);
        currentBall.GetComponent<SwipeScript>().SetARRaycastManager(ARRaycastManagerScript);

        SetLaunchGameButtonStatus(true);
        LaunchGameMyButtonScript.SetTMProtext(MainText);
    }
}
