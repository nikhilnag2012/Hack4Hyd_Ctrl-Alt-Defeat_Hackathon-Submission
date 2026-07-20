using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    void Start()
    {
        if(!player)
        {
            Debug.Log("Player is not assigned. Please assign the player field");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(player);
    }
}
