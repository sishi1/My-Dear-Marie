using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    [SerializeField]
    private string level;
    private MarieController marie;

    public void OnTriggerEnter(Collider other)
    {
        marie = other.gameObject.GetComponent<MarieController>();

        if (marie) SceneManager.LoadScene(level);
    }
}
