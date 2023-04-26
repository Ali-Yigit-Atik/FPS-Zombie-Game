using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour
{

    
    private bool openDoor =false;
    public static bool isCageOpen = false;

    [SerializeField]private GameObject otherDoor;

    

    void Update()
    {
        if(openDoor ==true) 
        {
            transform.Rotate(0,  0, -0.1f);
            otherDoor.transform.Rotate(0, 0, 0.1f);
            Debug.Log(transform.localRotation.z);
        }
        if(Mathf.Abs(transform.localRotation.z) > 0.5f) 
        {
            openDoor = false;
            isCageOpen = true;
            gameObject.GetComponent<CageOpen>().enabled = false; // dont need to run anymore
            Debug.Log("openDoor = false");
        }

    }

    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            openDoor = true;
            Debug.Log("collison çalýþýyor");
        }
    }
}
