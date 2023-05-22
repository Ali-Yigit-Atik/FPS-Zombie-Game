using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{

    // player'a can azaltma yapýlacak
   

    [HideInInspector] public GameObject targetPlayer;
    [SerializeField] private float followDistance = 10;
    
    private float beforeZombieShotDistance;
    private float distanceBetweenZombieAndPlayer;


    
    private float wanderRadius = 100f;
    private NavMeshAgent agent;

    
    private Vector3 patrolPos = new Vector3();
    private bool isStartingPatrol = true;

    private float patrolSpeed;    

    private Animator animator_;   
    private bool isWalking = false;


    private ZombieHealth zombieHealth_;
    private GameObject player;

    private ChildHealth childHealth_;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator_ = GetComponent<Animator>();
        zombieHealth_ = GetComponent<ZombieHealth>();

        patrolSpeed = agent.speed;
        beforeZombieShotDistance = followDistance;
        player = GameObject.FindGameObjectWithTag("Player");
        

    }

    
    private void OnDisable() // when zombie die values need to reset
    {
        ResetValues();
    }

    private void Update()
    {
        

        if (zombieHealth_.zombieIsDead == false)
        {

            if (targetPlayer == null) { Patrol(); }

            followPlayer();
        }
        else
        {
            agent.SetDestination(transform.position);
            
        }

        IsTargetDead(); // it is define to not follow target anymore

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
        if(targetPlayer !=null && Vector3.Distance(transform.position, targetPlayer.transform.position) < 2) 
        {
            if (targetPlayer.CompareTag("Player")) 
            { targetPlayer.GetComponent<CharacterHealth>().getDamage(10, 6); }
            else if (targetPlayer.CompareTag("Child")) 
            {
                targetPlayer.GetComponent<ChildHealth>().GetDamage(10, 6);
            }

        }
    }

    private void IsTargetDead() 
    {
        if(targetPlayer != null) 
        {
            if (targetPlayer.CompareTag("Child"))
            {
                if (childHealth_ == null) childHealth_ = targetPlayer.GetComponent<ChildHealth>(); // for just one time get component in update
                if (childHealth_.isChildDead)
                {
                    ResetValues();
                }
            }
            

            if (CharacterHealth.isDead) ResetValues();
        }

    }

    private void ResetValues()
    {
        targetPlayer = null;
        childHealth_ = null;
        followDistance = beforeZombieShotDistance;
        agent.speed = patrolSpeed;

    }

}
