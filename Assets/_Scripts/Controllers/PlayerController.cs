using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData PlayerData;

    private int currentHealth;
    private int maxHealth;
    private int damage;

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
    }

    private void Start()
    {
        Debug.Log("Player health is: " + currentHealth);
    }
}
