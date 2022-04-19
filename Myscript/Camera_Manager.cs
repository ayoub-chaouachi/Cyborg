using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Camera_Manager : MonoBehaviour
{
    public static Camera_Manager instance;


    private float Zoom;
    private float ZoomMax = -0.35f;
    private float ZoomMin = -0.126f;
    private GameObject player;
    
    [SerializeField] private MouseLook m_MouseLook;
    private Vector3 m_OriginalCameraPosition;


    public GameObject Object_Follow;
    public GameObject Crosshair;
    public GameObject ImpactEffect;



    public int Enemy_DeathCounter;
    public GameObject DoorCollider;

    public bool CanShoot;
    public float FireRate;

    Scene m_Scene;


    public AudioSource SourceSound;

    public AudioClip PlayerShootSound;

    

    public AudioClip EnemyShootSound;

    

    public AudioClip DroneShootSound;

    public AudioClip PowerUp;


    public AudioClip FirstDoorSound;

    public AudioClip LazerSound;

    public AudioClip BarrierSound;

    public AudioClip RockSound;

    public AudioClip ClickSound;

    public GameObject FinalColl;
    private void Awake()
    {
        instance = this;

        m_Scene = SceneManager.GetActiveScene();
    }







    // Use this for initialization
    void Start()
    {
        Zoom = -2.5f;
        player = GameObject.FindGameObjectWithTag("Player");

        m_MouseLook.Init(player.transform, transform);
        m_OriginalCameraPosition = transform.localPosition;



    }



    // Update is called once per frame
    void Update()
    {
        //print(m_Scene.name);
        if (m_Scene.name == "Level1")
        {
            if (Enemy_DeathCounter == 2)
            {
                DoorCollider.SetActive(true);
            }
        }
        if (m_Scene.name == "Level2")
        {
            if (Enemy_DeathCounter == 2)
            {
                DoorCollider.SetActive(true);
            }
        }

        if (m_Scene.name == "Level3")
        {
            if (Enemy_DeathCounter == 2)
            {
                FinalColl.SetActive(true);
                
            }
        }



        if (!CanShoot)
        {
            FireRate+=Time.deltaTime;
            if (FireRate>=0.3f)
            {
                CanShoot = true;
            }

        }
        if (PlayerController.instance.Health > 0)
        {
            RotateView();
            CrosshairMvt();

            if (Input.GetMouseButton(1))
            {



                Zoom = ZoomMin;
                transform.localPosition = new Vector3(0.058f, 0.186f, Zoom);
                PlayerController.instance.Anim.SetBool("Run", false);
                PlayerController.instance.Anim.SetBool("Aim", true);



            }
            else
            {

                Zoom = ZoomMax;
                transform.localPosition = new Vector3(0.06f, 0.2f, Zoom);
                PlayerController.instance.Anim.SetBool("Aim", false);

            }

            transform.SetParent(player.transform);

        }
        else
        {
            transform.SetParent(Object_Follow.transform);
            Crosshair.SetActive(false);
        }

    }



    private void RotateView()
    {
        m_MouseLook.LookRotation(player.transform, transform);

    }


    public void CrosshairMvt()
    {
        if (Input.GetMouseButton(1))
        {


            RaycastHit hit;



            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward * 20, Color.red);

                if (Input.GetMouseButtonDown(0))
                {
                    SourceSound.PlayOneShot(PlayerShootSound);
                    Debug.Log(hit.transform.name);

                    if (hit.collider.tag == "Enemy")
                    {
                        if (CanShoot)
                        {
                            hit.transform.GetComponent<EnemyManager>().Health -= 10;
                            CanShoot = false;
                            FireRate = 0;
                           
                        }

                    }
                    if (hit.collider.tag == "Drone")
                    {
                        if (CanShoot)
                        {
                            hit.transform.GetComponent<Drone_Manager>().Health -= 30;
                            CanShoot = false;
                            FireRate = 0;
                            
                        }
                    }
                    GameObject ImpactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(ImpactGo, 0.2f);
                }
                
            }

        }
    }



  
}
