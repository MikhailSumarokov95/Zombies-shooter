using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveHorizontal { get { return _gameManager.IsMobile ? _joystick.Horizontal : Input.GetAxis("Horizontal"); } }

    public float MoveVertical { get { return _gameManager.IsMobile ? _joystick.Vertical : Input.GetAxis("Vertical"); } }

    public Vector3 Move { get { return new Vector3(MoveHorizontal, 0, MoveVertical); } }

    public bool IsLocked { get; set; }
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float maxUpHead = 30f;
    [SerializeField]
    private float maxDownHead = -15f;
    [SerializeField]
    private float moveHeadSpeed = 10f;
    [SerializeField]
    private GameObject head;
    private Joystick _joystick;
    private GameManager _gameManager;
    private TouchSystem _touchSystem;
    private CharacterController _characterController;
    private float _moveHeadX;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _touchSystem = FindObjectOfType<TouchSystem>();
        _gameManager = FindObjectOfType<GameManager>();
        _characterController = GetComponent<CharacterController>();

        if (!_gameManager.IsMobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
           // _joystick.gameObject.SetActive(false);
        }
        else _touchSystem.OnDragForMove += MoveHead;
    }

    private void OnEnable()
    {
        IsLocked = false;
        Camera.main.GetComponent<MainCamera>().SetTarget(head.transform);
    }

    private void Update()
    {
        //if (gameManager.IsPause) return;
        if (IsLocked) return;
        _characterController.SimpleMove(transform.rotation * Move * movementSpeed);
        if (!_gameManager.IsMobile)
            MoveHead(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime);
    }

    public void MoveHead(Vector2 mouse)
    {
        var directionMove = mouse * moveHeadSpeed;

        _moveHeadX += directionMove.y;
        _moveHeadX = Mathf.Clamp(_moveHeadX, maxDownHead, maxUpHead);
        head.transform.localEulerAngles = new Vector3(-_moveHeadX, head.transform.localEulerAngles.y, 0);

        transform.Rotate(Vector3.up, directionMove.x);
    }
}