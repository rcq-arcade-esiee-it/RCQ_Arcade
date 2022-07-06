using TMPro;
using UnityEngine;
/// <summary>Class <c>ScorePlayer</c> représentant le texte affichant le score du joueur </summary>
public class ScorePlayer : MonoBehaviour
{
    private TextMeshProUGUI playerScore;

    private void Start()
    {
        playerScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.name ==("Player_1_Score"))
        
            playerScore.text = FirstGameManager.instance.scorePlayer1.ToString();
        else 
            playerScore.text = FirstGameManager.instance.scorePlayer2.ToString();

    }
}