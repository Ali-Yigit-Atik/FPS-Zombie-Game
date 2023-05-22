using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDisappearParticlePool : MonoBehaviour
{
    public static List<GameObject> disappearParticles;
    [SerializeField] GameObject disappearParticle;
    private GameObject disappearParticleSample;
    private Transform disappearParticleParent; // parent object for better hierarcy layout


    void Start()
    {
        disappearParticles = new List<GameObject>();
        disappearParticleParent = GameObject.FindGameObjectWithTag("DisappearParticleParent").transform;
        for (int i = 0; i < 10; i++)
        {
            disappearParticleSample = Instantiate(disappearParticle);
            disappearParticleSample.transform.parent = disappearParticleParent;
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
