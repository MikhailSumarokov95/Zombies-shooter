using UnityEngine;
using Bot;

public class RangedWeapon : Weapon
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform barrel;

    public override void Attack(GameObject targetObj)
    {
        if (_isPostShotDelay) return;
        var targetAttack = targetObj.transform.position + new Vector3(0f, targetObj.GetComponent<CapsuleCollider>().height * 0.85f, 0f) - barrel.transform.position;
        Instantiate(bullet, barrel.position, Quaternion.LookRotation(targetAttack));
        _animator.SetTrigger("Attack");
        StartCoroutine(WaitPostShotDelay());
    }
}
