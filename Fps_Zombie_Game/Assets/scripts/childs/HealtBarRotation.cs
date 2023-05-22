using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBarRotation : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerRotation = new Vector3();
    private int direction = 0;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRotation.y =0;

        if (transform.rotation.y == transform.localRotation.y)
        {
            direction = 1;
        }
        else direction = -1;
    }

    
    private void Update()
    {
        

        playerRotation.x = player.transform.position.x - transform.position.x;        
        playerRotation.z = player.transform.position.z - transform.position.z;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction * playerRotation), 7f * Time.deltaTime);
        
    }
}
