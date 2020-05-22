using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PinchManager : MonoBehaviour
{
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    [SerializeField]
    public PlaceOnPlane PlaceOnPlaneScript = default;
    
    private bool canChangeScale = false;
    private GameObject objectToChangeScale = default;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.02f);
        }
    }

    private void zoom(float increment)
    {
        float newValue;

        if (canChangeScale)
        {
            objectToChangeScale = PlaceOnPlaneScript.GetSpawedObject();
            newValue = objectToChangeScale.transform.localScale.x + increment;
            objectToChangeScale.transform.localScale = new Vector3(newValue, newValue, newValue);
        }
    }

    /// <summary>
    /// Called by button in scene
    /// </summary>
    /// <param name="status"></param>
    public void CanChangeScale(bool status)
    {
        canChangeScale = status;
    }
}

