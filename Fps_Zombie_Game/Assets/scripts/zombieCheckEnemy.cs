using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieCheckEnemy : MonoBehaviour
{

    //public float checkRadius = 5f;
    RaycastHit hit;

    private zombieMovement zombieMovement_;

    //private RaycastHit hit;
    private bool isHit;
    

    Vector3 playerPosition;
    [SerializeField] private LayerMask platformLayerMask;


    private void Start()
    {
        
        zombieMovement_ = gameObject.transform.parent.gameObject.GetComponent<zombieMovement>();
    }




    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Child")) && zombieMovement_.targetPlayer ==null) // || other.gameObject.CompareTag("Child")
        {
            Debug.Log("player check çalýþýyor");

            playerPosition = other.gameObject.transform.position;

            
            

            Ray ray = new Ray(transform.position, playerPosition - transform.position);

            isHit = Physics.Raycast(ray, out hit, 10,platformLayerMask);

            if (isHit && (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Child")))
            {
                zombieMovement_.targetPlayer = hit.collider.gameObject;

                Debug.Log("it is a "+hit.collider.name);

            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHit = false;
    }


    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, hit.point - transform.position);
            Debug.Log("isHit çalýþýyor");
        }
    }

    
}
