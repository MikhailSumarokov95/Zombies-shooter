using UnityEngine;

public class AIBotController : MonoBehaviour
{
    [SerializeField] private Transform eyesTr;
    [Range(0f, 30f)]
    [SerializeField] private float angleVisibility = 5f;
    [Range(0, 100)]
    [SerializeField] private int hitAccuracy = 50;
    private Transform _target;
    private CapsuleCollider _targetCol;
    private BotMove _botMove;
    private Weapon _gun;
    private Vector3 _sideIsTargetIsVisible;
    private Animator _animator;

    private void Start()
    {
        _target = GetComponent<TargetMoveBot>().GetTarget();
        _targetCol = _target.GetComponent<CapsuleCollider>();
        _botMove = GetComponent<BotMove>();
        _gun = transform.GetComponentInChildren<Weapon>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DetermineIfThereObstaclesBetweenTargetAndBot())
            if (TargetVisibilityCheck()) Fire();
            else _botMove.RotateTowardsTarget();
        else _botMove.RunTowardsTarget();
    }

    private bool TargetVisibilityCheck()
    {
        var rotationLookAnTarget = Quaternion.LookRotation(_target.position - transform.position);
        if (Mathf.Abs(transform.rotation.eulerAngles.y - rotationLookAnTarget.eulerAngles.y) < angleVisibility)
            return true;
        else return false;
    }

    private bool DetermineIfThereObstaclesBetweenTargetAndBot()
    {
        var sideIsTarget = GetSideIsTargetWithIndent(0.9f);

        for (var i = 0; i < sideIsTarget.Length; i++)
        {
            Debug.DrawRay(eyesTr.position, sideIsTarget[i] - eyesTr.position, Color.red);
            RaycastHit hit;
            if (!Physics.Raycast(eyesTr.position, sideIsTarget[i] - eyesTr.position, out hit)) continue;
            if (hit.collider.gameObject == _target.gameObject)
            {
                _sideIsTargetIsVisible = sideIsTarget[i];
                return true;
            }
        }
        return false;
    }

    private void Fire()
    {
        var isHittingTarget = Random.Range(0, 101) <= hitAccuracy;
        if (isHittingTarget) _gun.Fire(_sideIsTargetIsVisible);
        else
        {
            var hittingPoint = GetSideIsTargetWithIndent(1.1f)[Random.Range(0, 4)];
            _gun.Fire(hittingPoint);
        }
    }

    private Vector3[] GetSideIsTargetWithIndent(float indentFromSide)
    {
        var perpendicularToVectorBetweenTargetAndBot =
            (Quaternion.Euler(0, 90, 0) * (_target.position - transform.position)).normalized;
        var sideIsTarget = new Vector3[4];
        sideIsTarget[0] = _targetCol.transform.up * indentFromSide * _targetCol.height + _target.position;
        sideIsTarget[1] = _targetCol.transform.up * (1 - indentFromSide) + _target.position;
        sideIsTarget[2] = - perpendicularToVectorBetweenTargetAndBot * indentFromSide * _targetCol.radius + _target.position +
            _targetCol.transform.up * _targetCol.height / 2;
        sideIsTarget[3] = perpendicularToVectorBetweenTargetAndBot * indentFromSide * _targetCol.radius + _target.position +
            _targetCol.transform.up * _targetCol.height / 2;
        return sideIsTarget;
    }
}