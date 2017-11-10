using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIActivate : MonoBehaviour {

    [SerializeField]
    private Text volumeText;
    [SerializeField]
    private GameObject AudioManager;
    [SerializeField]
    private Slider slider;

    public GameObject[] selection;
    public GameObject[] deactivate;
    public Text[] selectionText;
    public Text[] deselectedText;
    public AudioClip buttonPressed;
    //public Slider[] volumeSlider;
    //public Text[] volumeChangedText;
    public int type = 0;
    public AudioClip mainTheme;

    private AudioController AC;

    void Start()
    {
        if (type == 1)
        {
            slider.onValueChanged.AddListener(delegate { OnMasterChanged(); });
            slider.value = AudioController.instance2.masterVolumePercent * 100;
        }
        else if (type == 2)
        {
            slider.onValueChanged.AddListener(delegate { OnMusicChanged(); });
            slider.value = AudioController.instance2.musicVolumePercent * 100;
        }
        else if (type == 3)
        {
            slider.onValueChanged.AddListener(delegate { OnSfxChanged(); });
            slider.value = AudioController.instance2.sfxVolumePercent * 100;
        }
        
        AC = AudioManager.GetComponent<AudioController>();

    }
    
    public void OnMenuPressed()
    {
        Debug.Log("Menupressed");
        for (int i = 0; i < selection.Length; i++)
        {
            selection[i].SetActive(true);
        }
        for (int i = 0; i < deactivate.Length; i++)
        {
            deactivate[i].SetActive(false);
        }
        for (int i = 0; i < selectionText.Length; i++)
        {
            selectionText[i].color = new Color(255, 255, 0);
        }
        for (int i = 0; i < deselectedText.Length; i++)
        {
            deselectedText[i].color = new Color(0, 181, 255);
        }
        //Time.timeScale = 1;
        AudioController.instance2.PlaySound(buttonPressed, transform.position);
    } 

    public void OnMasterChanged()
    {
        Debug.Log(slider.value);
        AudioController.instance2.SetVolume(slider.value / 100, AudioController.AudioChannel.Master);
        //AC.masterVolumePercent = slider.value / 100;
        volumeText.text = (slider.value).ToString();
        Debug.Log(AC.masterVolumePercent);
        //AudioController.instance2.PlayMusic(mainTheme, 3);
    }
    public void OnSfxChanged()
    {
        Debug.Log(slider.value);
        AudioController.instance2.SetVolume(slider.value / 100, AudioController.AudioChannel.Sfx);
        //AC.sfxVolumePercent = slider.value / 100;
        volumeText.text = (slider.value).ToString();
        Debug.Log(AC.sfxVolumePercent);
        //AudioController.instance2.PlayMusic(mainTheme, 3);
    }
    public void OnMusicChanged()
    {
        Debug.Log(slider.value);
        AudioController.instance2.SetVolume(slider.value / 100, AudioController.AudioChannel.Music);
        //AC.musicVolumePercent = slider.value / 100;
        volumeText.text = (slider.value).ToString();
        Debug.Log(AC.musicVolumePercent);
        //AudioController.instance2.PlayMusic(mainTheme, 3);
    }

    public void OnQuitPress()
    {
        Debug.Log("Quit Pressed");
        Application.Quit();
        AudioController.instance2.PlaySound(buttonPressed, transform.position);
    }

    public void OnNewGamePressed()
    {
        Application.LoadLevel("Level_2");
        AudioController.instance2.PlaySound(buttonPressed, transform.position);
    }

    public void OnMainMenuPressed()
    {
        Application.LoadLevel("MainMenu");
        AudioController.instance2.PlaySound(buttonPressed, transform.position);
    }

}
