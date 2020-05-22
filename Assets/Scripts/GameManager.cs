using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public PlaceBallOnPlane PlaceBallOnPlaneScript = default;

    [SerializeField]
    public TextMeshProUGUI MainText = default;
    [SerializeField]
    public Button LaunchGameButton = default;
    [SerializeField]
    public MyButtonScript LaunchGameMyButtonScript = default;
    [SerializeField]
    public Button FIREButton = default;
    [SerializeField]
    public Slider RotateBallSliderZ = default;

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
        currentBall.GetComponent<Bullet>().Fire(target);
        FIREButton.interactable = false;
        RotateBallSliderZ.gameObject.SetActive(false);
    }

    private void MakeFireButtonInteractable()
    {
        FIREButton.interactable = true;
        RotateBallSliderZ.gameObject.SetActive(true);
        currentBall.GetComponent<Bullet>().CanTrowBall(false);
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

        currentBall.GetComponent<Bullet>().SetLineRenderer();
    }

    public void SetBallPlacedOnPlan(GameObject ball)
    {
        currentBall = ball;
        currentBall.GetComponent<Bullet>().SetText(MainText);

        SetLaunchGameButtonStatus(true);
        LaunchGameMyButtonScript.SetTMProtext(MainText);
    }
}
