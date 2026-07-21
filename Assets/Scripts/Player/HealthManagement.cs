using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private HealthBarUI healthBarUI;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBarUI != null)
        {
            healthBarUI.SetupHearts(maxHealth);
            healthBarUI.UpdateHearts(currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (healthBarUI != null)
        {
            healthBarUI.UpdateHearts(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        if (healthBarUI != null)
        {
            healthBarUI.UpdateHearts(currentHealth);
        }
    }
}
