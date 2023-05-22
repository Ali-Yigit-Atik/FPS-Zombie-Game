using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChildHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int maxHealth;
    
    [SerializeField] private GameObject bloodParticle;
    [HideInInspector] public bool dontMoveWhenTakeHit =false;
    [HideInInspector] public bool isChildDead = false;
    private ParticleSystem ps;

    private Animator animator_;
    [SerializeField] private Image healthBar;

    private void OnEnable()
    {
        Helicopter.howManyChildrenIsAlive++;
    }
    

    private void Start()
    {
        ps = bloodParticle.gameObject.GetComponent<ParticleSystem>();
        animator_ = GetComponent<Animator>();
        maxHealth = health;
        
    }

    
    private void Update()
    {
        
        health = Mathf.Clamp(health, 0, maxHealth);
        


        if (health <= 0 && !animator_.GetBool("isDead"))
        {
            animator_.SetBool("isDead", true);
            isChildDead = true;
            Helicopter.howManyChildrenIsAlive--;
        }
        
    }

    public void GetDamage(int damage, int maxCriticalDamage)
    {
        health -= (damage + Random.Range(1, maxCriticalDamage));
        animator_.SetTrigger("hit");
        dontMoveWhenTakeHit = true;
        healthBar.fillAmount = (float)health / maxHealth;
        ps.Play();
    }

    private void EndOFTakingDamage() // call in animator
    {
        dontMoveWhenTakeHit = false;
    }
}
