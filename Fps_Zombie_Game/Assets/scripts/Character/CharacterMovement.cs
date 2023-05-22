using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    private Vector3 forward = new Vector3();
    private Vector3 right = new Vector3();

    private Vector3 forwardRelativeVerticalInput = new Vector3();
    private Vector3 rightRelativeVerticalInput = new Vector3();

    private Vector3 cameraRelativeMovement = new Vector3();

    private Rigidbody rigidbody_;
    public float speed = 0.2f;

    private void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }


    
    private void FixedUpdate() // nesnelerin içinden geçmemesi için Fixed kullandým
    {
        if (CharacterHealth.isDead) return;

        if (Crouch.isCrouch)
        {
            speed = 0.1f;
        }
        else speed = 0.2f;
        Movement();
    }

    private void Movement() 
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward=forward.normalized; // forward ve right zaten normalizeydi ama "y" deðerini sýfýr yapýnca tekrar birim vektör yapmak için
        right = right.normalized; // normalize yapamak gerekiyor. 

        forwardRelativeVerticalInput = verticalInput * forward;
        rightRelativeVerticalInput = horizontalInput * right;

        cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        //transform.Translate(cameraRelativeMovement/5, Space.World); //25

        rigidbody_.position += (cameraRelativeMovement * speed);
        //rigidbody_.MovePosition( transform.position+(cameraRelativeMovement / 2));
        

    }
}
