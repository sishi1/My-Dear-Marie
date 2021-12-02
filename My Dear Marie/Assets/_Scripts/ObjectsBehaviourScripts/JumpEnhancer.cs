using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can only be used if an object can be charged
[RequireComponent(typeof(ChargeableObject))]

public class JumpEnhancer : MonoBehaviour
{
    ChargeableObject theObject;
    JumpScript playerJump;
    public float enhancePower;

    private void Awake()
    {
        theObject = GetComponent<ChargeableObject>();
        playerJump = FindObjectOfType<JumpScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enahnces jump power of the player when the object is charged
        if (theObject.IsFullyCharged)
        {
            playerJump.JumpPower += enhancePower;

            // Makes the player jump instantly on collision (aka a bounce effect)
            playerJump.Jump();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerJump.JumpPower = playerJump.baseJumpPower;
        if (theObject.IsFullyCharged)
            theObject.IsUsed = true;
    }

    // Makes it so that the player can't jump on an object that lost his charge 
    // while player was still stading on it
    private void OnTriggerStay(Collider other)
    {
        if (!theObject.IsFullyCharged)
        {
            playerJump.JumpPower = playerJump.baseJumpPower;
        }
    }
}
