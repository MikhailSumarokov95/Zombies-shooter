using System.Collections;
using UnityEngine;

namespace Bot
{

    public abstract class Weapon : MonoBehaviour
    {
        protected Animator _animator;

        protected bool _isPostShotDelay;
        protected bool _isReloading;

        public bool IsAttacking { get; protected set; }

        [SerializeField] protected float _distanceAttack = 2;
        public float DistanceAttack { get { return _distanceAttack; } }

        protected void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public abstract void Attack(GameObject targetObj);

        protected IEnumerator WaitPostShotDelay()
        {
            IsAttacking = true;
            _isPostShotDelay = true;
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            yield return new WaitForSecondsRealtime(_animator.GetCurrentAnimatorStateInfo(0).length);
            _isPostShotDelay = false;
            IsAttacking = false;
        }
    }
}