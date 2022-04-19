using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastshooter : MonoBehaviour {

    public float range = 100;
    public Vector2 inaccuracy = new Vector2(1, 1);
    public float timeGap = 0.5f;
    public float damage =50;
    float lastShot = 0;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Shoot()
    {
        if (Time.time - lastShot > timeGap)
        {
            lastShot = Time.time;
        }
        else
        {
            return;
        }
        float rotX = Random.Range(-inaccuracy.x, inaccuracy.x);
        float rotY = Random.Range(-inaccuracy.y, inaccuracy.y);

        transform.Rotate(rotX, rotY,0);

        RaycastHit hit;
        if(Physics.Raycast (new Ray(transform.position, transform.forward),out hit, range))
        {
            float scaledamage = Mathf.Lerp(0, damage, 1 - (hit.distance / range));
            Vector3 hitPosition = hit.point;

            ShootHit s;
            s.damage = scaledamage;
            s.hitPosition = hitPosition;
            hit.transform.gameObject.SendMessage("OnRaycastHIt", s, SendMessageOptions.DontRequireReceiver);
           

        }
        SendMessage("OnShoot", SendMessageOptions.DontRequireReceiver);
        transform.Rotate(-rotX, -rotY, 0);
    }
}
public struct ShootHit
{
    public float damage;
    public Vector3 hitPosition;
}