using System.Collections;
using UnityEngine;

public class AminationZombie : MonoBehaviour
{
    [SerializeField] private int timeEating = 10;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(TimeEating());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator TimeEating()
    {
        while (true)
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("zombie eating"));
            yield return new WaitForSeconds(timeEating);
            _animator.SetTrigger("Stay");
        }
    }
}
