using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{

    private float timer = 0;
    [SerializeField] private float lifeTime = 1;

    private void OnEnable()
    {
        timer = 0;
    }

    private void OnDisable()
    {
        BloodParticlePool.AddBloodParticleInList(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifeTime) 
        {

            gameObject.SetActive(false);

        }
    }

}
