using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Dash,
    Gathering,
    Battle
}

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
    private PlayerMovement playerMovement;
    public PlayerState CurState { get; private set; }

    public bool CanDash { get; set; }
    public bool IsDead { get; private set; }

    private void Awake()
    {
        PlayerEvents = new PlayerEventHandler();

        _rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();
        playerMovement = GetComponent<PlayerMovement>();

        if (playerController)
            playerController.Init(this);
        if (playerStatus)
            playerStatus.Init(this);
        if (playerMovement)
            playerMovement.Init(this);

        CurState = PlayerState.Idle;

        CanDash = true;
        IsDead = false;
    }

    void FixedUpdate()
    {
        playerMovement.Move(playerController.MoveDirection, playerStatus.MoveSpeed);
    }

    void Update()
    {
        playerMovement.Rotate(playerController.LookDirection);
    }

    public void ChangeState(PlayerState state)
    {
        CurState = state;
    }

    public void Dash()
    {
        if (CanDash && playerStatus.UseStamina(playerDataSO.DashStamina))
        {
            float dashSpeed = playerDataSO.DashSpeed;
            float duration = playerDataSO.DashDuration;
            float cooldown = playerDataSO.DashCoolDown;

            StartCoroutine(playerMovement.DashCoroutine(playerController.LookDirection, dashSpeed, duration, cooldown));
        }
    }

    public void Dead()
    {
        IsDead = true;
    }
}
