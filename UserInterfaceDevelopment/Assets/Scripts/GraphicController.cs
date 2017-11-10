using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GraphicController : MonoBehaviour {

    public bool fullScreen = true;
    public Text text;
    public int screenResolution = 0;
    public bool resolution = true;

	// Use this for initialization
	void Start () {

        screenResolution = PlayerPrefs.GetInt("ScreenRexIndex");
        Screen.fullScreen = (PlayerPrefs.GetInt("Fullscreen") == 1) ? true : false;

        if (resolution)
        {
            if (screenResolution == 0)
            {
                text.text = "960x540";
                Screen.SetResolution(960, 540, Screen.fullScreen);
            }
            else if (screenResolution == 1)
            {
                text.text = "1280x720";
                Screen.SetResolution(12800, 720, Screen.fullScreen);
            }
            else if (screenResolution == 2)
            {
                text.text = "1920x1080";
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
            }
        }
        else if(Screen.fullScreen)
        {
            text.text = "YES";
        }
        else if(!Screen.fullScreen)
        {
            text.text = "NO";
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnGraphicChanged()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            text.text = "NO";
        }
        else if(!Screen.fullScreen)
        {
            text.text = "YES";
        }
        PlayerPrefs.SetInt("Fullscreen", ((Screen.fullScreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void OnResolutionRight()
    {
        if (screenResolution <=1)
        {
            screenResolution++;
        }
        else
        {
            screenResolution = 0;
        }
        
        if (screenResolution == 0)
        {
            text.text = "960x540";
            Screen.SetResolution(960, 540, Screen.fullScreen);
        }
        else if (screenResolution == 1)
        {
            text.text = "1280x720";
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        else if (screenResolution == 2)
        {
            text.text = "1920x1080";
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }

        PlayerPrefs.SetInt("ScreenResIndex", screenResolution);
        PlayerPrefs.Save();
    }

    public void OnResolutionLeft()
    {
        if (screenResolution >= 1)
        {
            screenResolution--;
        }
        else
        {
            screenResolution = 2;
        }

        if (screenResolution == 0)
        {
            text.text = "960x540";
            Screen.SetResolution(960, 540, Screen.fullScreen);
        }
        else if (screenResolution == 1)
        {
            text.text = "1280x720";
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        else if (screenResolution == 2)
        {
            text.text = "1920x1080";
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }

        PlayerPrefs.SetInt("ScreenResIndex", screenResolution);
        PlayerPrefs.Save();
    }
}
