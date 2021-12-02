using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToPlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    //Variable to store the offset distance between the player and camera
    private Vector3 offset;

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Set the position of the camera's transform to be the same as the player, but with a offset
        transform.position = player.transform.position + offset;
    }
}
