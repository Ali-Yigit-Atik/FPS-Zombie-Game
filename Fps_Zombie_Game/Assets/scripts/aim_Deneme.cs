using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_Deneme : MonoBehaviour
{
    private Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
        sniper.whenFired+=target;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    target();
    //}

    private void target()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag("enemy"))
            {
                Debug.Log("enemy in target");
                
            }
            else
            {
                Debug.Log("nereye niþan alýyorsun");
            }

           

        }
    } 
}
