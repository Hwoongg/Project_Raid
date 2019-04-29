using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    int currentHealth;
    //[SerializeField] Image damageImage;
    //[SerializeField] float flashSpeed = 0.5f;
    //[SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    bool isDead;
    protected bool damaged;


    private void Awake()
    {
        currentHealth = startingHealth;
    }

    
    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
