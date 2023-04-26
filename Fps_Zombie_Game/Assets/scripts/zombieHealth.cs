using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombieHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [HideInInspector] public int currentHealth;
    [SerializeField] private float bodyDisappearTime = 2f; // body disappear time when zombie dead
    private float timer = 0;

    [HideInInspector] public bool zombieIsDead = false;

    private zombieMovement zombieMovement_;
    private ZombiePool zombiePool_;

    private Collider[] collider_ =new Collider[2];
    private Vector3 YOfset = new Vector3(0, 0.20f, 0);

    private void OnEnable()
    {
        
        if (collider_[0]  ==null && collider_[1] == null) 
        {
            //collider_[0] = GetComponent<BoxCollider>();
            //collider_[1] = GetComponent<CapsuleCollider>();

        }
        currentHealth = health;
        zombieIsDead = false;
        Debug.Log("health: " + health + "currentHealth: " + currentHealth);
        //collider_[0].enabled = true;
        //collider_[1].enabled = true;

        
    }
    private void OnDisable()
    {
        //zombiePool_.ReSpawn(gameObject, 3f, 5f);
        //ZombiePool.zombies.Add(gameObject);
    }

    private void Start()
    {
        //currentHealth = health;
        zombieMovement_ = GetComponent<zombieMovement>();
        zombiePool_ = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ZombiePool>();
        
    }
    
    private void Update()
    {
        
        
        if (currentHealth <= 0)
        {
            zombieIsDead = true;
            WhenDie();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("bullet"))
    //    {
    //
    //        //if(collision.collider.GetType() == typeof(CapsuleCollider))
    //        //{
    //        //    health -= health;
    //        //    Debug.Log("ddddddd");
    //        //}
    //        //else
    //        //{
    //        //    health -= 1;
    //        //}
    //
    //        health -= 1;
    //
    //    }
    //}

    public void getDamage() 
    {
        currentHealth -= 1;
        zombieMovement_.whileZombieGetShotFarAwayFollowDistance(); 

    } 

    private void WhenDie() 
    {
        //collider_[0].enabled = false;
        //collider_[1].enabled = false;

        timer += Time.deltaTime;

        //if (timer > (bodyDisappearTime-0.2f) && ZombiePool.zombies.Contains(gameObject) ==false)
        //{
        //    ZombieDisappearParticlePool.InstDisappearEffect(gameObject.transform.position + YOfset, transform.rotation);
        //    ZombiePool.zombies.Add(gameObject);
        //}

        if (timer > bodyDisappearTime) 
        {
            ZombieDisappearParticlePool.InstDisappearEffect(gameObject.transform.position +YOfset, transform.rotation);
            timer = 0;
            ZombiePool.zombies.Add(gameObject);
            gameObject.SetActive(false);
        }

    }
}
