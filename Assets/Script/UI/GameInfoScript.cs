using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInfoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameInfo gameInfo
            = GameManager.gameInfo;
        GameObject.Find("GameInfoCanvas/Title").GetComponent<TextMeshProUGUI>().text = gameInfo.Title;
        GameObject.Find("GameInfoCanvas/Description").GetComponent<TextMeshProUGUI>().text = gameInfo.Description;
    }

    
}
