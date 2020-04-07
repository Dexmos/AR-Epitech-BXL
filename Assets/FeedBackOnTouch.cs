using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedBackOnTouch : MonoBehaviour
{
    [Header("FeedBack Visuel")]
    [SerializeField]
    public Canvas MainCanvas = default;
    [SerializeField]
    public GameObject ImageToInstanciate = default;

    private GameObject touchZeroInstance = default;
    private GameObject touchOneInstance = default;

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            SetActiveAllTouchImage(false);
        }
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);

            if (touchZeroInstance == null)
            {
                touchZeroInstance = Instantiate(ImageToInstanciate, MainCanvas.transform);

                touchZeroInstance.SetActive(true);
                touchOneInstance.SetActive(false);
            }
            SetActiveTouchImage(touchZeroInstance, true);
            SetActiveTouchImage(touchOneInstance, false);

            touchZeroInstance.transform.position = new Vector3(touchZero.position.x, touchZero.position.y, 0.0f);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchOneInstance == null)
            {
                touchOneInstance = Instantiate(ImageToInstanciate, MainCanvas.transform);
            }
            SetActiveAllTouchImage(true);
            touchZeroInstance.transform.position = new Vector3(touchZero.position.x, touchZero.position.y, 0.0f);
            touchOneInstance.transform.position = new Vector3(touchOne.position.x, touchOne.position.y, 0.0f);
        }
    }

    private void SetActiveAllTouchImage(bool status)
    {
        SetActiveTouchImage(touchZeroInstance, status);
        SetActiveTouchImage(touchOneInstance, status);
    }

    private void SetActiveTouchImage(GameObject TouchImage, bool status)
    {
        if (TouchImage != null)
        {
            TouchImage.SetActive(status);
        }
    }
}
