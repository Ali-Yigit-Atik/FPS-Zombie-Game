using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeShot : MonoBehaviour
{
    private Camera mainCam;    
    [SerializeField] private GameObject bullet_;
    [SerializeField] private GameObject bloodParticle;

    
    [SerializeField] private LayerMask layerMask_;

    

    private void OnEnable()
    {
        mainCam = Camera.main;
        Sniper.whenFired += Shoot;        
        Rifle.shoot += Shoot;
    }
    private void OnDisable()
    {
        Sniper.whenFired -= Shoot;        
        Rifle.shoot -= Shoot;
    }


    private void Shoot()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 1000 ,layerMask_))
        {

            if (hit.collider.CompareTag("enemy"))
            {
                //Debug.Log("You hit enemy");                

                BloodParticlePool.InstBloodEffect(hit.point, hit.collider.gameObject.transform);
                
                hit.collider.gameObject.GetComponent<ZombieHealth>().getDamage(hit.collider);
                

            }           


            //Debug.Log("name :" + hit.collider.gameObject.name);
            

            

        }
    }


    

    
}
