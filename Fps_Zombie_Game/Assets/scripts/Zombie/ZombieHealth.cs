using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private int currentHealth;
    [SerializeField] private float bodyDisappearTime = 2f; // body disappear time when zombie dead
    private float timer = 0;

    [HideInInspector] public bool zombieIsDead = false;

    private ZombieMovement zombieMovement_;    

    private Collider[] collider_ =new Collider[2]; // head and body colliders
    private Vector3 YOfset = new Vector3(0, 0.20f, 0);

    private void OnEnable()
    {
        
        if (collider_[0]  ==null && collider_[1] == null) 
        {
            collider_[0] = GetComponent<BoxCollider>();
            collider_[1] = GetComponent<CapsuleCollider>();

        }
        currentHealth = health;
        zombieIsDead = false;
        Debug.Log("health: " + health + "currentHealth: " + currentHealth);
        collider_[0].enabled = true;
        collider_[1].enabled = true;

        
    }
    

    private void Start()
    {
        
        zombieMovement_ = GetComponent<ZombieMovement>();       
        
    }
    
    private void Update()
    {
        
        
        if (currentHealth <= 0)
        {
            zombieIsDead = true;
            WhenDie();
        }
    }

   

    public void getDamage() // overload for tomahawk
    {
        currentHealth -= 1;
        zombieMovement_.whileZombieGetShotFarAwayFollowDistance(); 
    
    }

    public void getDamage(Collider collider_) // overload for riffle and sniper
    {
        
        if(collider_.GetType() == typeof(BoxCollider)) // body shot
        {
            currentHealth -= 1;
        }
        else currentHealth -= currentHealth; // head shot 

        zombieMovement_.whileZombieGetShotFarAwayFollowDistance();

    }

    private void WhenDie() 
    {
        collider_[0].enabled = false;
        collider_[1].enabled = false;

        timer += Time.deltaTime;

        

        if (timer > bodyDisappearTime) 
        {
            ZombieDisappearParticlePool.InstDisappearEffect(gameObject.transform.position +YOfset, transform.rotation);
            timer = 0;
            ZombiePool.deadZombies.Add(gameObject);
            gameObject.SetActive(false);
        }

    }
}
