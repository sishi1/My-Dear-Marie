using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPosition;
    public GameObject camera;
    public float parallaxStrength;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;

        //get the length of the sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //How far moved relative to the camera
        float temporary = (camera.transform.position.x * (1 - parallaxStrength));

        //how far moved in world space
        float distance = (camera.transform.position.x * parallaxStrength);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        //if the camera reaches the right side of the background
        if (temporary > startPosition + length) startPosition += length;
        //if the camera reaches the left side of the background
        else if (temporary < startPosition - length) startPosition -= length;
    }
}
