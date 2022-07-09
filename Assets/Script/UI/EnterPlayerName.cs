using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Object = UnityEngine.Object;

public class EnterPlayerName : MonoBehaviour
{
    // Start is called before the first frame update
    private String[] letters = new string[] {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
    private String[] score = new string[] {"FirstLetterPlayer","SecondLetterPlayer","ThirdLetterPlayer"};
    private String[] finalName = new []{"A","A","A"};
    private int indexLetter = 0;
    private int indexScore = 0;

    private TextMeshProUGUI text;
    private PlayerActions _playerActions;
    private String getCanvas;
    private Vector2 joystick;
    
    void Awake()
    {
        if (gameObject.name == "Player1")
        
            getCanvas = "Player1";

        
        else
        
            getCanvas = "Player2";

        
        _playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
            if (gameObject.name == "Player1")
            _playerActions.Player_Map.Enable();
            else
            
                _playerActions.Player2_Map.Enable();

            _playerActions.UI.Enable();

    }

    private void OnDisable()
    {
        if (gameObject.name == "Player1")
            _playerActions.Player_Map.Disable();
        else
            
            _playerActions.Player2_Map.Disable();                
        _playerActions.UI.Disable();

    }

    // Update is called once per frame
    void Update()
    {

        if (_playerActions.Player_Map.ScoreRight.WasPressedThisFrame() ||
            _playerActions.Player2_Map.ScoreRight.WasPressedThisFrame())
        {
            SelectLettersPolygon(0);

            indexScore += 1;
            if (indexScore > 2 )
                indexScore =0;
            SelectLettersPolygon(1);

        } else if (_playerActions.Player_Map.ScoreLeft.WasPressedThisFrame() ||
                   _playerActions.Player2_Map.ScoreLeft.WasPressedThisFrame())
        {
            SelectLettersPolygon(0);

            indexScore -= 1;
            if (indexScore < 0 )
            
                indexScore =2;
            SelectLettersPolygon(1);
        }
        
        else if (_playerActions.UI.Submit.WasPressedThisFrame())
        {
            if (gameObject.name == "Player1")
                PlayerScore.Player1Name = String.Concat(finalName);
            else
                PlayerScore.Player2Name = String.Concat(finalName);
            PlayerScore.saveScoreToCurrentGame(GameManager.gameInfo.GameScene);
            GameManager.instance.LoadScene("Aff_Score");

        }
        else
        {
            StartCoroutine(chooseLetter( GameObject.Find(getCanvas + "/" + score[indexScore]).GetComponent<TextMeshProUGUI>()));

        }
        
        


        

    }

    private IEnumerator chooseLetter(TextMeshProUGUI letter)
    {
        if (_playerActions.Player_Map.ScoreDown.WasPressedThisFrame() ||  _playerActions.Player2_Map.ScoreDown.WasPressedThisFrame())
        {          

            indexLetter += 1;
            if (indexLetter> letters.Length-1 )
            
                indexLetter =0;

            letter.text = letters[indexLetter];


            
        }
        else if (_playerActions.Player_Map.ScoreUp.WasPressedThisFrame() || _playerActions.Player2_Map.ScoreUp.WasPressedThisFrame() )
        {         

            indexLetter -= 1;
            if (indexLetter<0)
                indexLetter = letters.Length -1;
            letter.text = letters[indexLetter];



        }
        finalName[indexScore] = letters[indexLetter];

        yield break;

    }

    private void SelectLettersPolygon(int order)
    {
        var components =  GameObject.Find(getCanvas + "/" + score[indexScore]).GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprites in components)
            sprites.sortingOrder = order;
    }
    
}
