using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollideLugia : MonoBehaviour
{
    private TextMeshProUGUI MainText = default;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            MainText.fontSize = 100;
            MainText.text = "CHEH !";
        }
    }

    public void SetMainText(TextMeshProUGUI text)
    {
        MainText = text;
    }
}
