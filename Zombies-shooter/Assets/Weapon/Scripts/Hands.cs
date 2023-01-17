using UnityEngine;

public class Hands : Weapon
{
    [SerializeField] private int damage = 20;

    public override void Attack(Vector3 target, GameObject targetObj)
    {
        if (_isPostShotDelay) return;
        targetObj.GetComponent<HealthPoints>().TakeDamage(damage);
        _animator.SetTrigger("Attack");
        StartCoroutine(WaitPostShotDelay());
    }
}
