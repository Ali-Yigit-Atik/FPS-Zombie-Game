using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Rifle : MonoBehaviour
{
    private Animator animator_;
    public static Action shoot;
    private bool isStillShooting = false;

    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private int bulletCount = 30; // current bullet count
    private int maxBulletCount;
    [SerializeField] private int backUpBullet =120;


    private void Start()
    {
        maxBulletCount = bulletCount;
        animator_ = GetComponent<Animator>();
        animator_.enabled = true;
        

        animator_.SetInteger("backUpBullet", backUpBullet);
        animator_.SetInteger("bulletCount", bulletCount);

    }

    private void OnEnable()
    {
        
        bulletCountText.enabled = true;        
        shoot += fire;
        if (animator_ != null) 
        {
            
            animator_.SetInteger("backUpBullet", backUpBullet);
            animator_.SetInteger("bulletCount", bulletCount);
        }
        

    }
    private void OnDisable()
    {
        shoot -= fire;
        bulletCountText.enabled = false;
        
        
    }

    

    
    private void Update()
    {
        if (CharacterHealth.isDead && !animator_.enabled) return;
        else if(CharacterHealth.isDead && animator_.enabled)
        {
            animator_.enabled = false;
            return;
        }

        if (Input.GetMouseButton(0)  && !isStillShooting && bulletCount >=1)  
        {
            shoot.Invoke();
        }

        RunAnimation();

        if (Input.GetMouseButtonUp(0)) 
        {
            animator_.SetBool("fire", false);
        }

        if(bulletCountText.enabled) bulletCountText.text = bulletCount.ToString()+" / "+backUpBullet.ToString();

        //Debug.Log("isStillShooting: " + isStillShooting);

        IsShootingAvoidBug();
    }

    private void fire()
    {
        
        
        animator_.SetBool("fire", true);
        isStillShooting = true;
        bulletCount--;
        animator_.SetInteger("bulletCount", bulletCount);

        
    }
    private void RunAnimation()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator_.SetBool("isRunning", true);
        }
        else if ((Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) || Input.GetMouseButton(0))
        {
            animator_.SetBool("isRunning", false);
        }
    }
    private void MagazineChange() // call in animator
    {
        if(backUpBullet >= maxBulletCount) 
        {
            animator_.SetInteger("bulletCount", maxBulletCount);
            
            backUpBullet -= maxBulletCount;
            bulletCount = maxBulletCount;
            animator_.SetInteger("backUpBullet", backUpBullet);
        }
        else 
        {
            animator_.SetInteger("bulletCount", backUpBullet);
            bulletCount = backUpBullet;
            backUpBullet -= backUpBullet;            
            animator_.SetInteger("backUpBullet", backUpBullet);
        }
        
    }

    private void IsShooting()  // call in animator
    {
        isStillShooting = false;
    }

    private void IsShootingAvoidBug()  // the firing animation runs so fast, although rare it may skip making isStillshooting = true. 
    {                                  // This function was written to prevent this.
        if (!Input.GetMouseButton(0)) 
        {
            isStillShooting = false;
            
        }
    }
}
