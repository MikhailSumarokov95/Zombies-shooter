using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    public Action OnDead;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int currentHealth;
    public int CurrentHealth 
    { 
        get 
        { 
            return currentHealth; 
        } 
        set 
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            healthBar.value = currentHealth;
        }
    }

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        var remainingHealth = CurrentHealth - damage;
        if (remainingHealth <= 0) ToDead();
        CurrentHealth = remainingHealth;
    }

    public void TakeHealth(int health)
    {
        var remainingHealth = CurrentHealth + health;
        if (remainingHealth >= maxHealth) CurrentHealth = maxHealth;
        else CurrentHealth = remainingHealth;
    }

    private void ToDead()
    {
        OnDead?.Invoke();
    }
}
