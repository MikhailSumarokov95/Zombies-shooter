using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    private Life _life;

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
            if (healthBar != null) healthBar.value = currentHealth;
        }
    }

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        _life = GetComponent<Life>();
    }

    public void TakeDamage(int damage)
    {
        if (_life.IsDid) return;
        var remainingHealth = CurrentHealth - damage;
        if (remainingHealth <= 0) ToDead();
        CurrentHealth = remainingHealth;
    }

    public void TakeHealth(int health)
    {
        if (_life.IsDid) return;
        var remainingHealth = CurrentHealth + health;
        if (remainingHealth >= maxHealth) CurrentHealth = maxHealth;
        else CurrentHealth = remainingHealth;
    }

    private void ToDead()
    {
        if (gameObject.CompareTag("Player")) print("you did");
        else
        {
            GetComponent<Animator>().SetTrigger("Did");
            GetComponent<Life>().OnDid();
        }
    }
}
