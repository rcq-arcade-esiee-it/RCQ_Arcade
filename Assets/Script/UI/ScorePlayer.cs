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
        playerScore.text = FirstGameManager.score.ToString();
    }
}