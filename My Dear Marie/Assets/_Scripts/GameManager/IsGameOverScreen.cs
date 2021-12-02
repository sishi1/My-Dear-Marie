using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsGameOverScreen : MonoBehaviour
{
    private AudioSource[] allAudio;

    // Start is called before the first frame update
    void Start()
    {
        allAudio = FindObjectsOfType<AudioSource>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Pause the game
        foreach (AudioSource audio in allAudio)
            if (audio != GetComponent<AudioSource>())
                audio.Stop();
        if (gameObject.activeSelf) Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
