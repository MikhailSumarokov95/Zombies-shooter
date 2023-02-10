using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int currentHealth;
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private GameObject takeDamageSoundPref;
    private Life _life;

    [SerializeField] private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

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
            if (currentHealthText != null) currentHealthText.text = currentHealth.ToString();
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

        if (takeDamageSoundPref != null)
        {
            var soundTakeDamage = Instantiate(takeDamageSoundPref, transform.position, transform.rotation);
            Destroy(soundTakeDamage, soundTakeDamage.GetComponent<AudioSource>().clip.length);
        }

        if (!_corotuneDelayTimerAfterHit) 
            StartCoroutine(DelayTimerAfterHit());
    }

    public void TakeHealth(int health)
    {
        var remainingHealth = CurrentHealth + health;
        if (remainingHealth >= maxHealth) CurrentHealth = maxHealth;
        else CurrentHealth = remainingHealth;
    }

    private void ToDead()
    {
        if (!gameObject.CompareTag("Player"))
            GetComponent<Animator>().SetTrigger("Did");

        GetComponent<Life>().Did();
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
