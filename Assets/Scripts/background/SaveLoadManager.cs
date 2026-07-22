using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private HealthBarUI healthScript; // Drag your health script here

    private string saveFileName = "most recent save.json";
    private string saveFilePath;

    private void Awake()
    {
        // Path resolves to Application.persistentDataPath/most recent save.json
        saveFilePath = Path.Combine(Application.persistentDataPath,"Saves", saveFileName);
    }

    /// <summary>
    /// Saves player position and public currHealth to JSON.
    /// </summary>
    public void SaveGame()
    {
        if (playerTransform == null || healthScript == null)
        {
            Debug.LogError("Missing required references in SaveLoadManager!");
            return;
        }

        // Create save container
        SaveData dataToSave = new SaveData(healthScript.currHealth, playerTransform.position);

        // Convert to JSON and save to disk
        string jsonString = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
        File.WriteAllText(saveFilePath, jsonString);

        Debug.Log($"Game saved :- Health: {healthScript.currHealth}, Position: {playerTransform.position}");
    }

    /// <summary>
    /// Loads saved data and restores player position and currHealth.
    /// </summary>
    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning($"No save file found at: {saveFilePath}");
            return;
        }

        // Read string and deserialize
        string jsonString = File.ReadAllText(saveFilePath);
        SaveData loadedData = JsonConvert.DeserializeObject<SaveData>(jsonString);

        // Apply position
        if (playerTransform != null)
        {
            playerTransform.position = loadedData.GetVector2Position();
        }

        // Apply health
        if (healthScript != null)
        {
            healthScript.currHealth = loadedData.health;

            // Optional: Call your UI refresh function if you have one!
            // healthScript.UpdateUI(); 
        }

        Debug.Log($"Game Loaded! Restored Health: {healthScript.currHealth}");
    }
}

// Simple serializable container
[System.Serializable]
public class SaveData
{
    public int health;
    public float posX;
    public float posY;
    

    public SaveData(int health, Vector2 position)
    {
        this.health = health;
        this.posX = position.x;
        this.posY = position.y;
        
    }

    public Vector2 GetVector2Position()
    {
        return new Vector2(posX, posY);
    }
}