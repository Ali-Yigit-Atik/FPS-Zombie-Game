using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodParticle : MonoBehaviour
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

            transform.parent = null;
            gameObject.SetActive(false);

        }
    }

}
