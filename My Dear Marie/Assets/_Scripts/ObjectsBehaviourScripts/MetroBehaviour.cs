using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetroBehaviour : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField]
    private float driveSpeed;

    [SerializeField]
    //With this boolean, you can switch the debugs in this script to on or off in the inspector
    private bool debugTrigger = false;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //The metro will move horizontaly with the set drive speed
        rigidbody.velocity = new Vector3(driveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        var marie = collision.gameObject.GetComponent<MarieController>();
        
        //If something with the MarieController connected to it collides with the metro
        if (marie)
        {
            //reset that thing and the metro to their start positions
            marie.GameOverScreen.SetActive(true);
        }
    }
}
