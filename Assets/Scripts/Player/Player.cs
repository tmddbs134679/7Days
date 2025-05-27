using UnityEngine;
using UnityEngine.InputSystem;

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
    private Rigidbody _rigidbody;
    // Player Data
    [SerializeField] private PlayerDataSO playerDataSO;
    public PlayerDataSO PlayerDataSO { get => playerDataSO; }

    private PlayerController playerController;
    private PlayerStatus playerStatus;
    private PlayerMovement playerMovement;
    private PlayerVehicleHandler playerVehicle;
    public PlayerState CurState { get; private set; }

    // Player Events
    public PlayerEventHandler PlayerEvents { get; private set; }

    public bool CanDash { get; set; }
    public bool IsDead { get; private set; }
    public bool OnVehicle { get; set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        PlayerEvents = new PlayerEventHandler();

        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();
        playerMovement = GetComponent<PlayerMovement>();
        playerVehicle = GetComponent<PlayerVehicleHandler>();

        if (playerController)
            playerController.Init(this);
        if (playerStatus)
            playerStatus.Init(this);
        if (playerMovement)
            playerMovement.Init(this, _rigidbody);
        if (playerVehicle)
            playerVehicle.Init(this);

        CurState = PlayerState.Idle;

        CanDash = true;
        IsDead = false;
        OnVehicle = false;
    }

    void FixedUpdate()
    {
        if (!OnVehicle)
            playerMovement.Move(playerController.MoveDirection, playerStatus.MoveSpeed);
    }

    void Update()
    {
        if (!OnVehicle)
            playerMovement.Rotate(playerController.LookDirection);
    }

    public void ChangeState(PlayerState state)
    {
        CurState = state;
    }

    public void Dash()
    {
        if (CanDash && playerStatus.UseStamina(playerDataSO.DashStamina) && !OnVehicle)
        {
            float dashSpeed = playerDataSO.DashSpeed;
            float duration = playerDataSO.DashDuration;
            float cooldown = playerDataSO.DashCoolDown;

            StartCoroutine(playerMovement.DashCoroutine(playerController.LookDirection, dashSpeed, duration, cooldown));
        }
    }

    public void SetVehicle(VehicleController vehicle)
    {
        _rigidbody.isKinematic = !OnVehicle;
        playerVehicle.SetVehicle(vehicle, GetComponent<PlayerInput>());
    }

    public void Dead()
    {
        IsDead = true;
    }
}
