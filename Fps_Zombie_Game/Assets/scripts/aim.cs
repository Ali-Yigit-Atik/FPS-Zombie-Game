using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    [SerializeField] private GameObject aimObject;
    private void Start()
    {
        aimObject.SetActive(false);
        sniper.whenScopeOpen += aimOpened;
        sniper.whenScopeClosed += aimClose;
    }

    
    
    
    private void aimOpened()
    {
        StartCoroutine(aimOpen());
    
    }

    IEnumerator aimOpen()
    {
        yield return new WaitForSeconds(0.35f);
        aimObject.SetActive(true);
    }

    private void aimClose()
    {
        //sniper.aimIsOpened = false;
        aimObject.SetActive(false);
    }
}
