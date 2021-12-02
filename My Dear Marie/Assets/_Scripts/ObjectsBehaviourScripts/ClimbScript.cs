using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbScript : MonoBehaviour
{
    private Rigidbody marieRigidbody;

    private MarieController marie;

    private bool collisionIsPossible = false;

    [SerializeField]
    private KeyCode keyCode;

    [SerializeField]
    //With this boolean, you can switch the debugs in this script on or off through the inspector
    private bool debugTrigger = false;

    private void Awake()
    {
        //Find the script of Marie
        marie = FindObjectOfType<MarieController>();
    }

    //Checks if Marie is touching the climbable object
    private void OnCollisionEnter(Collision collision)
    {        
        if (marie)
        {
            if (debugTrigger) Debug.Log("Touched the bars!");
            marie.currentMovement = MarieController.Movement.climbing;
        }
    }

    //Checks if Marie isn't touching the climbable object anymore
    private void OnCollisionExit(Collision collision)
    {        
        if (marie)
        {
            if (debugTrigger) Debug.Log("Let go of the bars!");
            marie.currentMovement = MarieController.Movement.walking;
        }
    }
}
