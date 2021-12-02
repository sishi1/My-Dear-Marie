using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : AbstractObject
{
    Rigidbody theBody;

    // Booleans to check if the correct state to move object is active.
    private bool canMove;
    public bool moveInLightMode, moveInDarkMode; 
    [SerializeField] private float fallingSpeed;

    // Initialise Rigidbody.
    private void Awake()
    {
        theBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Makes it so that the rigidbody olny interacts with other rigidbodys 
        // when in the correct state.
        if (canMove) theBody.isKinematic = false;
        else theBody.isKinematic = true;
    }

    // Sets own velocity as the players velocity when there is a collision
    // and resets on exit, this way the object can be pushed.
    private void OnTriggerEnter(Collider other)
    {
        if (canMove && other.GetComponent<MarieController>() 
            && other.attachedRigidbody.velocity.x != 0)
            theBody.velocity = new Vector3 (other.attachedRigidbody.velocity.x, 0, 0);
    }
    private void OnTriggerExit(Collider other)
    {
        if (canMove && other.GetComponent<MarieController>()) theBody.velocity = new Vector3(0, -fallingSpeed, 0);
    }

    public override void OnDarkModeEnter()
    {
        if (moveInDarkMode) canMove = true;
        else canMove = false;
    }

    public override void OnLightModeEnter()
    {
        if (moveInLightMode) canMove = true;
        else canMove = false;
    }
}
