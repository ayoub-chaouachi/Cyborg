using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Target : MonoBehaviour {

    
    public GameObject player;     


    private Vector3 offset;        

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (PlayerController.instance.Health>0)
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;
        }
        
    }
}
