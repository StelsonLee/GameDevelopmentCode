using UnityEngine;
using System.Collections;

public class InGameOption : MonoBehaviour {

    [SerializeField]
    private GameObject activated;
    [SerializeField]
    private GameObject shootDisable;

    private PlayerShooter playershooter;
    public bool _On = false;

	// Use this for initialization
	void Start () {
        playershooter = shootDisable.GetComponent<PlayerShooter>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_On)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playershooter.optionOn = true;
                activated.SetActive(true);
                Time.timeScale = 0;
                _On = true;
            }
            else if (_On)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playershooter.optionOn = false;
                activated.SetActive(false);
                Time.timeScale = 1;
                _On = false;
            }
        }
	}

    public void BackToGame()
    {
        Debug.Log("backtogame");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playershooter.optionOn = false;
        activated.SetActive(false);
        Time.timeScale = 1;
        _On = false;
    }
}
