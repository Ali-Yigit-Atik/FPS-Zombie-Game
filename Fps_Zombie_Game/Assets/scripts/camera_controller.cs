using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class camera_controller : MonoBehaviour
{
    private float rotataionX = 0;
    private float rotataionY = 0;
    public float sensitivity = 2;

    public static Vector3 camIntialPos;

    private float oldFieldOfView;

    private void Start()
    {
        camIntialPos = transform.position;
        oldFieldOfView = Camera.main.fieldOfView;

        //sniper.whenScopeOpen += cameraCloserToAim;
        //sniper.whenScopeClosed += camerafarToAim;

        sniper.whenScopeOpen += sniperScopeOpened;
        sniper.whenScopeClosed += sniperScopeClosed;

    }

    void Update()
    {
        cameraMouseMoving();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    cameraClosetrToAim();
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    camerafarToAim();
        //}

    }

    private void cameraMouseMoving()
    {
        rotataionX += Input.GetAxis("Mouse X") * sensitivity;
        rotataionY -= Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(rotataionY, rotataionX, 0);
    }

    //public void cameraCloserToAim()
    //{
    //
    //    camIntialPos = transform.position;
    //    transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z +5), 0.5f)
    //                    .SetEase(Ease.InBack); //.OnComplete();
    //}
    //
    //private void camerafarToAim()
    //{
    //
    //    float oldPositionGap = Mathf.Abs(Mathf.Abs(transform.position.z) - Mathf.Abs(camIntialPos.z));
    //    transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y , transform.localPosition.z - oldPositionGap), 0.5f)
    //                    .SetEase(Ease.InBack); //.OnComplete();
    //
    //    
    //}


    private void sniperScopeOpened()
    {
        StartCoroutine(sniperScopeOpen());
    }

    IEnumerator sniperScopeOpen()
    {
        yield return new WaitForSeconds(0.35f);
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Guns")); // don't render guns
        Camera.main.fieldOfView = 10;
    }
    
    private void sniperScopeClosed()
    {
        StartCoroutine(sniperScopeClose());
    }
    
    IEnumerator sniperScopeClose()
    {
        yield return new WaitForSeconds(0.2f);
        Camera.main.cullingMask = -1; // render everything
        Camera.main.fieldOfView = oldFieldOfView;
    }


}
