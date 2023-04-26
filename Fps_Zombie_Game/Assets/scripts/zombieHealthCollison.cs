using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHealthCollison : MonoBehaviour
{
    [SerializeField] private bool isHead = false;
    private zombieHealth health_;

    private void Start()
    {
        health_ = gameObject.transform.parent.gameObject.GetComponent<zombieHealth>();

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            if (isHead) // head shot
            {
                health_.currentHealth -= health_.currentHealth;
            }
            else
            {
                health_.currentHealth -= 1;
            }
    
            Debug.Log("Zombie vuruldu");
        }
        Debug.Log("Zombie collison");
    }

    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("bullet"))
    //    {
    //        if (isHead) // head shot
    //        {
    //            health_.health -= health_.health;
    //        }
    //        else
    //        {
    //            health_.health -= 1;
    //        }
    //
    //        Debug.Log("Zombie vuruldu");
    //    }
    //    Debug.Log("Zombie collison");
    //}


    //public void decreaseHealth()
    //{
    //    if (isHead) // head shot
    //    {
    //        health_.health -= health_.health;
    //    }
    //    else
    //    {
    //        health_.health -= 1;
    //    }
    //
    //    Debug.Log("Zombie vuruldu");
    //}
}

