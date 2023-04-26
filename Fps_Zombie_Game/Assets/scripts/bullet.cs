using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector3 target { get; set; }
    [SerializeField] private float bulletSpeed =5f;


    void Start()
    {
        //target = target - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("çalýþýyor");
        transform.position = Vector3.Lerp(transform.position,target, Time.deltaTime * bulletSpeed);

        //transform.Translate(target* Time.deltaTime * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            //Debug.Log("You hit enemy");
            
        }
        else
        {
            //Debug.Log("You miss. Haha");
        }
        //Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("enemy"))
    //    {
    //        Debug.Log("You hit enemy");
    //    }
    //    else
    //    {
    //        Debug.Log("You miss. Haha");
    //    }
    //    //Destroy(collision.gameObject);
    //    Destroy(gameObject);
    //}
}
