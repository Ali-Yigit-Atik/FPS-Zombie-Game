using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator_;

    private GameObject helipad;
    private bool isInHelipad = false;
    [SerializeField] private Transform helicopterTarget;
    private bool getOnHeliCopter = false;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator_ = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        
        FollowPlayer();
        Animations();
        GoToHeliCopter();
        WaveRotation();

    }

    private void FollowPlayer() 
    {
        if (CageOpen.isCageOpen && !isInHelipad )//&& !getOnHeliCopter) 
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
        if (getOnHeliCopter && Vector3.Distance(helicopterTarget.position, transform.position) > 0.4f)  //(Mathf.Abs(helicopterTarget.position.y) - Mathf.Abs(transform.position.y))
        {
            agent.SetDestination(helicopterTarget.position);            
            Debug.Log("getOnHeliCopter");
            animator_.SetBool("running", true);
        }
        else if ( isInHelipad && getOnHeliCopter && Vector3.Distance(helicopterTarget.position, transform.position) <= 0.4f) 
        {
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
        //if(Vector3.Distance(transform.position, player.transform.position) > 4f) 
        //{
        //    animator_.SetBool("running", true);
        //}
        //else if(Vector3.Distance(transform.position, player.transform.position) <= 4f && !getOnHeliCopter) 
        //{
        //    animator_.SetBool("running", false);
        //}
        //if (getOnHeliCopter) 
        //{
        //    animator_.SetBool("shouldWave", false);
        //    animator_.SetBool("running", true);
        //}
        //
        //if(Vector3.Distance(helicopterTarget.position, transform.position) <= 0.4f) 
        //{
        //    animator_.SetBool("running", false);
        //}
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
