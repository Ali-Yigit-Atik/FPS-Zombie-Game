using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class zombieMovement : MonoBehaviour
{

    // player'a can azaltma yapýlacak
   

    [HideInInspector] public GameObject targetPlayer;
    [SerializeField] private float followDistance = 10;
    
    private float beforeZombieShotDistance;
    private float distanceBetweenZombieAndPlayer;


    private float timerPatrol = 10; //
    private float wanderTimer = 10f; //
    private float wanderRadius = 100f;
    private NavMeshAgent agent;

    private float timeGapPatrol = 2f; //
    private float gapTimer = 0; //


    private Vector3 patrolPos = new Vector3();
    private bool isStartingPatrol = true;


    private float patrolSpeed;
    private float followSpeed;//
    private float speedWhileAttack; //


    private Animator animator_;
    
    
    // animation bools

    private bool isWalking = false;


    private zombieHealth zombieHealth_;
    private GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator_ = GetComponent<Animator>();
        zombieHealth_ = GetComponent<zombieHealth>();

        patrolSpeed = agent.speed;
        beforeZombieShotDistance = followDistance;
        player = GameObject.FindGameObjectWithTag("Player");
        

    }

    
    private void OnDisable() // when zombie die target player need to reset
    {
        targetPlayer = null;
        followDistance = beforeZombieShotDistance;
        agent.speed = patrolSpeed;
    }

    private void Update()
    {


        if (zombieHealth_.zombieIsDead == false)
        {

            if (targetPlayer == null) { Patrol2(); }

            followPlayer();
        }
        else
        {
            agent.SetDestination(transform.position);
            
        }

        animations();

        
    }
    
    private void followPlayer()
    {

        if (targetPlayer != null)
        {
            distanceBetweenZombieAndPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);
            Debug.Log("null check çalýþýyor");
            if (distanceBetweenZombieAndPlayer < followDistance )
            {
                var playerRotation = new Vector3(targetPlayer.transform.position.x, transform.position.y, targetPlayer.transform.position.z) - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), 7f * Time.deltaTime);

                agent.SetDestination(targetPlayer.transform.position);
                speedAndAttack();
            }
            else
            {
                targetPlayer = null;
                agent.speed = patrolSpeed;

            }

            if(distanceBetweenZombieAndPlayer < beforeZombieShotDistance/4f) 
            {
                followDistance = beforeZombieShotDistance;
            }
        }
    }

    private void speedAndAttack() 
    {
        //float dist = Vector3.Distance(transform.position, targetPlayer.transform.position);


        if (distanceBetweenZombieAndPlayer > followDistance / 1.5f) 
        {
            agent.speed = 10 *(1 -(distanceBetweenZombieAndPlayer) /followDistance);
        }
        else if (distanceBetweenZombieAndPlayer <= followDistance/1.5f)
        {
            agent.speed= 10 * ((distanceBetweenZombieAndPlayer) / (followDistance-1))+2;
        }
    }


    private void Patrol()
    {

        timerPatrol += Time.deltaTime;
        Debug.Log("yürüyor");

        if (timerPatrol >= wanderTimer)
        {
            Vector3 newPos = RandomNavPosition(transform.position, wanderRadius, -1);
            //Vector3 newPos = RandomPoint(transform.position, 100);
            agent.SetDestination(newPos);
            timerPatrol = 0;
            Debug.Log("wait");

            
        }

    }
    private void Patrol2()
    {

        isWalking = true;
        
        if (isStartingPatrol) 
        {
            patrolPos = RandomNavPosition(transform.position, wanderRadius,-1);
            isStartingPatrol = false;
        }
        
        
        if(Vector3.Distance(transform.position, patrolPos) < 4  || Mathf.Abs(transform.position.y - patrolPos.y)>10f )
        {
            patrolPos = RandomNavPosition(transform.position, wanderRadius, -1);
        }
        

        agent.SetDestination(patrolPos);

    }


    private Vector3 RandomNavPosition(Vector3 origin, float radius, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * radius;

        randDirection += origin;

        NavMeshHit navHit;

        //NavMesh.SamplePosition(randDirection, out navHit, radius, layermask);

        //return navHit.position;

        if (NavMesh.SamplePosition(randDirection, out navHit, radius, NavMesh.GetAreaFromName("zombieCantWalk")))
        {
            return navHit.position;
        }
        else 
        { 
            return Vector3.zero;
        }

        
    }

    private Vector3 RandomPoint(Vector3 center, float range)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;

        }
        return Vector3.zero;
        
    }


    private void waitBeforePatrol() 
    {
        
        gapTimer += Time.deltaTime;
        if (gapTimer > timeGapPatrol) 
        {
            
            gapTimer = 0;
        }
       
        
    }


    private void animations()
    {
        animator_.SetBool("walk", isWalking);

        if(targetPlayer != null) 
        {
            animator_.SetBool("isFollowing", true);
        }
        else animator_.SetBool("isFollowing", false);

        if(targetPlayer != null) 
        {
            //float dist =Vector3.Distance(transform.position, targetPlayer.transform.position);

            if(followDistance != beforeZombieShotDistance) 
            {
                if (distanceBetweenZombieAndPlayer < beforeZombieShotDistance / 4)
                {
                    animator_.SetFloat("dangerPercent", (((followDistance - distanceBetweenZombieAndPlayer) / (followDistance + 2)) * 2.5f));
                }
                else animator_.SetFloat("dangerPercent", 1.5f);
            }
            else animator_.SetFloat("dangerPercent", (((followDistance - distanceBetweenZombieAndPlayer) / (followDistance + 2)) * 2.5f));



        }

        if (zombieHealth_.zombieIsDead) 
        {
            animator_.SetBool("isDead", true);
        }
        else 
        {
            animator_.SetBool("isDead", false);
        }
    }

    
    public void whileZombieGetShotFarAwayFollowDistance() // uzaktan ateþ edince player'ý görüp saldýrsýn
    {
        

        if(targetPlayer == null || targetPlayer.gameObject.CompareTag("Child")) 
        {
            followDistance = Vector3.Distance(transform.position, player.transform.position) + 15f;
            targetPlayer = player;
        }
        
    }



    private void attack() //call in animator
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) < 2) 
        {
            if (targetPlayer.CompareTag("Player")) 
            { targetPlayer.GetComponent<characterHealth>().getDamage(10, 6); }
            // else if (targetPlayer.CompareTag("Child"))

        }
    }

}
