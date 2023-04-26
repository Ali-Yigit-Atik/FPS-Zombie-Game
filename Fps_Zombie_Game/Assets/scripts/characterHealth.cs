using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class characterHealth : MonoBehaviour
{
    [SerializeField] private float health =100f;
    [SerializeField] private Image bloodEffect;
    private Color bloodEffectColor; // alpha value deðiþecek

    [SerializeField] private float durationTime;
    private float durationTimer =0;

    private float healthTimer = 0;

    //[SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        
        bloodEffectColor = bloodEffect.color;
        bloodEffectColor.a = 0f;
        bloodEffect.color = bloodEffectColor;
        
    }

    private void Update()
    {
        //health = Mathf.Clamp(health, 0, 100);
        
        //healthText.text = "Health: " + health.ToString();


        BloodEffect();
        //HealthIncrease();

    }

    public void getDamage(int damage ,int maxCriticalDamage) 
    {
        health -= (damage + Random.Range(1, maxCriticalDamage));      
        
        bloodEffectColor.a += 0.2f;
        bloodEffectColor.a = Mathf.Clamp(bloodEffectColor.a, 0, 0.9f); // alpha=1 çok karanlýk oluyordu
        bloodEffect.color = bloodEffectColor;
        durationTimer = 0;
    }

    private void BloodEffect() 
    {
        if (bloodEffectColor.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > durationTime) 
            {
                bloodEffectColor.a -= (Time.deltaTime/5);
                bloodEffect.color = bloodEffectColor;

            }
        }
    }

    private void HealthIncrease()  // her 1 saniyede can 1 artsýn
    {
        if (health < 100) 
        {

            healthTimer += Time.deltaTime;
            if(healthTimer >1) 
            {
                health += 1f;
                healthTimer = 0;
            }
        }
    }
}
