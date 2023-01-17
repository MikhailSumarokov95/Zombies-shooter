using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform barrel;

    public override void Attack(GameObject targetObj)
    {
        if (_isPostShotDelay) return;
        var targetAttack = targetObj.transform.position + new Vector3(0f, targetObj.GetComponent<CapsuleCollider>().height * 0.85f, 0f) - barrel.transform.position;
        //var targetAttack = targetObj.transform.position + new Vector3(0f, targetObj.GetComponent<CapsuleCollider>().height * 0.3f, 0f) - transform.position;
        Instantiate(bullet, barrel.position, Quaternion.LookRotation(targetAttack));
        _animator.SetTrigger("Attack");
        StartCoroutine(WaitPostShotDelay());
    }
}
