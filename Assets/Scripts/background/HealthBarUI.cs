using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject heartPrefab; // Assign Heart Prefab
    [SerializeField] private Transform heartContainer; // Assign transform of HealthBar

    [Header("Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

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

        // Set no. of heart prefabs to max health
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
    }
}
