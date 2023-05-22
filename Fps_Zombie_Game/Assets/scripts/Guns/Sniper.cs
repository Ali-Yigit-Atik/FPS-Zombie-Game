using System.Collections;
using System.Collections.Generic;
using System; // Action and Func
using DG.Tweening;
using UnityEngine;
using TMPro;

public class Sniper : MonoBehaviour
{
    private Animator animator_;
    public static bool isStillFiring;
    [SerializeField] private TextMeshProUGUI bulletCountText;


    [SerializeField] private int bulletCount = 6;
    private int maxBulletCount;
    [SerializeField] private int backUpBullet = 30;

    public static event Action whenScopeOpen;
    public static event Action whenFired;
    public static event Action whenScopeClosed;


    private Vector3 intialLocalPos;


    public static bool aimIsOpened;

    
    

    private void Start()
    {
        isStillFiring = false;
        aimIsOpened = false;
        animator_ = GetComponent<Animator>();
        animator_.enabled = true;

        Debug.Log("bullet count" + animator_.GetInteger("bulletCount"));
        maxBulletCount = bulletCount;
        animator_.SetInteger("bulletCount", bulletCount);
        animator_.SetInteger("backUpBullet", backUpBullet);

        intialLocalPos = transform.localPosition;        

        Debug.Log("bullet count" + animator_.GetInteger("bulletCount"));
        
    }
    private void OnEnable()
    {
        
        if (animator_ != null) 
        {
            animator_.SetInteger("bulletCount", bulletCount);
            animator_.SetInteger("backUpBullet", backUpBullet);
        }

        bulletCountText.enabled = true;
        whenScopeOpen += lookScope;
        whenFired += fire;
        whenScopeClosed += outlLookScope;

    }

    private void OnDisable()
    {
        
        whenScopeOpen -= lookScope;
        whenFired -= fire;
        whenScopeClosed -= outlLookScope;
        bulletCountText.enabled = false;
    }

    void Update()
    {
        if (CharacterHealth.isDead && !animator_.enabled) return;
        else if (CharacterHealth.isDead && animator_.enabled)
        {
            animator_.enabled = false;
            return;
        }

        if (Input.GetMouseButtonDown(0) && animator_.GetInteger("bulletCount") > 0 && !isStillFiring)
        {
            whenScopeOpen?.Invoke();
        }

        if (Input.GetMouseButtonUp(0) && animator_.GetInteger("bulletCount") > 0 && !isStillFiring)
        {
            whenFired.Invoke();
        }

        bulletCountText.text = animator_.GetInteger("bulletCount").ToString() + " / " + backUpBullet.ToString() ;

        SniperRunAnimation();
    }

    

    private void lookScope() // animation effect
    {
        aimIsOpened = true; //
        transform.DOLocalMove(new Vector3(0f, -0.07f, 0.40f), 0.35f)
            .SetEase(Ease.InBack);
    }

    private void outlLookScope() // animation effect
    {
        aimIsOpened = false;
        transform.DOLocalMove(intialLocalPos, 0.5f).SetEase(Ease.InBack);
        
    }

    private void SniperRunAnimation() 
    {
        if (Input.GetAxis("Vertical") !=0  || Input.GetAxis("Horizontal") !=0) 
        {
            animator_.SetBool("isRunning", true);
        }
        else if((Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) || Input.GetMouseButtonDown(0))
        {
            animator_.SetBool("isRunning", false);
        }
    }

    private void fire()
    {
        animator_.SetTrigger("isFired");
        isStillFiring = true;
    }

    
    private void bulletDecrease() // call in animator
    {
        if(animator_.GetInteger("bulletCount") > 0)
        {
            bulletCount--;
            animator_.SetInteger("bulletCount", bulletCount);
            whenScopeClosed?.Invoke();
        }

        
    }

    private void fireIsEnd() // call in animator
    {
        isStillFiring = false;
        
    }

    private void magazineChange() // call in animator
    {
        


        if (backUpBullet >= maxBulletCount)
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


    
}
