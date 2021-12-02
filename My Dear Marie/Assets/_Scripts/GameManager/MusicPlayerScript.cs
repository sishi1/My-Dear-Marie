using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour, ILightDarkModeSwitch
{
    [SerializeField]
    private AudioSource musicPlayer;
    private float pitchQuantity;

    [SerializeField]
    private bool darkMusic, lightMusic;

    private void Start()
    {
        musicPlayer.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (darkMusic) pitchQuantity = 0.8f;
        else if (lightMusic) pitchQuantity = 1f;

        musicPlayer.pitch = pitchQuantity;
    }

    public void OnDarkModeEnter()
    {
        darkMusic = true;
        lightMusic = false;
    }

    public void OnLightModeEnter()
    {
        lightMusic = true;
        darkMusic = false;
    }
}