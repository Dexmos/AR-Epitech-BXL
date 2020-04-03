using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private bool currentAdjustButtonsStatus = false;

    /// <summary>
    /// Call by Button in scene
    /// </summary>
    public void OnClickSettingsButton()
    {
        if (!currentMainButtonsStatus)
        {
            ManageButtons(true);
        }
        else
        {
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
}
