using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera_Manager : MonoBehaviour {

    public GameObject RayPoint;
    private LineRenderer Lr;

    public GameObject AlertLight;
    public bool Alerted;
    GameObject newLight;
    // Use this for initialization
    void Start ()
    {
        Lr = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;

       Debug.DrawLine(transform.position, RayPoint.transform.position,Color.red);
        Lr.SetPosition(1, new Vector3 (0, 0, Vector3.Distance(transform.position, RayPoint.transform.position)));

        if (Physics.Linecast(transform.position, RayPoint.transform.position, out hit))
        {
            Lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            if (hit.collider.tag == "Player")
            {
                if (!Alerted)
                {
                    newLight = Instantiate(AlertLight, new Vector3(hit.collider.transform.position.x,-4, hit.collider.transform.position.z), hit.collider.transform.rotation) as GameObject;
                    Alerted = true;
                    Time.timeScale = 0;
                    UiManager.instance.Lose_Panel.SetActive(true);
                    UiManager.instance.PausePanel.SetActive(false);
                    UiManager.instance.CursorVisibility = true;
                    Camera.main.GetComponent<Camera_Manager>().enabled = false;

                  
                    Debug.Log(hit.transform.name);
                }
               
              
            }
        }



       
    }
}
