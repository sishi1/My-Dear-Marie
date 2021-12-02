using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCommands : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ObjectModelTestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
