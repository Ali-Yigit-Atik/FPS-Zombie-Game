using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDisappearParticle : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float lifeTime = 1.7f;

    private void OnEnable()
    {
        timer = 0;
    }

    private void OnDisable()
    {
        ZombieDisappearParticlePool.AddDisappearParticleInList(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {

            
            gameObject.SetActive(false);

        }
    }
}
