using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineBehaviour : MonoBehaviour
{
    public GameObject startOfLine, endOfLine;
    ChargeableObject theZiplinesCharge;
    MarieController thePlayer;
    private Vector3 startPosition, endPosition;
    private Vector3 ziplineVelocity;
    bool AtEndPoint { get { return (gameObject.transform.position - endPosition).sqrMagnitude <= 0.1; } }
    [SerializeField]
    private float zipLineSpeed;
    public bool movesBothWays;
    private AudioSource ziplineSound;
    private bool isMoving;

    void Awake()
    {
        theZiplinesCharge = GetComponent<ChargeableObject>();
        thePlayer = FindObjectOfType<MarieController>();
        ziplineSound = gameObject.GetComponent<AudioSource>();

        // Initialise start and end position of the zipline.
        startPosition = startOfLine.transform.position;
        endPosition = endOfLine.transform.position;

        // Set the zipline on start position.
        gameObject.transform.position = startPosition;

        // Calculate zipline velocity
        ziplineVelocity = (endPosition - startPosition) * (zipLineSpeed/150); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movesBothWays)
        {
            if (AtEndPoint)
            {
                AfterUsage();
                // Swap start and end position of zipline
                Vector3 previousStartPosition = startPosition;
                startPosition = endPosition;
                endPosition = previousStartPosition;

                // Recalculate zipline velocity
                ziplineVelocity = (endPosition - startPosition) * (zipLineSpeed / 150);
            }
        }
        // One way ziplines
        else
            if (AtEndPoint)
            {
                AfterUsage();
                // Special condition in this case is that the zipline can't be used the other way around
                theZiplinesCharge.SpecialChargeCondition = false;
            }
    }

    // OnTriggerEnter is purely to fix sound bug
    private void OnTriggerEnter(Collider other)
    {
        if (theZiplinesCharge.IsFullyCharged & !AtEndPoint)
            ziplineSound.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        // Moves 'Swing' if player is on it
        if (theZiplinesCharge.IsFullyCharged & !AtEndPoint)
        {
            gameObject.transform.position += ziplineVelocity;
            SetPlayerPosition();
        }
    }

    // Makes the player move correctly with the ziplines seat
    private void SetPlayerPosition()
    {
        thePlayer.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                thePlayer.gameObject.transform.position.y, thePlayer.gameObject.transform.position.z);
    }

    // Code for after usage of both type of ziplines 
    private void AfterUsage()
    {
        theZiplinesCharge.IsUsed = true;
        ziplineSound.Stop();
    }
}
