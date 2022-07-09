using System.IO;
using TMPro;
using UnityEngine;

public class AffScore : MonoBehaviour
{
    //Application.dataPath + "/Script/UI/" + "score_jeu_un.txt"
    public TextMeshProUGUI txt_score2;
    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        var varTemp = 0;
        var nbScores = 0;
        var fileName = Application.dataPath + "/Saves/" + "score_" + GameManager.gameInfo.GameScene + ".txt";
        TextReader reader;
        reader = new StreamReader(fileName);
        string line;
        while (true)
        {
            ++nbScores;
            // lecture de la ligne
            line = reader.ReadLine();
            // si la ligne est vide on arrête
            if (line == null) break;

            if (nbScores == 6)
                break;
            // on affiche la ligne

            if (varTemp == 1)
            {
                txt_score2.text = txt_score2.text + '\n' + line;
            }
            else
            {
                txt_score2.text = line;
                varTemp++;
            }
        }

        reader.Close();
    }

    private void Update()
    {
        if (_playerActions.UI.Submit
            .WasPressedThisFrame()) // en appuaynt sur "back space" on retourne au menu de choix de jeu
        {
            GameManager.twoPlayers = false;
            GameManager.instance.LoadScene("GameMenu");
        }
    }

    private void OnEnable()
    {
        _playerActions.UI.Enable();
    }

    private void OnDisable()
    {
        _playerActions.UI.Disable();
    }
}