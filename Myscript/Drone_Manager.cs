using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone_Manager : MonoBehaviour
{


    public static Drone_Manager Instance;


    NavMeshAgent MyNavMesh;



    public string State;


    public float Wait;


    public Transform Target;

    public Transform[] PointTab;

    public int PointIdx;
    public float Amount;


    public bool CanShoot;
    public float FireRate;
    public GameObject ImpactEffect;

    public float Health = 100;
    public GameObject DeathDrone;
    public GameObject Drone_HealthBar;


    public float AttackDistance;
    public float ChaseDistance;
    public bool ActivateAttack;
    public GameObject FxDrone;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {


        MyNavMesh = GetComponent<NavMeshAgent>();
        //  MyAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {



        if (!CanShoot)
        {
            FireRate += Time.deltaTime;
            if (FireRate >=1f)
            {
                CanShoot = true;
            }

        }

        if (Health > 0 && PlayerController.instance.Health > 0)
        {
            if (State == "Walk")
            {
                Idle();
            }

            if (State == "Idle")
            {
                Walk();
            }

            if (CanSeeTarget(ChaseDistance))
            {
                MyNavMesh.destination = Target.position;
                MyNavMesh.stoppingDistance = 3;
                if (Vector3.Distance(Target.position, transform.position) >= MyNavMesh.stoppingDistance)
                {
                    transform.LookAt(Target.position, transform.up);

                    MyNavMesh.speed = 3.5f;


                    State = "Chase";

                }
            }
            else if (State == "Chase")
            {
                print("Chase");
                State = "Idle";

            }

            if (CanSeeTarget(AttackDistance))
            {

                print("Attack");

                MyNavMesh.destination = Target.position;
                MyNavMesh.stoppingDistance = 3;
                if (Vector3.Distance(Target.position, transform.position) >= MyNavMesh.stoppingDistance)
                {
                    RaycastHit hit;
                    Debug.DrawLine(transform.position, Target.position, Color.red);
                    if (Physics.Linecast(transform.position, Target.position, out hit))
                    {

                        if (hit.collider.tag == "Player")
                        {
                            if (CanShoot)
                            {
                                PlayerController.instance.Health -= 5;
                                CanShoot = false;
                                Debug.Log(hit.transform.name);
                                FireRate = 0;
                                GameObject ImpactGo = Instantiate(ImpactEffect, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.LookRotation(hit.normal));
                                Destroy(ImpactGo, 0.5f);
                                Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.DroneShootSound);
                            }

                        }
                        
                     
                    }
                    transform.LookAt(Target.position, transform.up);
                    //Get distance between enemy and player
                    float distance = Vector3.Distance(Target.position, transform.position);
                    MyNavMesh.speed = 0f;


                    State = "Attack";



                }
            }
            else if (State == "Attack")
            {
                State = "Idle";

            }



        }
        if (Health <= 0)
        {
            MyNavMesh.isStopped = true;

            Health = 0;
            Instantiate(DeathDrone, transform.position, transform.rotation);
            Destroy(gameObject);
            
           Camera_Manager.instance.Enemy_DeathCounter++;
             GameObject newFx = Instantiate(FxDrone, transform.position, transform.rotation) as GameObject;
             Destroy(newFx, 2);
        }


        Drone_HealthBar.GetComponent<RectTransform>().localScale = new Vector3(Health * 0.01f, 1, 1);
    }


    public void Walk()
    {
        MyNavMesh.stoppingDistance = 0;
        MyNavMesh.speed = 3.5f;


        if (PointTab.Length == 0)
        {
            return;
        }



        MyNavMesh.SetDestination(PointTab[PointIdx].position);

        PointIdx = (PointIdx + 1) % PointTab.Length;

        Wait = 2;

        State = "Walk";




    }


    public void Idle()
    {
        MyNavMesh.stoppingDistance = 0;
        if (!MyNavMesh.pathPending && MyNavMesh.remainingDistance <= MyNavMesh.stoppingDistance)
        {
            if (Wait > 0)
            {

                Wait -= Time.deltaTime;
                MyNavMesh.speed = 0;

            }
            else
            {


                State = "Idle";
            }

        }


    }



    public bool CanSeeTarget(float NearDistance)
    {

        NavMeshHit Hit;
        if (!NavMesh.Raycast(transform.position, Target.position, out Hit, 5))
        {

            if (Vector3.Distance(transform.position, Target.position) > NearDistance)
            {
                return false;
            }


            return true;
        }



        return false;

    }


    }
