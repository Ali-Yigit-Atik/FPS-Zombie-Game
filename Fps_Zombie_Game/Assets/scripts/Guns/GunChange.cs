using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunList = new List<GameObject>();
    private int gunIndex=0;
    private void Start()
    {
        
        gunList[gunIndex].SetActive(true);
    }

    
    private void Update()
    {

        
        
        if (CharacterHealth.isDead) return;
        if (Input.GetKeyDown(KeyCode.Q) && !Sniper.isStillFiring && Sniper.aimIsOpened==false)
        {
            ChangeGun();

        }
    }

    private  void ChangeGun() 
    {
        

        gunList[gunIndex].SetActive(false);

        if ((gunIndex) == gunList.Count -1)
        {
            gunIndex = 0;
            gunList[gunIndex].SetActive(true);
            Debug.Log("1. gun index: " + gunIndex);
        }
        else 
        {
            gunIndex++;
            gunList[gunIndex].SetActive(true);
            Debug.Log("2. gun index: " + gunIndex);
        }
        

        

        
    }
}
