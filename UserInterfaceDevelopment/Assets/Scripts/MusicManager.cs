using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public AudioClip mainTheme;
    public AudioClip menuTheme;

    void Start()
    {
        AudioController.instance2.PlayMusic(menuTheme, 2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioController.instance2.PlayMusic(mainTheme, 3);
        }

    }

}