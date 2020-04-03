using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButtonScript : Button
{
    private GameManager GameManagerScript = default;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Down");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Debug.Log("Up");
        GameManagerScript.SetLaunchGameButtonStatus(true);
    }

    public void SetGameManager(GameManager newGameManager)
    {
        GameManagerScript = newGameManager;
    }
}
