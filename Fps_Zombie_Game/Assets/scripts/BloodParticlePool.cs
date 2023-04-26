using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticlePool : MonoBehaviour
{
    public static List<GameObject> bloodParticles = new List<GameObject>();
    [SerializeField] GameObject bloodParticle;
    private GameObject bloodParticleSample;
    void Start()
    {
        for(int i=0; i<10; i++) 
        {
            bloodParticleSample = Instantiate(bloodParticle);
            bloodParticle.SetActive(false);
            bloodParticles.Add(bloodParticleSample);
        }
    }

    

    public static void InstBloodEffect(Vector3 bloodPos, Transform parent) 
    {
        bloodParticles[0].gameObject.transform.position = bloodPos;
        bloodParticles[0].gameObject.SetActive(true);
        bloodParticles[0].gameObject.transform.parent = parent;
        bloodParticles.Remove(bloodParticles[0].gameObject);
    }

    public static void AddBloodParticleInList(GameObject bloodParticle) 
    {
        bloodParticles.Add(bloodParticle);
    }
}
