using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomahawk : MonoBehaviour
{
    private Animator animator_ ;
    private float timer = 0;
    private float maxTimeGapBetweenAttack=2f;
    private int countAttackClick = 0;
    private GameObject enemy;
    [SerializeField] GameObject bloodEffect;
    private Vector3 bloodEffectPositionYOfset = new Vector3(0, 0.5f, 0);

    void Start()
    {
        animator_ = GetComponent<Animator>();
    }

    private void Update()
    {
        Animations();


        //if (Input.GetMouseButtonDown(0))
        //{
            //animator_.SetInteger("attackPhase", animator_.GetInteger("attackPhase") + 1);
            //timer += Time.deltaTime;
            //if (timer > maxTimeGapBetweenAttack)
            //{
            //    animator_.SetInteger("attackPhase", 0);
            //    timer = 0;
            //}
            //
            //if(animator_.GetInteger("attackPhase") == 0) 
            //{
            //    animator_.SetInteger("attackPhase", 1);
            //}
            //else if(animator_.GetInteger("attackPhase") == 1) 
            //{
            //    animator_.SetInteger("attackPhase", 2);
            //}
            //else if(animator_.GetInteger("attackPhase") >= 2) 
            //{
            //    animator_.SetInteger("attackPhase", 1);
            //}
            //
            //
            //if (!animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK1(no hit)")) 
            //    {
            //        animator_.SetInteger("attackPhase", animator_.GetInteger("attackPhase") + 1);
            //    }
            //    else if(animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK1(no hit)")) 
            //    {
            //        animator_.SetInteger("attackPhase", animator_.GetInteger("attackPhase") + 1);
            //        animator_.SetInteger("attackPhase", 0);
            //    }
            //

            //if (animator_.GetInteger("attackPhase") == 1)
            //{
            //    //timer = 0;
            //    timer += Time.deltaTime;
            //
            //    if (timer > maxTimeGapBetweenAttack)
            //    {
            //        animator_.SetInteger("attackPhase", 0);
            //        timer = 0;
            //    }
            //}
            //else if (animator_.GetInteger("attackPhase") >= 2) //for avoid bug
            //{
            //    animator_.SetInteger("attackPhase", 0);
            //}
            //animator_.SetInteger("attackPhase", 0);
            //DoubleAttack();
        //}
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
            //if ((animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK1(no hit)") || animator_.GetCurrentAnimatorStateInfo(0).IsName("Tomahawk_ATK2(no hit)")))
            //{
            //    animator_.SetInteger("attackPhase", 0);
            //    Debug.Log("çalýþýyor");
            //}

            Debug.Log(countAttackClick);

        }

        RunAnimation();
    }

    private void ResetAttackPhase() 
    {
        //countAttackClick = 0;
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
            if (enemy.GetComponent<zombieHealth>()) // avoid bug check
            {
                enemy.GetComponent<zombieHealth>().getDamage();

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
