using UnityEngine;
using System.Collections;

public class ButtonDescription : MonoBehaviour {

    [SerializeField]
    private GameObject continueDescription, newGameDescription, loadGameDescription, optionDescription, quitDescription;

    public Texture2D mouseOver;
    public Texture2D mouseNormal;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void ShowContinue()
    {
        continueDescription.SetActive(true);
    }

    public void HideContinue()
    {
        continueDescription.SetActive(false);
    }

    public void ShowNewGame()
    {
        newGameDescription.SetActive(true);
    }

    public void HideNewGame()
    {
        newGameDescription.SetActive(false);
    }

    public void ShowLoadGame()
    {
        loadGameDescription.SetActive(true);
    }

    public void HideLoadGame()
    {
        loadGameDescription.SetActive(false);
    }

    public void ShowOption()
    {
        optionDescription.SetActive(true);
    }

    public void HideOption()
    {
        optionDescription.SetActive(false);
    }

    public void ShowQuit()
    {
        quitDescription.SetActive(true);
    }

    public void HideQuit()
    {
        quitDescription.SetActive(false);
    }

    public void MouseOver()
    {
        Cursor.SetCursor(mouseOver, hotSpot, cursorMode);
    }

    public void MouseAway()
    {
        Cursor.SetCursor(mouseNormal, hotSpot, cursorMode);
    }
}
