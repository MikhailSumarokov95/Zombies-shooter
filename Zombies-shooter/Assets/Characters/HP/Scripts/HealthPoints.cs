using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int currentHealth;
    private Life _life;

    private bool _isHit;
    public bool IsHit { get { return _isHit;  } set { _isHit = value; } }

    private bool _corotuneDelayTimerAfterHit;


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
        if (healthBar != null) healthBar.maxValue = maxHealth;
            CurrentHealth = maxHealth;
        _life = GetComponent<Life>();
    }

    public void TakeDamage(int damage)
    {
        if (_life.IsDid) return;
        var remainingHealth = CurrentHealth - damage;
        if (remainingHealth <= 0) ToDead();
        CurrentHealth = remainingHealth;
        if (!_corotuneDelayTimerAfterHit) 
            StartCoroutine(DelayTimerAfterHit());
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
            GetComponent<Life>().Did();
        }
    }

    private IEnumerator DelayTimerAfterHit()
    {
        _corotuneDelayTimerAfterHit = true;
        IsHit = true;
        yield return new WaitForSeconds(1f);
        _corotuneDelayTimerAfterHit = false;
        IsHit = false;
    }
}
