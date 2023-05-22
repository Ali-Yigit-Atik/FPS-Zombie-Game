using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator_;
    
    private bool isInHelipad = false;
    [SerializeField] private Transform helicopterTarget;
    private bool getOnHeliCopter = false;

    private ChildHealth childHealth_;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator_ = GetComponent<Animator>();
        childHealth_ = GetComponent<ChildHealth>();


    }

    
    private void Update()
    {

        
        if (!childHealth_.dontMoveWhenTakeHit) FollowPlayer();
        else if (childHealth_.dontMoveWhenTakeHit) agent.SetDestination(transform.position);
        Animations();
        GoToHeliCopter();
        WaveRotation();

        if (childHealth_.isChildDead) GetComponent<ChildAI>().enabled =false; // dont run anymore

    }

    private void FollowPlayer() 
    {
        if (CageOpen.isCageOpen && !isInHelipad )
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 4f) 
            {
                agent.SetDestination(player.transform.position);
                animator_.SetBool("running", true);
            }
            else 
            {
                agent.SetDestination(transform.position);
                animator_.SetBool("running", false);
            }
        }
    }
    private void WaveRotation()
    {
        if (!getOnHeliCopter && isInHelipad)
        {
            var playerRotation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), 7f * Time.deltaTime);
        }
    }

    private void GoToHeliCopter() 
    {
        if (getOnHeliCopter && Vector3.Distance(helicopterTarget.position, transform.position) > 0.4f)  
        {
            agent.SetDestination(helicopterTarget.position);            
            Debug.Log("getOnHeliCopter");
            animator_.SetBool("running", true);
        }
        else if ( isInHelipad && getOnHeliCopter && Vector3.Distance(helicopterTarget.position, transform.position) <= 0.4f) 
        {
            if (animator_.GetBool("running")) // this if check for run one time only below codes
            {
                Helicopter.howManyChildInHelicopter++;
                transform.parent = helicopterTarget.transform;
                agent.enabled = false;
            }
            animator_.SetBool("running", false);
            Debug.Log("!getOnHeliCopter");
        }

    }

    
    private void Animations() 
    {
        if (CageOpen.isCageOpen) 
        {
            animator_.SetBool("rescued", true);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Helipad") && !getOnHeliCopter) 
        {
            animator_.SetBool("shouldWave", true);
            isInHelipad = true;
            agent.SetDestination(transform.position);

            
        }
    }

    private void isWaveDone() 
    {
        getOnHeliCopter = true;
        
        animator_.SetBool("shouldWave", false);        
        agent.radius = 0.15f;
        Debug.Log("isWaveDone");
    }

    
}
