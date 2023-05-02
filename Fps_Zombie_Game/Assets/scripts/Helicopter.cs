using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Helicopter : MonoBehaviour
{
    private Vector3 targetTakeOffPos = new Vector3();
    private float takeOffSpeed = 0;
    private float YPos;

    private bool shouldMoveForward = false;
    private float moveForwardSpeed = 0;

    private Animator animator_;

    private float takeOffTime = 3f; // wait before take off
    private float timer = 0;

    public static int howManyChildInHelicopter = 0; // if 2 child in helicopter, it needs take of

    void Start()
    {
        targetTakeOffPos = transform.position;
        targetTakeOffPos.y += 40f;

        YPos = transform.position.y;

        animator_ = GetComponent<Animator>();
        animator_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(!shouldMoveForward && howManyChildInHelicopter >=2) TakeOff();
        if(shouldMoveForward) 
        {
            
            if(moveForwardSpeed < 3f) 
            {
                moveForwardSpeed += 0.0001f; // speed get faster
            }

            transform.Translate(Vector3.forward  * moveForwardSpeed);

            
        }

    }

    private void TakeOff() 
    {

        //animator_.SetBool("takeOff", true);
        animator_.enabled = true;

        timer += Time.deltaTime;

        if(takeOffTime < timer) 
        {
            if (takeOffSpeed < 0.2f)
            {
                takeOffSpeed += 0.000001f; // speed get faster
            }

            transform.position = Vector3.Lerp(transform.position, targetTakeOffPos, takeOffSpeed);
            

            if (Vector3.Distance(targetTakeOffPos, transform.position) < 0.3f)
            {
                shouldMoveForward = true;
            }
        }

        
    }
}
