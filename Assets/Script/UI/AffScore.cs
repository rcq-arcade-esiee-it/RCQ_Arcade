using TMPro;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using UnityEngine;

public class AffScore : MonoBehaviour
{
    //Application.dataPath + "/Script/UI/" + "score_jeu_un.txt"
    public TextMeshProUGUI txt_score2;
    void Start()
    {
         int varTemp = 0;
         int nbScores = 0;
        string fileName = Application.dataPath + "/Saves/" + "score_"+GameManager.gameInfo.gameScene+".txt";
        TextReader reader;
        reader = new  StreamReader(fileName);
        string line;
        while (true)
        {
            ++nbScores;
            // lecture de la ligne
            line=reader.ReadLine();
            // si la ligne est vide on arrête
            if (line==null)  break;

            if (nbScores ==6)
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

    void Update()
    {
        if (Keyboard.current.backspaceKey.wasPressedThisFrame) // en appuaynt sur "back space" on retourne au menu de choix de jeu
        {
            GameManager.twoPlayers = false;
            GameManager.instance.LoadScene("GameMenu");
        }
    }
    
}
