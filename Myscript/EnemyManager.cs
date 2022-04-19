using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{


     NavMeshAgent MyNavMesh;

    
    Animator MyAnimator;

 
    public string State;

    
    public  float Wait;


    public Transform Target;

    public Transform[] PointTab;

    public int PointIdx;
    public float Amount;
    public static EnemyManager instance;

    public bool CanShoot;
    public float FireRate;
    public GameObject ImpactEffect;


    public float AttackDistance;
    public float ChaseDistance;

    public float Health = 100;
    public GameObject Enemy_HealthBar;

    public GameObject HealthPotion;
    public GameObject FxEnemy;


    // Use this for initialization
    void Start()
    {
        instance = this;

        MyNavMesh = GetComponent<NavMeshAgent>();
        MyAnimator = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanShoot)
        {
            FireRate += Time.deltaTime;
            if (FireRate >= 0.5f)
            {
                CanShoot = true;
            }

        }

        if (Health>0 && PlayerController.instance.Health>0)
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

                if (Vector3.Distance(Target.position, transform.position) >= MyNavMesh.stoppingDistance)
                {
                    transform.LookAt(Target.position, transform.up);
                    //Get distance between enemy and player
                    float distance = Vector3.Distance(Target.position, transform.position);
                    MyNavMesh.speed = 1.8f;

                    MyAnimator.SetTrigger("Walk");
                    State = "Chase";

                }
            }
            else if (State == "Chase")
            {
                print("Chase");
                State = "Idle";
                MyAnimator.ResetTrigger("Walk");
            }

            if (CanSeeTarget(AttackDistance))
            {

               print("Attack");
                MyNavMesh.destination = Target.position;

                if (Vector3.Distance(Target.position, transform.position) >= MyNavMesh.stoppingDistance)
                {
                    RaycastHit hit;
                    Debug.DrawLine(transform.position, Target.position, Color.red);
                    if (Physics.Linecast(transform.position, Target.position, out hit))
                    {
                        
                        if (hit.collider.tag=="Player")
                        {
                            if (CanShoot)
                            {
                                PlayerController.instance.Health -= 1.5f;
                                CanShoot = false;

                                FireRate = 0;

                                Debug.Log(hit.transform.name);
                                GameObject ImpactGo = Instantiate(ImpactEffect, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.LookRotation(hit.normal));
                                Destroy(ImpactGo, 0.5f);

                                Camera_Manager.instance.SourceSound.PlayOneShot(Camera_Manager.instance.EnemyShootSound);
                            }
                        }

                        }
                       
                      
                    transform.LookAt(Target.position, transform.up);
                    //Get distance between enemy and player
                    float distance = Vector3.Distance(Target.position, transform.position);
                    MyNavMesh.speed = 0f;

                    MyAnimator.SetTrigger("Aim");
                    MyAnimator.ResetTrigger("Walk");
                    State = "Attack";



                }
            }
            else if (State == "Attack")
            {
                State = "Idle";
                MyAnimator.ResetTrigger("Aim");
            }


          
        }
        if (Health<=0)
        {
            
            MyNavMesh.isStopped = true;
            MyAnimator.SetTrigger("Death");
           Health=0;
            
           
        }

        Enemy_HealthBar.GetComponent<RectTransform>().localScale = new Vector3(Health * 0.01f, 1, 1);

    }


    public void Walk()
    {

        MyNavMesh.speed = 1.5f;
      

        if (PointTab.Length == 0)
        {
            return;
        }



        MyNavMesh.SetDestination(PointTab[PointIdx].position);

        PointIdx = (PointIdx + 1) % PointTab.Length;

        Wait = 3;
        MyAnimator.SetTrigger("Walk");
        State = "Walk";




    }


    public void Idle()
    {
        if (!MyNavMesh.pathPending && MyNavMesh.remainingDistance <= MyNavMesh.stoppingDistance)
        {
            if (Wait > 0)
            {
                MyAnimator.SetTrigger("Idle");
                Wait -= Time.deltaTime;
                MyNavMesh.speed = 0;
            }
            else
            {
                MyAnimator.ResetTrigger("Idle");

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
                return false;

            return true;
        }

        return false;
    }

    public void S2()
    {
        Destroy(gameObject);
        Instantiate(HealthPotion, new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z), transform.rotation);
        GameObject newFx = Instantiate(FxEnemy, transform.position, transform.rotation) as GameObject;
         Destroy(newFx, 2);

    }



}
