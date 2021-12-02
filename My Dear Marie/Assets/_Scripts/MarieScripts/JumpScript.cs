using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    private float raycastLength;
    public float baseJumpPower;
    public LayerMask mask;
    private Vector3 jumpVector;
    private Rigidbody rigidbody;
    public AudioSource jumpSound;

    GameObject menuScreen;

    [SerializeField]
    //With this boolean, you can switch the debugs in this script on or off through the inspector
    private bool debugTrigger = false;

    // When changed by jumpable objects the jumpVector changes as well
    public float JumpPower
    {
        get { return baseJumpPower; }
        set { jumpVector = new Vector3(0, value, 0); }
    }

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        JumpPower = baseJumpPower;

        menuScreen = FindObjectOfType<isPauseMenu>().gameObject;
    }

    void Update()
    {
        //If you press Space
        if (Input.GetKeyDown(KeyCode.Space) &! DialogueTrigger.activated &! menuScreen.activeSelf)
        {
            //Execute the Jump method
            Jump();

            if(debugTrigger) Debug.Log("Space pressed");
        }
    }

    public void Jump()
    {
        //If the CanJump method returned true
        if (CanJump())
        {
            // Jump off the object where you are standing on
            rigidbody.AddForce(jumpVector, ForceMode.Impulse);
            jumpSound.Play();

            if (debugTrigger) Debug.Log("LOOK MUMMY I JUMPED!");

        }
    }

    bool CanJump()
    {
        // Create a Ray to check where the ground is
        Ray ray = new Ray(transform.position, transform.up * -1);

        //See what it hits
        RaycastHit hit;

        //If the casted ray hits something under the player
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, raycastLength, mask))
        {
            if (debugTrigger) Debug.Log("There is a mask detected");

            //You are on ground
            return true;
        }
        else
        {
            //It returns false if you may not jump
            if (debugTrigger) Debug.Log("There isn't a mask detected");

            return false;
        }
    }

    //A method used to draw something in the scene, but not in-game
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * raycastLength * -1);
    }    
}