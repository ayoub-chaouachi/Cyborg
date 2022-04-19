using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.tag = "aaa";
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.RockSound);
        }
    }
}
