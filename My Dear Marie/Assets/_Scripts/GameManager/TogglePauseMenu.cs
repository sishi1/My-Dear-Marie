using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePauseMenu : MonoBehaviour
{
    public KeyCode pauseKey;
    GameObject menuScreen;
    private AudioSource[] allAudio;

    private void Awake()
    {
        menuScreen = FindObjectOfType<isPauseMenu>().gameObject;
        allAudio = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            menuScreen.SetActive(true);
            foreach (AudioSource audio in allAudio)
                audio.Pause();
        }

        //Pause the game
        if (menuScreen.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        menuScreen.SetActive(false);
        foreach (AudioSource audio in allAudio)
            audio.UnPause();
    }
}
