using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeAimPosition : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 aimPosition = new Vector3();
    [SerializeField] private GameObject bullet_;
    [SerializeField] private GameObject bloodParticle;

    public static Vector3 bulletSpawnPosition = new Vector3();
    [SerializeField] private LayerMask layerMask_;

    void Start()
    {
        mainCam = Camera.main;
        sniper.whenFired += target;
        sniper.whenFired += bulletSpawn;

        Rifle.shoot += target;

    }

    

    private void target()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 1000 ,layerMask_))
        {

            if (hit.collider.CompareTag("enemy"))
            {
                Debug.Log("enemy in target");
                

                BloodParticlePool.InstBloodEffect(hit.point, hit.collider.gameObject.transform);
                
                hit.collider.gameObject.GetComponent<zombieHealth>().getDamage();
                //aimPosition = hit.point;

            }
            else if (hit.collider.CompareTag("platform"))
            {
                Debug.Log("platform!!!");
            }
            else
            {
                Debug.Log("nereye niþan alýyorsun");
                //aimPosition = hit.point;
            }


            Debug.Log("name :" + hit.collider.gameObject.name);
            aimPosition = hit.point;

            

        }
    }


    private void bulletSpawn()
    {
        GameObject bullet__= Instantiate(bullet_, bulletSpawnPosition, Quaternion.identity);

        bullet__.GetComponent<bullet>().target = aimPosition;
        
    }

    
}
