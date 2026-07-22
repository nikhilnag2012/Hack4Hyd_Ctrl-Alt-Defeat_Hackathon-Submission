using System.IO;
using UnityEngine;

public class SaveFileManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string mapsPath = Path.Combine(Application.persistentDataPath, "Chapters");
        string mathsPath = Path.Combine(Application.persistentDataPath, "Chapters", "Maths");
        string SavePath = Path.Combine(Application.persistentDataPath, "Saves");
        Directory.CreateDirectory(mapsPath);
        Directory.CreateDirectory(mathsPath);
        Directory.CreateDirectory(SavePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
