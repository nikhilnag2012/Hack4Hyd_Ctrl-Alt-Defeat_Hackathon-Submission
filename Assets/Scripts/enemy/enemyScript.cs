using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class enemyScript : MonoBehaviour
{
    [Header("Json Info")]
    public string message;
    public string type;
    public string puzzle;

    [Header("Dialogue Card")]
    public GameObject dialogueCard;
    public Text dialogueText;
    public Button dialoguebutton;

    [Header("Player")]
    public PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.name == "player") {
            player.enabled = false;
            dialogueCard.SetActive(true);
            dialogueText.text = message;
            dialoguebutton.onClick.AddListener(() => { 
                ActivePuzzle.puzzle = puzzle;
                SceneManager.LoadScene("NumericalQuestion");
            });
        }
    }
}
