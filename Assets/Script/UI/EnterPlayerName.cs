using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterPlayerName : MonoBehaviour
{
    private readonly string[] finalName = { "A", "A", "A" };

    // Start is called before the first frame update
    private readonly string[] letters =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z"
    };

    private readonly string[] score = { "FirstLetterPlayer", "SecondLetterPlayer", "ThirdLetterPlayer" };
    private PlayerActions _playerActions;
    private string getCanvas;
    private int indexLetter;
    private int indexScore;

    private Vector2 joystick;

    private TextMeshProUGUI text;

    private void Awake()
    {
        if (gameObject.name == "Player1")

            getCanvas = "Player1";


        else

            getCanvas = "Player2";


        _playerActions = new PlayerActions();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((_playerActions.Player_Map.ChooseRight.WasPressedThisFrame() &&
             Joystick.current.name == PlayerJoystick.FirstJoystick) ||
            (_playerActions.Player2_Map.ChooseRight.WasPressedThisFrame() &&
             Joystick.current.name == PlayerJoystick.SecondJoystick))
        {
            SelectLettersPolygon(0);

            indexScore += 1;
            if (indexScore > 2)
                indexScore = 0;
            SelectLettersPolygon(1);
        }
        else if ((_playerActions.Player_Map.ChooseLeft.WasPressedThisFrame() &&
                  Joystick.current.name == PlayerJoystick.FirstJoystick) ||
                 (_playerActions.Player2_Map.ChooseLeft.WasPressedThisFrame() &&
                  Joystick.current.name == PlayerJoystick.SecondJoystick))
        {
            SelectLettersPolygon(0);

            indexScore -= 1;
            if (indexScore < 0)

                indexScore = 2;
            SelectLettersPolygon(1);
        }

        else if (_playerActions.UI.Submit.WasPressedThisFrame())
        {
            if (gameObject.name == "Player1")
                PlayerScore.Player1Name = string.Concat(finalName);
            else
                PlayerScore.Player2Name = string.Concat(finalName);
            PlayerScore.saveScoreToCurrentGame(GameManager.gameInfo.GameScene, gameObject.name);
            GameManager.instance.LoadScene("Aff_Score");
        }

        StartCoroutine(
            chooseLetter(GameObject.Find(getCanvas + "/" + score[indexScore]).GetComponent<TextMeshProUGUI>()));
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

    private IEnumerator chooseLetter(TextMeshProUGUI letter)
    {
        if ((_playerActions.Player_Map.ScoreDown.WasPressedThisFrame() &&
             Joystick.current.name == PlayerJoystick.FirstJoystick) ||
            (_playerActions.Player2_Map.ScoreDown.WasPressedThisFrame() &&
             Joystick.current.name == PlayerJoystick.SecondJoystick))
        {
            indexLetter += 1;
            if (indexLetter > letters.Length - 1)

                indexLetter = 0;

            letter.text = letters[indexLetter];

            finalName[indexScore] = letters[indexLetter];
        }
        else if ((_playerActions.Player_Map.ScoreUp.WasPressedThisFrame() && Joystick.current != null &&
                  Joystick.current.name ==
                  PlayerJoystick.FirstJoystick) ||
                 (_playerActions.Player2_Map.ScoreUp.WasPressedThisFrame() &&
                  Joystick.current.name == PlayerJoystick.SecondJoystick))
        {
            indexLetter -= 1;
            if (indexLetter < 0)
                indexLetter = letters.Length - 1;
            letter.text = letters[indexLetter];
            finalName[indexScore] = letters[indexLetter];
        }

        yield break;
    }

    private void SelectLettersPolygon(int order)
    {
        var components = GameObject.Find(getCanvas + "/" + score[indexScore]).GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprites in components)
            sprites.sortingOrder = order;
    }
}