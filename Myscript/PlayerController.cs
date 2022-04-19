using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public Animator Anim;
    private Rigidbody Rbody;
    private CapsuleCollider MyCollider;
    private float TurnAmount;
    private float ForwardAmount;
    public static PlayerController instance;
    private bool Walk;
    private float speedF = 2f;
    private float speedT = 2f;
    private Camera Cam;
    public Animator Dooranim;
    public Animator Dooranim1;
    public Animator Dooranim2;
    public Animator Dooranim3;
    public Animator Dooranim4;
    public Animator Dooranim5;
    public Animator Dooranim6;
    public Animator Dooranim7;
    public Animator PressE_Panel_Anim;

   

    private Transform PlayerRot;

    public float Health = 100;

    public GameObject HealthBar;


    public GameObject[] Trap_Anim;

    public GameObject[] Trap3SpawnPos;
 
    public GameObject Trap3;

    public GameObject[] Trap4SpawnPos;
    public GameObject Trap4;

    public bool PanelActive;

    public bool IsOnGround;

    public float Offset;

    public GameObject Loading_Img;

    public GameObject HpFx;
    public GameObject DeathFX;
    // Use this for initializationv
    private void Awake()
    {
        instance = this;
    }
    void Start () 
    {
        Anim = GetComponent<Animator>();
        Rbody = GetComponent<Rigidbody>();
        MyCollider = GetComponent<CapsuleCollider>();
        Cam = Camera.main;
        instance = this;
    }
	
	// Update is called once per frame
	void Update ()
    {

        Grounded();

        if (Health>0)
        {
            Move();
            crouching();
            jumping();


            Anim.SetBool("Walk", Walk);


        }

        if (Health<=0)
        {
            Anim.SetBool("Death", true);

            Health = 0;
            UiManager.instance.PausePanel.SetActive(false);
            

        }

        HealthBar.GetComponent<RectTransform>().localScale = new Vector3(Health * 0.01f, 1, 1);

        if (PanelActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (UiManager.instance.Code_Panel.activeSelf)
                {
                    UiManager.instance.Code_Panel.SetActive(false);
                    UiManager.instance.CursorVisibility = false;
                    Camera.main.GetComponent<Camera_Manager>().enabled = true;
                }
                else
                {
                    UiManager.instance.Code_Panel.SetActive(true);
                    UiManager.instance.CursorVisibility = true;
                    Camera.main.GetComponent<Camera_Manager>().enabled = false;
                }
            }
        }


    }
    
    

    void Move()
    {
        TurnAmount = Input.GetAxis("Horizontal");
        ForwardAmount = Input.GetAxis("Vertical");
        if (ForwardAmount != 0 || TurnAmount != 0)
        {
            Anim.SetFloat("InputH", TurnAmount);
            Anim.SetFloat("InputV", ForwardAmount);
            Walk = true;
      
            if (ForwardAmount > 0 )
            {
                 transform.Translate(Vector3.forward *Time.deltaTime *speedF , Space.Self);
           
            }
            if (ForwardAmount < 0 )
            {
                transform.Translate(Vector3.back * Time.deltaTime * 1, Space.Self);
            }
            if (TurnAmount> 0 )
            {
                transform.Translate(Vector3.right * Time.deltaTime * speedT, Space.Self);
            }
            if (TurnAmount < 0 )
            {
                transform.Translate(Vector3.left * Time.deltaTime * speedT, Space.Self);
            }
            if (Input.GetKey(KeyCode.LeftShift)&& ForwardAmount>0  )
            {  
                speedF =5f;
                speedT = 2.5f;
                Anim.SetBool("Run", true);
            }
            else
            {
                Anim.SetBool("Run", false);
                speedF = 2f;
                speedT = 2f;
            }    
        }
        else
        {
            Walk = false;
        }  
    }
    void crouching()
    {
        if (Input.GetKey(KeyCode.C) && IsOnGround)
        {
            Anim.SetBool("Crouch", true);

        }
        else
            Anim.SetBool("Crouch", false);

    }
    void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& IsOnGround)
        {
            
            Anim.SetBool("Jump", true);
            Rbody.AddForce(Vector3.up*250);

            if (ForwardAmount > 0.1)
            {
                Rbody.AddForce(transform.TransformDirection(Vector3.forward)* 50);
                    
            }
        }
        else {
            Anim.SetBool("Jump", false);

        }
    }



   


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Door")
        {
            Dooranim.SetBool("Open_Door", true);
            //Dooranim.SetBool("Close_Door", false);
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);
        }
        if (collider.gameObject.tag == "DoorP")
        {
            Dooranim1.SetBool("Open_DoorP", true);
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);

        }
        if (collider.gameObject.tag == "DoorS")
        {
            Dooranim2.SetBool("Open_DoorS", true);
            Drone_Manager.Instance.ActivateAttack = true;
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);

        }
        if (collider.gameObject.tag == "Door_C")
        {
           
            Drone_Manager.Instance.ActivateAttack = true;
            

        }
        if (collider.gameObject.tag == "Door_LC")
        {
            Dooranim5.SetBool("Open_Door_LC", true);
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);

        }


        if (collider.gameObject.tag == "Door_SR2")
        {
            Dooranim6.SetBool("Open_DoorR1", true);
            Dooranim7.SetBool("Open_DoorL1", true);
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.FirstDoorSound);
            print("aa");
        }

        if (collider.gameObject.tag == "Lazer")
        {
            Health = 0;
            // GameObject newFx = Instantiate(, transform.position, transform.rotation) as gameObject;
            //  Destroy(newFx, 2);
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.LazerSound);
        }

        if (collider.gameObject.tag == "Block")
        {
            Health = 0;
        }

        if (collider.gameObject.tag == "Trap2")
        {
            Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.BarrierSound);
            for (int i = 0; i < Trap_Anim.Length; i++)
            {
                Trap_Anim[i].GetComponent<Animator>().enabled = true;

            }
        }

        if (collider.gameObject.tag == "Spawn_Block")
        {
            Destroy(collider.gameObject);

            for (int i = 0; i < Trap3SpawnPos.Length; i++)
            {
                GameObject newTrap = Instantiate(Trap3, Trap3SpawnPos[i].transform.position, Trap3.transform.rotation) as GameObject;
            }
            
          

        }
        if (collider.gameObject.tag == "Spawn_Rock2")
        {
            Destroy(collider.gameObject);

            for (int i = 0; i < Trap4SpawnPos.Length; i++)
            {
                GameObject newTrap = Instantiate(Trap4, Trap4SpawnPos[i].transform.position, Trap4.transform.rotation) as GameObject;
            }



        }
        if (collider.gameObject.tag == "PressE")
        {
            PressE_Panel_Anim.SetBool("Open_PanelE", true);

            PanelActive = true;
        }
        if (collider.gameObject.tag == "Potion")
        {
            if (Health<100)
            {
                Health += 10;
                GameObject newFx = Instantiate(HpFx, collider.gameObject.transform.position, HpFx.transform.rotation) as GameObject;
                Destroy(newFx, 2);
                Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.PowerUp);

            }

            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Level3")
        {
            StartCoroutine(Loading(3));

            
        }
        if (collider.gameObject.tag == "FinalColl")
        {
            Time.timeScale = 0;
            UiManager.instance.Win_Panel.SetActive(true);
            UiManager.instance.CursorVisibility = true;
            UiManager.instance.Crosshair.SetActive(false);
            Camera.main.GetComponent<Camera_Manager>().enabled = false;

        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PressE")
        {
            PressE_Panel_Anim.SetBool("Open_PanelE", false);

            PanelActive = false;
        }
   


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Rock")
        {
            Health = 0;
        }
    }



    public void S1()
    {
        UiManager.instance.Lose_Panel.SetActive(true);
        UiManager.instance.PausePanel.SetActive(false);
        UiManager.instance.CursorVisibility = true;
        Destroy(gameObject);
         GameObject newFx = Instantiate(DeathFX, new Vector3 (transform.position.x, transform.position.y+0.5f, transform.position.z+0.5f), transform.rotation) as GameObject;
         Destroy(newFx, 3);
    }

   

    public void Grounded()
    {


        Vector3 leftRay;
        Vector3 rightRay;

        leftRay = MyCollider.bounds.center;

        rightRay = MyCollider.bounds.center;

        leftRay.x -= MyCollider.bounds.extents.x;
        rightRay.x += MyCollider.bounds.extents.x;

        Debug.DrawRay(leftRay, Vector3.down*Offset, Color.cyan);
        Debug.DrawRay(rightRay, Vector3.down * Offset, Color.red);

        if (Physics.Raycast(leftRay, Vector3.down, Offset))
        {
            print("Is grounded");
            IsOnGround = true;


        }
        else
        {
            print("Is not grounded");
            IsOnGround = false;

        }
        if (Physics.Raycast(rightRay, Vector3.down, Offset))
        {
            print("Is grounded");
            IsOnGround = true;



        }
        else
        {
            print("Is not grounded");
            IsOnGround = false;

        }



    }
    IEnumerator Loading(int sceneNbr)
    {

        Loading_Img.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneNbr);


    }

}

