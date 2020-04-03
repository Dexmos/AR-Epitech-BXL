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
    public GameObject[] ARPlanes = default;

    private void Start()
    {
        LaunchGameMyButtonScript.SetGameManager(this);
    }

    private void Update()
    {
        if (currentBall)
        {
            MainText.text = currentBall.transform.position.ToString();
        }
    }

    public void SetLaunchGameButtonStatus(bool status)
    {
        LaunchGameButton.gameObject.SetActive(status);
        if (status)
        {
            MainText.text = "GameReady";
            if (currentBall == null)
            {
                currentBall = Instantiate(BallPrefab);
                currentBall.GetComponent<SwipeScript>().SetCamera(MainCamera);
                //ARSessionOriginScript.MakeContentAppearAt(currentBall.transform, PositionToInstantiateBalls);
                //DisablePlaneManagerScript();
            }
            currentBall.transform.position = MainCamera.transform.position + MainCamera.transform.forward * 2.5f + MainCamera.transform.up * -0.5f;
        }
        else
        {
            MainText.text = "GameNotReady";
            if (currentBall != null)
            {
                Destroy(currentBall);
            }
        }
    }

    /*private void DisablePlaneManagerScript()
    {
        ARPlaneManagerScript.enabled = false;
        ARPlanes = GameObject.FindGameObjectsWithTag("ARPlane");

        for(int i = 0; ARPlanes[i] != null; i++)
        {
            ARPlanes[i].SetActive(false);
        }
    }

    public GameObject[] GetAllPlanes()
    {
        return (ARPlanes);
    }*/
}
