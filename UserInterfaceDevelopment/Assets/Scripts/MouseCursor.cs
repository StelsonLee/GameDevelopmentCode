using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {

    [SerializeField]
    private GameObject shootDisable;
    [SerializeField]
    private GameObject inGameOption;

    public Texture2D cursorTexture;
    public Texture2D cursorPressed;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorPressed, hotSpot, cursorMode);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    public void BackGame()
    {
        Debug.Log("backgtogamepressed");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        //inGameOption.transform.GetComponent<InGameOption>()._On = false;
        //shootDisable.transform.GetComponent<PlayerShooter>().optionOn = false;
    }
}
