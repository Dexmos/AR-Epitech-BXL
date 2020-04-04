using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MyButtonScript : Button
{
    private GameManager GameManagerScript = default;
    private TextMeshProUGUI MainText = default;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Down");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Debug.Log("Up");
        MainText.text = "Set LaunchGameButton to true";
        //GameManagerScript.SetLaunchGameButtonStatus(true);
        GameManagerScript.StartGame();
    }

    public void SetGameManager(GameManager newGameManager)
    {
        GameManagerScript = newGameManager;
    }

    public void SetTMProtext(TextMeshProUGUI text)
    {
        MainText = text;
    }
}
