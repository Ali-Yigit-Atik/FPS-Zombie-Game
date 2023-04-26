using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDisappearParticlePool : MonoBehaviour
{
    public static List<GameObject> disappearParticles = new List<GameObject>();
    [SerializeField] GameObject disappearParticle;
    private GameObject disappearParticleSample;
    

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            disappearParticleSample = Instantiate(disappearParticle);
            disappearParticle.SetActive(false);
            disappearParticles.Add(disappearParticleSample);
        }
    }



    public static void InstDisappearEffect(Vector3 Pos, Quaternion rotation)
    {
        disappearParticles[0].gameObject.transform.position = Pos;
        disappearParticles[0].gameObject.transform.rotation = Quaternion.Euler(0,0 ,0 );

        disappearParticles[0].gameObject.SetActive(true);
        
        disappearParticles.Remove(disappearParticles[0].gameObject);
    }

    public static void AddDisappearParticleInList(GameObject particle)
    {
        disappearParticles.Add(particle);
    }
}
