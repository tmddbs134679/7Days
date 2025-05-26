using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Data
    [SerializeField] private PlayerDataSO playerDataSO;
    public PlayerDataSO PlayerDataSO { get => playerDataSO; }
    
    // Player Events
    public PlayerEventHandler PlayerEvents { get; private set; }

    private Rigidbody _rigidbody;
    private PlayerController playerController;
    private PlayerStatus playerStatus;

    public bool IsDie { get; private set; }

    private void Awake()
    {
        PlayerEvents = new PlayerEventHandler();

        _rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();

        if (playerController)
            playerController.Init(this);
        if (playerStatus)
            playerStatus.Init(this);

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
        velocity.x = direction.x * playerStatus.MoveSpeed;
        velocity.z = direction.z * playerStatus.MoveSpeed;

        _rigidbody.velocity = velocity;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(playerController.LookDirection.x, playerController.LookDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void Die()
    {
        IsDie = true;
    }
}
