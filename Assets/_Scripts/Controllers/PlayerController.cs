using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData PlayerData;

    [SerializeField] private float maxSpeed;
    [SerializeField, Range(1f, 10f)] private float acceleration;
    [SerializeField, Range(0.1f, 1f)] private float groundDecay;

    private int currentHealth;
    private int maxHealth;
    private int damage;

    private Vector2 input;
    private Rigidbody2D playerRb;
    public bool inShadow;
    private bool isAttacking;
    private bool updatedSpeed;

    private void Awake()
    {
        // return if there is no player data assigned.
        if (PlayerData == null)
        {
            Debug.LogError("PlayerData is not assigned in the Inspector!");
            return;
        }

        // initialize local variables to the asset variables
        currentHealth = PlayerData.Health;
        maxHealth = PlayerData.MaxHealth;
        damage = PlayerData.Damage;

        PlayerData.Body = GetComponent<Rigidbody2D>();
        playerRb = PlayerData.Body;
        isAttacking = PlayerData.IsAttacking;
    }

    private void Start()
    {
        Debug.Log("Player health is: " + currentHealth);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
        ApplyFriction();
    }

    private void Move()
    {
        // check if the player is moving and stop handeling input if player is not moving
        if (input == Vector2.zero) return;

        Vector2 increase = input * acceleration;
        Vector2 newVelocity = playerRb.velocity + increase;
        newVelocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);

        playerRb.velocity = newVelocity;
    }

    private void ApplyFriction()
    {
        if (input == Vector2.zero)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x * groundDecay, playerRb.velocity.y * groundDecay);
        }
    }

    private void HandleInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

        //if (inShadow && !updatedSpeed)
        //{
        //    speed *= 5f;
        //    updatedSpeed = true;
        //    Debug.Log(speed);
        //}
        //else // set speed back to normal
        //{
        //    updatedSpeed = false;
        //    speed = baseSpeed;
        //}
}
