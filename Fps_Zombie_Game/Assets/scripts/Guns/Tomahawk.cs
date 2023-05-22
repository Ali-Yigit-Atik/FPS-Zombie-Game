using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomahawk : MonoBehaviour
{
    private Animator animator_ ;    
    private int countAttackClick = 0;
    private GameObject enemy;
    [SerializeField] GameObject bloodEffect;
    private Vector3 bloodEffectPositionYOfset = new Vector3(0, 0.5f, 0);

    void Start()
    {
        animator_ = GetComponent<Animator>();
        animator_.enabled = true;
    }

    private void Update()
    {
        if (CharacterHealth.isDead && !animator_.enabled) return;
        else if (CharacterHealth.isDead && animator_.enabled)
        {
            animator_.enabled = false;
            return;
        }

        Animations();


        
    }

    private void Animations() 
    {
        if (Input.GetMouseButtonDown(0) ) 
        {
            if(countAttackClick >= 2) 
            {
                countAttackClick = 0;
            }
            
            if(!(animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK1(no hit)") || animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK2(no hit)"))) 
            {
                countAttackClick++;
            }
            
            animator_.SetInteger("attackPhase", countAttackClick);
            

            Debug.Log(countAttackClick);

        }

        RunAnimation();
    }

    private void ResetAttackPhase() 
    {
        
        animator_.SetInteger("attackPhase", 0);
    }
    private void RunAnimation()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator_.SetBool("isRunning", true);
        }
        else if ((Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) || Input.GetMouseButtonDown(0))
        {
            animator_.SetBool("isRunning", false);
        }
    }

    private void Attack()  // animator
    {
        animator_.SetInteger("attackPhase", 0);
        if(enemy != null) 
        {
            if (enemy.GetComponent<ZombieHealth>()) // avoid bug check
            {
                enemy.GetComponent<ZombieHealth>().getDamage();

                BloodParticlePool.InstBloodEffect(transform.position + transform.forward + bloodEffectPositionYOfset, enemy.transform);
                
            }
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy")) 
        {
            enemy = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            enemy = null;
        }
    }


}
