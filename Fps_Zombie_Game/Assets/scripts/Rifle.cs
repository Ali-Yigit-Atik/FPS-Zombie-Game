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

    private void OnEnable()
    {
        bulletCountText.enabled = true;
        if(animator_ != null) 
        {
            animator_.SetInteger("backUpBullet", backUpBullet);
            animator_.SetInteger("bulletCount", bulletCount);
        }
        

    }
    private void OnDisable()
    {
        bulletCountText.enabled = false;
    }

    private void Start()
    {
        maxBulletCount = bulletCount;
        animator_ = GetComponent<Animator>();
        shoot +=fire;

        animator_.SetInteger("backUpBullet", backUpBullet);
        animator_.SetInteger("bulletCount", bulletCount);

    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)  && !isStillShooting && bulletCount >=1)  
        {
            shoot.Invoke();
        }
        RunAnimation();

        if (Input.GetMouseButtonUp(0)) 
        {
            animator_.SetBool("fire", false);
        }

        bulletCountText.text = bulletCount.ToString()+" / "+backUpBullet.ToString();
        Debug.Log("isStillShooting: " + isStillShooting);

        IsShootingAvoidBug();
    }

    private void fire()
    {
        
        //animator_.SetTrigger("isFired");
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
