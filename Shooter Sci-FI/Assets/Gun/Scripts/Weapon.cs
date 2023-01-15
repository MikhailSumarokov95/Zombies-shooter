using System.Collections;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrel;
    [SerializeField] private float postShotDelay;
    [SerializeField] private int magazineSize;
    [SerializeField] private float timeReloading;
    [SerializeField] private Animator _animator;
    private bool _isPostShotDelay;
    private bool _isReloading;
    private TMP_Text _restOfBulletText;

    private int _restOfBulletInMagazine;
    public int RestOfBulletInMagazine
    {
        get 
        {
            return _restOfBulletInMagazine;
        }
        set
        {
            _restOfBulletInMagazine = value;
            if (gameObject.CompareTag("PlayerWeapon")) 
                _restOfBulletText.text = _restOfBulletInMagazine.ToString();
        }
    }

    private void Start()
    {
        _restOfBulletText = GameObject.FindGameObjectWithTag("RestBullet").GetComponent<TMP_Text>();
        RestOfBulletInMagazine = magazineSize;
    }

    public void Fire(Vector3 direction)
    {
        if (_isReloading) return;
        if (RestOfBulletInMagazine == 0) StartCoroutine(Reloading());
        if (_isPostShotDelay) return;
        Instantiate(bullet, barrel.position, Quaternion.LookRotation(direction - transform.position));
        RestOfBulletInMagazine--;
        if (_animator != null) _animator.SetTrigger("Fire");
        StartCoroutine(WaitPostShotDelay());
    }

    private IEnumerator WaitPostShotDelay()
    {
        _isPostShotDelay = true;
        yield return new WaitForSecondsRealtime(postShotDelay);
        _isPostShotDelay = false;
    }

    private IEnumerator Reloading()
    {
        if (_animator != null) _animator.SetTrigger("Reload");
        _isReloading = true;
        yield return new WaitForSecondsRealtime(timeReloading);
        _isReloading = false;
        RestOfBulletInMagazine = magazineSize;
    }
}