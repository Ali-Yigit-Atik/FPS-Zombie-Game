using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NozzleParticle : MonoBehaviour
{
    private ParticleSystem ps;

    private void OnEnable()
    {
        Rifle.shoot += PlayShootingParticle;
    }
    private void OnDisable()
    {
        Rifle.shoot -= PlayShootingParticle;
    }

    private void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        
    }

   
    

    private void PlayShootingParticle() 
    {
        ps.Play();
    }
}
