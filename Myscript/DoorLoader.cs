using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLoader : MonoBehaviour {


    public GameObject Loading_Img;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "aaa")
        {
            StartCoroutine(Loading(2));

        }
    }


    IEnumerator Loading(int sceneNbr)
    {

        Loading_Img.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneNbr);

    }
    }
