using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieMovementDeneme : MonoBehaviour
{
    // docs.unity3d.com/ScriptReference/Physics.BoxCast.html
    // youtube : Unity3D | Tutorial | Raycast/Boxcast/Spherecast


    private Vector3 detectArea = new Vector3(2, 2, 2) / 2;

    RaycastHit hit;
    bool isHit;

    private float radius = 4f;

    private float zGapCast = -5;
    private Vector3 castCenter;

    private float maxDistance = 8f;
    private Vector3 castDirection = Vector3.forward * -1;

    [SerializeField] private LayerMask platformLayerMask;


    /// ///////////////////////////////////////////////

    public GameObject targetPlayer;


    private void FixedUpdate()
    {
        castCenter.x = transform.position.x;
        castCenter.y = transform.position.y;
        castCenter.z = transform.position.z - zGapCast;


        //isHit = Physics.BoxCast(transform.position, detectArea, Vector3.one, out hit, transform.rotation, 0.0f);
        isHit = Physics.SphereCast(castCenter, radius, castDirection, out hit, maxDistance, platformLayerMask);
        //isHit = Physics.BoxCast(transform.position, detectArea, castDirection, out hit, transform.rotation, maxDistance);
        //Debug.Log(hit);

        if (isHit)
        {
            // do something
            Debug.Log("Hit : " + hit.collider.name);
        }
    }
    public void detect()
    {
        isHit = Physics.BoxCast(transform.position, detectArea, Vector3.zero * 1.5f, out hit, transform.rotation, maxDistance);

        if (isHit)
        {
            // do something
            Debug.Log("Hit : " + hit.collider.name);
            isHit = false;
        }
    }


    //private void OnDrawGizmos() // gizmo çizilebilmesi için lazým
    //{
    //    Gizmos.color = Color.red;
    //
    //    if (isHit)
    //    {
    //        if (hit.collider.CompareTag("enemy"))
    //        {
    //            // do something
    //            
    //        }
    //
    //        Gizmos.DrawWireSphere(castCenter + castDirection * hit.distance, radius);
    //        Gizmos.DrawRay(castCenter, castDirection * hit.distance);
    //        //Gizmos.DrawWireCube(transform.position + transform.forward * 0f, detectArea);
    //
    //        //Gizmos.DrawWireCube(transform.position + castDirection * hit.distance, detectArea*2);
    //    }
    //    else
    //    {
    //        Gizmos.DrawWireSphere(castCenter + castDirection * maxDistance, radius);
    //        Gizmos.DrawRay(castCenter, castDirection * maxDistance);
    //        //Gizmos.DrawWireCube(transform.position + castDirection * maxDistance, detectArea*2);
    //
    //    }
    //
    //
    //
    //
    //}
}
