using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    [SerializeField] private int damage = 20;

    public override void Fire(Vector3 target, GameObject targetObj)
    {
        if (_isPostShotDelay) return;
        targetObj.GetComponent<HealthPoints>().TakeDamage(damage);
        StartCoroutine(WaitPostShotDelay());
        _animator.SetTrigger("Fire");
    }
}
