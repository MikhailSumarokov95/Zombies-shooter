using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 1f;
    private Rigidbody bulletRb;
    private bool isTakeDamage;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        StartCoroutine(TimerDestroy());
    }

    private void Update()
    {
        bulletRb.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTakeDamage) return;
        var healthPoint = other.gameObject.GetComponent<HealthPoints>();
        if (healthPoint != null)
        {
            isTakeDamage = true;
            healthPoint.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
