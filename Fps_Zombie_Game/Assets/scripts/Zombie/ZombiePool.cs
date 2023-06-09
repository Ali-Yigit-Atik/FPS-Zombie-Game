using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePool : MonoBehaviour
{
    public static List<GameObject> deadZombies;

    [SerializeField] GameObject zombie;
    [SerializeField] private int zombieSpawnSize;
    private GameObject zombieSample;
    [SerializeField] private Transform navmeshSpawnCenter;
    private Transform zombieHolder; // parent object for better hierarcy layout

    NavMeshHit navHit;
    private Vector3 spawnPosition = new Vector3();
    private float spawnTime;
    private float timer = 0;

    private void Start()
    {
        deadZombies = new List<GameObject>();
        zombieHolder = GameObject.FindGameObjectWithTag("zombieHolder").transform;

        for(int i =0; i<zombieSpawnSize; i++) 
        {
            zombieSample = Instantiate(zombie);
            
            zombieSample.transform.position = RandomNavPosition(navmeshSpawnCenter.position, 70f, NavMesh.GetAreaFromName("zombieCantWalk")); //

            zombieSample.gameObject.SetActive(true);
            zombieSample.gameObject.transform.parent = zombieHolder;
        }
        
    }

    private void Update()
    {
        if (deadZombies.Count > 0) 
        {
            Debug.Log("zombies.Count: " + deadZombies.Count);
            ReSpawn( 20f, 25f);
        }
    }


    private Vector3 RandomNavPosition(Vector3 origin, float radius, int layermask)
    {
        spawnPosition = Random.insideUnitSphere * radius;

        spawnPosition += origin;

        if (NavMesh.SamplePosition(spawnPosition, out navHit, radius, layermask))
        {
            return navHit.position;
        }
        else
        {
            return origin + Random.insideUnitSphere*20;
        }


    }

    public void ReSpawn_(GameObject zombie, float minSpwanTime, float maxSpawnTime)  
    {
        spawnTime = Random.Range(minSpwanTime, maxSpawnTime + 1f);
        
        
        timer += Time.deltaTime;
        if (timer > spawnTime) 
        {
            zombie.transform.position = RandomNavPosition(navmeshSpawnCenter.position, 100f, NavMesh.GetAreaFromName("Walkable"));//
            zombie.SetActive(true);
        }
    }

    public void ReSpawn( float minSpwanTime, float maxSpawnTime )
    {
        
       // if(spawnTime ==0) spawnTime = Random.Range(minSpwanTime, maxSpawnTime + 1f);
        

        //Debug.Log("spawnTime: " + spawnTime);
        //Debug.Log("timer: " + timer);

        timer += Time.fixedDeltaTime;
        if (timer > maxSpawnTime)
        {
            
            deadZombies[0].transform.position = RandomNavPosition(navmeshSpawnCenter.position, 250f, -1);
            deadZombies[0].SetActive(true);
            deadZombies.Remove(deadZombies[0]);
            spawnTime = 0;
            timer = 0;
        }
    }
}
