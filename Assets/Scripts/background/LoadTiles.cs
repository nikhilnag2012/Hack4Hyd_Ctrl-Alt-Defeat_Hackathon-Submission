using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class LoadTiles : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile; // the tile asset to place

    public void BuildFromArray(int[][] map, int width, int height)
    {
        if (tile == null)
        {
            Debug.LogError("BuildFromArray: tile is not assigned.");
            return;
        }

        if (map == null || map.Length < height)
        {
            Debug.LogError($"BuildFromArray: map has {map?.Length ?? 0} rows but height is {height}.");
            return;
        }

        for (int y = 0; y < height; y++)
        {
            int rowWidth = Mathf.Min(width, map[y].Length);

            for (int x = 0; x < rowWidth; x++)
            {
                if (map[y][x] == 1)
                {
                    Vector2Int cell2D = new Vector2Int(x, y);
                    Vector3Int cellPos = new Vector3Int(cell2D.x, cell2D.y, 0);
                    tilemap.SetTile(cellPos, tile);
                }
            }
        }
    }

    public void GetJson(string Filepath)
    {
        string jsonText = File.ReadAllText(Filepath);
        try
        {
            LevelFileFormat levelLayout = JsonConvert.DeserializeObject<LevelFileFormat>(jsonText);
            foreach(int n in levelLayout.tiles[0])
            {
                Debug.Log(n);
            }
            BuildFromArray(levelLayout.tiles, levelLayout.width, levelLayout.height);
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