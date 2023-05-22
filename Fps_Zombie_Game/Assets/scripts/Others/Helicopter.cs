using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Helicopter : MonoBehaviour
{
    private Vector3 targetTakeOffPos = new Vector3();
    private float takeOffSpeed = 0;
    

    private bool shouldMoveForward = false;
    private float moveForwardSpeed = 0;

    private Animator animator_;

    private float takeOffTime = 3f; // wait before take off
    private float timer = 0;

    public static int howManyChildInHelicopter; // if All Alive childs in helicopter, it needs take of
    public static int howManyChildrenIsAlive;

    private void Awake()
    {
        howManyChildrenIsAlive = 0;
        howManyChildInHelicopter = 0;
    }

    private void Start()
    {
        
        targetTakeOffPos = transform.position;
        targetTakeOffPos.y += 40f;

        

        animator_ = GetComponent<Animator>();
        animator_.enabled = false;
    }

    
    private void Update()
    {
        //Debug.Log("howManyChildInHelicopter: " + howManyChildInHelicopter);
        //Debug.Log("howManyChildrenIsAlive: " + howManyChildrenIsAlive);

        if(!shouldMoveForward && howManyChildInHelicopter >= howManyChildrenIsAlive) TakeOff();
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
