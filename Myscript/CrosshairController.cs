using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {
    public Texture2D image;
    public int size;
    public float maxAngle;
    public float minAngle;
    public static CrosshairController instance;
    public float lookHeight;
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
       
		
	}
	
	// Update is called once per frame
	void Update () {
        LookUp(lookHeight);

    }
    public void LookUp(float value)
    {
        lookHeight += value;
        if (lookHeight > maxAngle || lookHeight < minAngle)
        {
            lookHeight -= value;
        }

    }
    void OnGUI()
    {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image);
     }
    
       
    

    
}
