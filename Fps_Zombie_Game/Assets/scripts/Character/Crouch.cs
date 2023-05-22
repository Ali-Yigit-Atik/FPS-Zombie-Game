using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crouch : MonoBehaviour
{
    private BoxCollider collider_;
    private Vector3 colliderSizeInitial =new Vector3();
    private Vector3 colliderCrouchSize = new Vector3();

    [SerializeField] private GameObject camera_;
    private float cameraIntialYPos;
    public static bool isCrouch; // it is public static cause speed is gonna decrease when character is crouch in characterMovement script  

    void Start()
    {
        isCrouch = false;
        collider_ = gameObject.GetComponent<BoxCollider>();
        colliderSizeInitial = collider_.size;
        colliderCrouchSize =collider_.size;
        colliderCrouchSize.y = colliderSizeInitial.y / 2f;

        cameraIntialYPos = camera_.transform.localPosition.y;
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !CharacterHealth.isDead) 
        {
            ColliderSizeDecrease();
            ColliderSizeIncrease();

            CameraMoveDown();
            CameraMoveUp();

            if (isCrouch) isCrouch = false;
            else isCrouch = true;
        }
        

    }

    private void ColliderSizeDecrease() 
    {
        if (isCrouch == false) 
        {
            
            collider_.size = colliderCrouchSize;            
            Debug.Log("ColliderSizeDecrease()");

        }
        
    }

    private void ColliderSizeIncrease() 
    {
        if ( isCrouch)
        {
            
            collider_.size = colliderSizeInitial;            
            Debug.Log("ColliderSizeIncrease()");

        }
         
    }


    private void CameraMoveDown() 
    {
        if ( !isCrouch ) 
        {
            camera_.transform.DOLocalMoveY(cameraIntialYPos / 2, 0.4f).SetEase(Ease.InBack);
        }
        
    }

    private void CameraMoveUp() 
    {
        if (  isCrouch ) 
        {
            camera_.transform.DOLocalMoveY(cameraIntialYPos, 0.4f).SetEase(Ease.InBack);
        }
    }
}
