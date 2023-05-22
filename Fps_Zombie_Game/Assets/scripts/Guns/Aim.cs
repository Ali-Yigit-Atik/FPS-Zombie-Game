using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject aimObject;
    
    private void OnEnable()
    {
        aimObject.SetActive(false);
        Sniper.whenScopeOpen += aimOpened;
        Sniper.whenScopeClosed += aimClose;
    }
    private void OnDisable()
    {
        Sniper.whenScopeOpen -= aimOpened;
        Sniper.whenScopeClosed -= aimClose;
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
        
        aimObject.SetActive(false);
    }
}
