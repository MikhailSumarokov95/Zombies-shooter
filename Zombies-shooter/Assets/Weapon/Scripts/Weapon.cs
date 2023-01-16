using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //[SerializeField] protected GameObject bullet;
    //[SerializeField] protected Transform barrel;
    [SerializeField] protected float postShotDelay;
    //[SerializeField] protected int magazineSize;
    //[SerializeField] protected float timeReloading;
    [SerializeField] protected Animator _animator;
    protected bool _isPostShotDelay;
    protected bool _isReloading;

    [SerializeField] protected float _distanceAttack = 2;
    public float DistanceAttack { get { return _distanceAttack; } }
    //private int _restOfBulletInMagazine;
    //public int RestOfBulletInMagazine
    //{
    //    get
    //    {
    //        return _restOfBulletInMagazine;
    //    }
    //    set
    //    {
    //        _restOfBulletInMagazine = value;
    //    }
    //}

    protected void Start()
    {
        //RestOfBulletInMagazine = magazineSize;
        //_animator.GetComponent<Animator>();
    }

    public abstract void Fire(Vector3 target, GameObject targetObj);
    
        //if (_isReloading) return;
        //if (RestOfBulletInMagazine == 0) StartCoroutine(Reloading());
        //if (_isPostShotDelay) return;
        //Instantiate(bullet, barrel.position, Quaternion.LookRotation(direction - transform.position));
        //RestOfBulletInMagazine--;
        //if (_animator != null) _animator.SetTrigger("Fire");
        //StartCoroutine(WaitPostShotDelay());
    

    protected IEnumerator WaitPostShotDelay()
    {
        _isPostShotDelay = true;
        yield return new WaitForSecondsRealtime(postShotDelay);
        _isPostShotDelay = false;
    }

    //protected IEnumerator Reloading()
    //{
    //    if (_animator != null) _animator.SetTrigger("Reload");
    //    _isReloading = true;
    //    yield return new WaitForSecondsRealtime(timeReloading);
    //    _isReloading = false;
    //    RestOfBulletInMagazine = magazineSize;
    //}
}