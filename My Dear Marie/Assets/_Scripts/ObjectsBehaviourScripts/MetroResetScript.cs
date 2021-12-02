using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroResetScript : MonoBehaviour
{
    MetroBehaviour metro;
    Rigidbody metroRigidBody;

    [SerializeField]
    //Set the startposition to (0, 0, 0) in the scene. May be altered in the inspector
    private Vector3 Startposition = Vector3.zero;

    private void Awake()
    {
        //Find the script of the metro
        metro = FindObjectOfType<MetroBehaviour>();
        //Set the metro rigidbody to the script of the metro
        metroRigidBody = metro.gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If there is a collision with the metro, reset it to its set startposition
        if (metro)
        {
            metroRigidBody.position = Startposition;
        }
    }
}
