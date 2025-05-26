using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSO;
    public PlayerDataSO PlayerDataSO { get => playerDataSO; }
    private Rigidbody _rigidbody;
    private PlayerController playerController;
    private PlayerStatus playerStatus;
    private float moveSpeed;
    public bool IsRun { get; private set; }
    public bool IsDie { get; private set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();

        if(playerController)
            playerController.Init(this);
        if(playerStatus)
            playerStatus.Init(this);

        moveSpeed = playerDataSO.WalkSpeed;
        IsDie = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Rotate();
    }

    private void Move()
    {
        Vector3 direction = Vector3.right * playerController.MoveDirection.x + Vector3.forward * playerController.MoveDirection.y;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = direction.x * moveSpeed;
        velocity.z = direction.z * moveSpeed;

        _rigidbody.velocity = velocity;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(playerController.LookDirection.x, playerController.LookDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetRun()
    {
        IsRun = !IsRun;
        moveSpeed = IsRun ? playerDataSO.RunSpeed : playerDataSO.WalkSpeed;
    }
}
