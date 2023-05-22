using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private float rotataionX = 0;
    private float rotataionY = 0;
    public float sensitivity = 2;

    public static Vector3 camIntialPos;
    private float camIntialPosY;
    private bool isCameraStartingFall = false;

    private float oldFieldOfView;

    private void OnEnable()
    {
        Sniper.whenScopeOpen += SniperScopeOpened;
        Sniper.whenScopeClosed += SniperScopeClosed;
    }
    private void OnDisable()
    {
        Sniper.whenScopeOpen -= SniperScopeOpened;
        Sniper.whenScopeClosed -= SniperScopeClosed;
    }

    private void Start()
    {
        camIntialPos = transform.position;
        camIntialPosY = transform.localPosition.y;
        oldFieldOfView = Camera.main.fieldOfView;


    }

    void Update()
    {

        FallDownWhenCharacterIsDead();

        if (CharacterHealth.isDead) return;
        CameraMouseMoving();
       

    }

    private void CameraMouseMoving()
    {
        rotataionX += Input.GetAxis("Mouse X") * sensitivity;
        rotataionY -= Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(rotataionY, rotataionX, 0);
    }

    

    private void SniperScopeOpened()
    {
        StartCoroutine(SniperScopeOpen());
    }

    IEnumerator SniperScopeOpen()
    {
        yield return new WaitForSeconds(0.35f);
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Guns")); // don't render guns
        Camera.main.fieldOfView = 10;
    }
    
    private void SniperScopeClosed()
    {
        StartCoroutine(SniperScopeClose());
    }
    
    IEnumerator SniperScopeClose()
    {
        yield return new WaitForSeconds(0.2f);
        Camera.main.cullingMask = -1; // render everything
        Camera.main.fieldOfView = oldFieldOfView;
    }


    private void FallDownWhenCharacterIsDead() 
    {
        if (CharacterHealth.isDead && !isCameraStartingFall) 
        {
            isCameraStartingFall = true;
            transform.DOLocalMoveY(camIntialPosY / 5, 1.5f).SetEase(Ease.InBack);
        }
        
    }

}
