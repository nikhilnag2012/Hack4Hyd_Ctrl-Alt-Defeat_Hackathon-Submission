using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadTiles : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile; // the tile asset to place
    public GameObject enemyPrefab; //The enemy prefab to spawn

    [Header("Dialogue Card")]
    public GameObject dialogueCard;
    public Text dialogueText;
    public Button dialoguebutton;

    [Header("Player")]
    public PlayerMovement player;

    public void BuildFromArray(int[][] map, int width, int height)
    {
        if (tile == null)
        {
            Debug.LogError("BuildFromArray: tile is not assigned.");
            return;
        }

        if (tilemap == null)
        {
            Debug.LogError("BuildFromArray: tilemap is not assigned.");
            return;
        }

        if (map == null || map.Length < height)
        {
            Debug.LogError($"BuildFromArray: map has {map?.Length ?? 0} rows but height is {height}.");
            return;
        }

        // Clear any tiles from a previous build so stale tiles don't linger.
        tilemap.ClearAllTiles();

        var positions = new List<Vector3Int>();

        for (int y = 0; y < height; y++)
        {
            if (map[y] == null)
            {
                Debug.LogWarning($"BuildFromArray: row {y} is null, skipping.");
                continue;
            }

            if (map[y].Length < width)
            {
                Debug.LogWarning($"BuildFromArray: row {y} has {map[y].Length} cells but width is {width}.");
            }

            int rowWidth = Mathf.Min(width, map[y].Length);

            for (int x = 0; x < rowWidth; x++)
            {
                if (map[y][x] == 1)
                {
                    positions.Add(new Vector3Int(x, -y, 0));
                }
            }
        }

        if (positions.Count == 0)
            return;

        var tiles = new TileBase[positions.Count];
        for (int i = 0; i < tiles.Length; i++)
            tiles[i] = tile;

        tilemap.SetTiles(positions.ToArray(), tiles);
    }

    public void SpawnEnemies(GameObject prefab, Vector2 pos, string puzzle, string type, string message) {
        GameObject newEnemy = Instantiate(prefab, pos, Quaternion.identity);
        newEnemy.GetComponent<enemyScript>().type = type;
        newEnemy.GetComponent<enemyScript>().puzzle = puzzle;
        newEnemy.GetComponent<enemyScript>().message = message;
        newEnemy.GetComponent<enemyScript>().dialogueCard = dialogueCard;
        newEnemy.GetComponent<enemyScript>().dialogueText = dialogueText;
        newEnemy.GetComponent<enemyScript>().dialoguebutton = dialoguebutton;
        newEnemy.GetComponent<enemyScript>().player = player;
    }

    public void GetJson(string Filepath)
    {
        string jsonText = File.ReadAllText(Filepath);
        try
        {
            LevelFileFormat levelLayout = JsonConvert.DeserializeObject<LevelFileFormat>(jsonText);
            CompleteInfo.level = levelLayout;
            foreach(int n in levelLayout.tiles[0])
            {
                Debug.Log(n);
            }
            BuildFromArray(levelLayout.tiles, levelLayout.width, levelLayout.height);
            foreach (challengeField i in levelLayout.challenges)
            {
                SpawnEnemies(enemyPrefab, new Vector2((float)i.pos.x, (float)i.pos.y), i.puzzle, i.type, i.message);
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error in decoding Json" + e);
        }
        

    }
}

[Serializable]
public class LevelFileFormat
{
    public int width {  get; set; }
    public int height { get; set; }
    public int[][] tiles { get; set; }
    public d2Coordinates start { get; set; }
    public notesField[] notes { get; set; }
    public challengeField[] challenges { get; set; }
    public Dictionary<string, puzzleFormat> puzzles { get; set; }
}

[Serializable]
public class challengeField
{
    public d2Coordinates pos { get; set; }
    public string puzzle { get; set; }
    public string type { get; set; }
    public string message { get; set; }
}

[Serializable]
public class notesField
{
    public d2Coordinates pos { get; set; }
    public string message { get; set; }
}

[Serializable]
public class d2Coordinates {
    public double x { get; set; }
    public double y { get; set; }
}

[Serializable]
public class puzzleFormat
{
    public string question { get; set; }
    public double answer { get; set; }
    public string type { get; set; }
}