using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawnPosition : MonoBehaviour
{
    private void OnEnable()
    {
        takeAimPosition.bulletSpawnPosition = transform.position;
    }
}
