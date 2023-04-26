using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunList = new List<GameObject>();
    private int gunIndex = 0;
    void Start()
    {
        gunList[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !sniper.isStillFiring && sniper.aimIsOpened==false)
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
