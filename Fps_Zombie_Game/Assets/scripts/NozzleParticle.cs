using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NozzleParticle : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        Rifle.shoot += PlayShootingParticle;
    }

    // Update is called once per frame
    

    private void PlayShootingParticle() 
    {
        ps.Play();
    }
}
