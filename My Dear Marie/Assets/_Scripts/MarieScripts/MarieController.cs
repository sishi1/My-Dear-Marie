using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MarieController : MonoBehaviour
{
    Rigidbody body;
    float horizontal;
    [HideInInspector]
    public GameObject GameOverScreen;

    [SerializeField]
    private KeyCode climbKey;

    public float moveSpeed;
    public Vector3 spawnPosition;
    public AudioSource walkingSound, climbingSound;

    // Enum to check the movement state of Marie
    public enum Movement { walking, climbing };
    [HideInInspector]
    public Movement currentMovement = Movement.walking;

    public Vector3 GetStartPosition { get { return spawnPosition; } private set { } }

    private bool ShouldPlaySound { get { return body.velocity.x != 0 && body.velocity.y == 0; } }

    // Start is called before the first frame update
    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody>();
        GameOverScreen = FindObjectOfType<IsGameOverScreen>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        HandleObjectCollision();
    }

    public void SetToStart()
    {
        this.transform.position = GetStartPosition;
    }

    private void UpdateMovement()
    {
        if (DialogueTrigger.activated)
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
            walkingSound.Stop();
            climbingSound.Stop();
            return;
        }

        switch (currentMovement)
        {
            case Movement.walking:
                //Stop walking climbing sound when the player walks again
                if (climbingSound.isPlaying) climbingSound.Stop();
                //Movement
                body.velocity = new Vector3(horizontal * moveSpeed, body.velocity.y);
                body.useGravity = true;
                //Handles walking sound
                if (ShouldPlaySound && !walkingSound.isPlaying) walkingSound.Play();
                else if (!ShouldPlaySound) walkingSound.Stop();
                break;
            case Movement.climbing:
                if (Input.GetKey(climbKey))
                {
                    //Movement
                    body.velocity = new Vector3(horizontal * moveSpeed, 0f, body.velocity.z);
                    body.useGravity = false;
                    //Handles climbing sound 
                    if (ShouldPlaySound && !climbingSound.isPlaying) climbingSound.Play();
                    else if (!ShouldPlaySound) climbingSound.Stop();
                }
                else
                {
                    climbingSound.Stop();
                    currentMovement = Movement.walking;
                }
                break;
            default:
                Debug.Log("Somehow ya boy (girl actually) has no movement at all...");
                break;
        }
    }

    private void HandleObjectCollision()
    {
        // Code for checking if current velocity will result into collisions.

        Vector3 forward = body.velocity.normalized;
        forward.y = 0;

        // Get position between ground and middle off the players body.
        Vector3 playerFeet = transform.position;
        playerFeet.y -= 0.8f;

        if (Physics.Raycast(transform.position, forward, 0.50001f)
            || Physics.Raycast(playerFeet, forward, 0.51f))
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
        }

        Debug.DrawRay(transform.position, forward * 0.5f, Color.white);
        Debug.DrawRay(playerFeet, forward * 0.5f, Color.white);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPosition, 0.05f);
    }
#endif
}
