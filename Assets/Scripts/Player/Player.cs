using System.Collections;
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

    private enum PlayerState
    {
        Idle,
        Dash,
        Gathering
    }

    private PlayerState curState;
    public bool canDash { get; private set; }
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

        curState = PlayerState.Idle;

        canDash = true;
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
        if (playerStatus == null || curState == PlayerState.Dash) return;

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

    public void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        curState = PlayerState.Dash;

        Vector3 dashDirection = playerController.LookDirection;
        float dashSpeed = playerDataSO.DashSpeed;

        float startTime = Time.time;

        _rigidbody.useGravity = false;

        while (Time.time < startTime + playerDataSO.DashDuration)
        {
            _rigidbody.velocity = new Vector3(dashDirection.x * dashSpeed, 0f, dashDirection.z * dashSpeed);
            yield return null;
        }

        _rigidbody.useGravity = true;
        curState = PlayerState.Idle;

        yield return new WaitForSeconds(playerDataSO.DashCoolDown);
        canDash = true;
    }

    public void Die()
    {
        IsDie = true;
    }
}
