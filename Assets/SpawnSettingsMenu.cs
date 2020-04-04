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
    [SerializeField]
    public TextMeshProUGUI MainText = default;

    private bool currentAdjustButtonsStatus = false;
    private bool currentMainButtonsStatus = false;

    /// <summary>
    /// Call by Button in scene
    /// </summary>
    public void OnClickSettingsButton()
    {
        MainText.text = currentAdjustButtonsStatus.ToString();
        if (!currentMainButtonsStatus)
        {
            GameManagerScript.SetLaunchGameButtonStatus(false);
            ManageButtons(true);
            ManageAdjustHoopButtons(false);
        }
        else
        {
            GameManagerScript.SetLaunchGameButtonStatus(true);
            ManageButtons(false);
            ManageAdjustHoopButtons(true);
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
            ManageButtons(false);
        }
        else
        {
            ManageAdjustHoopButtons(false);
            ManageButtons(true);
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
}
