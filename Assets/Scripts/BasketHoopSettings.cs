using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasketHoopSettings : MonoBehaviour
{
    [SerializeField]
    public PlaceOnPlane PlaceOnPlaneScript = default;
    [SerializeField]
    public Slider Slider = default;



    private GameObject gameObjectToChange = default;
    private bool canChangeRotation = false;


    /// <summary>
    /// Called by button in scene
    /// </summary>
    /// <param name="status"></param>
    public void SetChangeRotation(bool status)
    {
        canChangeRotation = status;
    }

    public void OnSliderValueChanged()
    {
        Vector3 newRotation = Vector3.zero;

        if (canChangeRotation)
        {
            gameObjectToChange = PlaceOnPlaneScript.GetSpawedObject();
            newRotation.x = gameObjectToChange.transform.localRotation.x;
            newRotation.y = gameObjectToChange.transform.localRotation.y;
            newRotation.z = (gameObjectToChange.transform.localRotation.z + Slider.value) * -1.0f;
            gameObjectToChange.transform.localEulerAngles = new Vector3(-90.0f, newRotation.y, newRotation.z);
        }
    }
}
