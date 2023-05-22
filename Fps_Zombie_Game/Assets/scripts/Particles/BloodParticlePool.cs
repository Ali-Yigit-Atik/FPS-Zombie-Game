using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticlePool : MonoBehaviour
{
    public static List<GameObject> bloodParticles;
    [SerializeField] GameObject bloodParticle;
    private GameObject bloodParticleSample;
    private Transform bloodParticleParent; // parent object for better hierarcy layout
    void Start()
    {
        bloodParticles = new List<GameObject>();
        bloodParticleParent = GameObject.FindGameObjectWithTag("BloodParticleParent").transform;
        for (int i=0; i<10; i++) 
        {
            bloodParticleSample = Instantiate(bloodParticle);
            bloodParticleSample.transform.parent = bloodParticleParent;
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
