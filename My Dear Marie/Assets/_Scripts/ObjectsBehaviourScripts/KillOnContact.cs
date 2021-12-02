using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnContact : AbstractObject
{
    // Booleans to check if if the correct state to kill is active.
    private bool canKill;
    public Behaviour killingGlow;

    [SerializeField]
    bool killInLightMode, killInDarkMode;

    MarieController player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<MarieController>();

        if (player && canKill)
        {
            player.GameOverScreen.SetActive(true);
        }
    }

    public override void OnDarkModeEnter()
    {
        if (killInDarkMode) canKill = true;
        else canKill = false;
        killingGlow.enabled = true;
    }

    public override void OnLightModeEnter()
    {
        if (killInLightMode) canKill = true;
        else canKill = false;
        killingGlow.enabled = false;
    }
}
