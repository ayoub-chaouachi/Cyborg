using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Manager : MonoBehaviour {


    public GameObject[] LazerTab;

  

   public int LazerCounter;

    public int  randomIdx;

    public int random;


    public int LazerMaxShowed;
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < LazerTab.Length; i++)
        {
           
                LazerTab[i].GetComponent<LineRenderer>().enabled = false;
            
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        LazerCounter++;
        if (LazerCounter> LazerMaxShowed)
        {
            randomIdx = Random.Range(0, LazerTab.Length);


            random = Random.Range(0, 10);

            if (random==0 )
            {
                for (int i = 0; i < LazerTab.Length; i++)
                {
                    LazerMaxShowed = 80;
                    LazerTab[i].GetComponent<BoxCollider>().enabled = false;
                    LazerTab[i].GetComponent<LineRenderer>().enabled = false;
                   
                }
            }
            if (random == 5 || random == 4 || random == 6)
            {
                for (int i = 0; i < LazerTab.Length; i++)
                {
                    LazerMaxShowed = 45;
                       LazerTab[i].GetComponent<BoxCollider>().enabled = true;
                    LazerTab[i].GetComponent<LineRenderer>().enabled = true;

                }
            }
            if (random>6)
            {
                for (int i = 0; i < LazerTab.Length; i++)
                {
                    LazerMaxShowed = 45;
                    if (i == randomIdx)
                    {
                        LazerTab[i].GetComponent<BoxCollider>().enabled = false;
                        LazerTab[i].GetComponent<LineRenderer>().enabled = false;
                    }
                    else
                    {
                        LazerTab[i].GetComponent<BoxCollider>().enabled = true;
                        LazerTab[i].GetComponent<LineRenderer>().enabled = true;
                    }
                }
            }
          

           
            LazerCounter = 0;
        }
        

       
    }
}
