using UnityEngine;
using UnityEngine.InputSystem;

public class ReduceHealth : MonoBehaviour
{
    public HealthManagement hm;
    public SaveLoadManager slm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.qKey.wasReleasedThisFrame)
        {
            hm.TakeDamage(1);
        }

        if(Keyboard.current.pKey.wasReleasedThisFrame)
        {
            slm.SaveGame();
        }
        if (Keyboard.current.kKey.wasReleasedThisFrame)
        {
            slm.LoadGame();
        }
    }
}
