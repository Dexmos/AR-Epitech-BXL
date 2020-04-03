using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class SpawnSettingsMenu : MonoBehaviour
{
    [Header("Main Buttons")]
    [SerializeField]
    public GameObject AdjustHoopButton = default;
    [SerializeField]
    public GameObject ChangeHoopButton = default;
    [SerializeField]
    public GameObject ChangeBallButton = default;

    private bool currentMainButtonsStatus = false;

    [Header("Adjust Hoop Buttons")]
    [SerializeField]
    public Button ChangeAHoopPositionButton = default;
    [SerializeField]
    public Button ChangeAHoopRotationButton = default;
    [SerializeField]
    public Button ChangeAHoopScaleButton = default;

    [Space(5f)]
    [SerializeField]
    public GameManager GameManagerScript = default;
    [SerializeField]
    public ARPlaneManager ARPlaneManagerScript = default;

    private bool currentAdjustButtonsStatus = false;
    private GameObject[] findedARPlanes = default;

    /// <summary>
    /// Call by Button in scene
    /// </summary>
    public void OnClickSettingsButton()
    {
        if (!currentMainButtonsStatus)
        {
            GameManagerScript.SetLaunchGameButtonStatus(false);
            ManageButtons(true);
        }
        else
        {
            GameManagerScript.SetLaunchGameButtonStatus(true);
            ManageButtons(false);
        }
    }

    /// <summary>
    /// Call by Button in scene
    /// </summary>
    public void OnClickAdjustHoopSettingsButton()
    {
        if (!currentAdjustButtonsStatus)
        {
            ManageAdjustHoopButtons(true);
        }
        else
        {
            ManageAdjustHoopButtons(false);
        }
    }

    public void ManageButtons(bool status)
    {
        AdjustHoopButton.SetActive(status);
        ChangeHoopButton.SetActive(status);
        ChangeBallButton.SetActive(status);
        currentMainButtonsStatus = status;
    }

    public void ManageAdjustHoopButtons(bool status)
    {
        ChangeAHoopPositionButton.gameObject.SetActive(status);
        ChangeAHoopRotationButton.gameObject.SetActive(status);
        ChangeAHoopScaleButton.gameObject.SetActive(status);
        currentAdjustButtonsStatus = status;
    }

    /*public void EnablePlaneManagerScript()
    {
        ARPlaneManagerScript.enabled = true;
        findedARPlanes = GameManagerScript.GetAllPlanes();

        for (int i = 0; findedARPlanes[i] != null; i++)
        {
            findedARPlanes[i].SetActive(false);
        }
    }*/
}
