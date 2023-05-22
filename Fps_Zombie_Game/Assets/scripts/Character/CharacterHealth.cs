using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float health =100f;
    [SerializeField] private Image bloodEffect;
    [SerializeField] private GameObject youDiedText;
    private Color bloodEffectColor; // alpha value deðiþecek

    [SerializeField] private float durationTime;
    private float durationTimer =0;
    
    public static bool isDead;

    

    private void Start()
    {
        
        health = 100f;
        isDead = false;
        bloodEffectColor = bloodEffect.color;
        bloodEffectColor.a = 0f;
        bloodEffect.color = bloodEffectColor;
        youDiedText.gameObject.SetActive(false);
        

    }

    private void Update()
    {
        

        BloodEffect();
        CharacterIsDead();
        
        //Debug.Log("is Character Dead: " + isDead);

    }

    public void getDamage(int damage ,int maxCriticalDamage) 
    {
        health -= (damage + Random.Range(1, maxCriticalDamage));

        //bloodEffectColor.a += 0.2f;
        bloodEffectColor.a = 1f- health / 100;
        bloodEffectColor.a = Mathf.Clamp(bloodEffectColor.a, 0, 0.98f); // alpha=1 çok karanlýk oluyordu
        bloodEffect.color = bloodEffectColor;
        durationTimer = 0;
        
    }

    private void BloodEffect() 
    {
        if (bloodEffectColor.a > 0 && !isDead)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > durationTime) 
            {
                bloodEffectColor.a -= (Time.deltaTime/5);
                health = 100- bloodEffectColor.a*100f; // health is increased
                bloodEffect.color = bloodEffectColor;

            }
        }
    }

    private void CharacterIsDead() 
    {
        if(health <= -20) // kan efekti tamamen açýldýktan sonra ki alýnan 2. darbede ölsün diye "-20" þartýný koydum
        {
            isDead = true;
            youDiedText.gameObject.SetActive(true);
                        

        }
    }

    
}
