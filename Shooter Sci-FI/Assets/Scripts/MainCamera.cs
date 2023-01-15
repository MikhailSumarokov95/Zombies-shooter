using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform _target;

    private void LateUpdate()
    {
        transform.SetPositionAndRotation(_target.position, _target.rotation);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}