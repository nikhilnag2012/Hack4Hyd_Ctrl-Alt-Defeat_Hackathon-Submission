using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    /*// Singleton instance for global access from any script
    public static HealthBarUI Instance { get; private set; }

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;

    // Public read-only properties
    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    [Header("UI References")]
    [SerializeField] private GameObject heartPrefab;    // UI Image Prefab
    [SerializeField] private Transform heartContainer;   // Panel with Horizontal Layout Group

    [Header("Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;   // Optional

    private List<Image> heartImages = new List<Image>();

    private void Awake()
    {
        // Enforce single instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize starting health
        CurrentHealth = maxHealth;

        // Spawn heart icons & render UI
        SetupHearts();
        UpdateHearts();
    }

    /// <summary>
    /// Spawns heart UI elements dynamically based on maxHealth.
    /// </summary>
    public void SetupHearts()
    {
        // Clear old hearts if re-initializing
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear();

        // Instantiate heart prefabs equal to maxHealth
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = newHeart.GetComponent<Image>();

            if (heartImage != null)
            {
                heartImages.Add(heartImage);
            }
        }
    }

    /// <summary>
    /// Reduces health by the given amount (default 1).
    /// </summary>
    public void TakeDamage(int amount = 1)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        UpdateHearts();

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player has died!");
        }
    }

    /// <summary>
    /// Restores health by the given amount (default 1).
    /// </summary>
    public void Heal(int amount = 1)
    {
        if (CurrentHealth >= maxHealth) return;

        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        UpdateHearts();
    }

    /// <summary>
    /// Updates all spawned heart images based on current health.
    /// </summary>
    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < CurrentHealth)
            {
                // Active heart
                heartImages[i].sprite = fullHeartSprite;
                heartImages[i].enabled = true;
            }
            else
            {
                // Lost heart
                if (emptyHeartSprite != null)
                {
                    heartImages[i].sprite = emptyHeartSprite;
                }
                else
                {
                    heartImages[i].enabled = false;
                }
            }
        }
    }*/
    [Header("UI References")]
    [SerializeField] private GameObject heartPrefab; // Assign your Heart Prefab
    [SerializeField] private Transform heartContainer; // Assign HealthBarContainer Transform

    [Header("Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    public int currHealth = 0;

    private List<Image> heartImages = new List<Image>();

    /// <summary>
    /// Call this once at start to initialize the hearts based on maximum health.
    /// </summary>
    public void SetupHearts(int maxHealth)
    {
        // Clear existing heart objects if any
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear();

        // Instantiate heart prefabs equal to max health
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = newHeart.GetComponent<Image>();

            if (heartImage != null)
            {
                heartImage.sprite = fullHeartSprite;
                heartImages.Add(heartImage);
            }
        }
        currHealth = maxHealth;
    }

    /// <summary>
    /// Call this whenever the player's current health changes.
    /// </summary>
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < currentHealth)
            {
                // Health is active for this index
                heartImages[i].sprite = fullHeartSprite;
                heartImages[i].enabled = true;
            }
            else
            {
                // Health is missing: either swap to empty sprite or disable image
                if (emptyHeartSprite != null)
                {
                    heartImages[i].sprite = emptyHeartSprite;
                }
                else
                {
                    heartImages[i].enabled = false; // Hide heart completely if no empty sprite
                }
            }
        }
        currHealth = currentHealth;
    }
}
