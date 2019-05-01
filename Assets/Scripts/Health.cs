using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    public int GetStartingHealth => startingHealth;
    [SerializeField] GameLogicHealthEvent HealthEvent;
    [SerializeField] bool IsPlayer = false;
    [SerializeField] GameObject objExplotionEfx;

    int currentHealth;
    public int GetCurrentHealth => currentHealth;
    //[SerializeField] Image damageImage;
    //[SerializeField] float flashSpeed = 0.5f;
    //[SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    bool isDead;
    protected bool damaged;


    private void Awake()
    {
        IsPlayer = gameObject.tag == "Player";
        //CustomDebug.LogCheckAssigned(HealthEvent, this);
        currentHealth = startingHealth;
        CustomDebug.Log($"{currentHealth}");
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        CustomDebug.Log($"{currentHealth}");
        if (IsPlayer)
        {
            HealthEvent.Raise(new GameLogicHealthEventType(currentHealth, startingHealth));
        }
        
        // TODO: 플레이어 아닐떄에만 폭발사
        if (currentHealth <= 0 && !isDead && !IsPlayer)
        {
            Instantiate(objExplotionEfx, transform.position, transform.rotation);
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
