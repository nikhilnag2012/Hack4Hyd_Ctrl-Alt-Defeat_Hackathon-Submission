using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                if (CompleteInfo.level.puzzles[puzzle].type == "Numerical")
                {
                    SceneManager.LoadScene("NumericalQuestion");

                }
                else if (CompleteInfo.level.puzzles[puzzle].type == "Fraction")
                {
                    //TODO: Change to fraction scene

                }
            });
        }
    }
}
