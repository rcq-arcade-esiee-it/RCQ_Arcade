using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Select_Equip : MonoBehaviour
{
    public Image maillot1;
    public Image maillot2;
    public Image maillot3;
    public Image maillot4;
    public Image logo1;
    public Image logo2;
    public Image logo3;
    public Image logo4;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public Image choixj1;
    public Image choixj2;
    private PlayerActions _playerActions;
    private int click;
    private int clickj2;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        choixj1.rectTransform.anchoredPosition = new Vector2(-177, -37);
        GameManager.instance.choixequipej1 = 2;
        choixj2.rectTransform.anchoredPosition = new Vector2(-120f, -37f);
        GameManager.instance.choixequipej2 = 2;
        if (GameManager.twoPlayers == false)
            { 
                choixj2.color = new Color(255, 255, 255, 0f);
            }
        MajMaillot1();
        MajMaillot2();
        MajMaillot3();
        MajMaillot4();
    }

    private void Update()
    {
        if (_playerActions.Player_Map.ChooseLeft.WasPressedThisFrame()  && Joystick.current.name == PlayerJoystick.FirstJoystick)
        {
            click++;
            Debug.Log("Press gauche");
            Debug.Log("click = " + click);
            Majj1();
        }
        else if (_playerActions.Player_Map.ChooseRight.WasPressedThisFrame() && Joystick.current.name == PlayerJoystick.FirstJoystick)
        {
            click--;
            Debug.Log("Press droite");
            Debug.Log("click = " + click);
            Majj1();
        }

        if (GameManager.twoPlayers)
        {
            if (_playerActions.Player2_Map.ChooseLeft.WasPressedThisFrame() && Joystick.current.name == PlayerJoystick.SecondJoystick)
            {
                Debug.Log("Press gauche");
                clickj2++;
                Majj2();
            }
            else if (_playerActions.Player2_Map.ChooseRight.WasPressedThisFrame()&& Joystick.current.name == PlayerJoystick.SecondJoystick)
            {
                clickj2--;
                Debug.Log("Press droite");
                Majj2();
            }
        }

        if (_playerActions.UI.Submit.WasPressedThisFrame())
            GameManager.instance.LoadScene(GameManager.gameInfo.GameScene);
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
        _playerActions.Player2_Map.Enable();
        _playerActions.UI.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
        _playerActions.Player2_Map.Disable();
        _playerActions.UI.Disable();
    }

    private void Majj1()
    {
        Debug.Log("Mise à jour du Joueur 1");
        if (click == 0)
        {
            Debug.Log("Joueur 1 : equipe 2");
            GameManager.instance.choixequipej1 = 2;
            choixj1.rectTransform.anchoredPosition = new Vector2(-177f, -37f);
            Debug.Log("Joueur 1 : on déplace le carré");
        }
        else if (click == 1 || click == -3)
        {
            Debug.Log("Joueur 1 : equipe 1");
            click = 1;
            GameManager.instance.choixequipej1 = 1;
            choixj1.rectTransform.anchoredPosition = new Vector2(-477f, -37f);
            Debug.Log("Joueur 1 : on déplace le carré");
        }
        else if (click == 2 || click == -2)
        {
            click = -2;
            GameManager.instance.choixequipej1 = 4;
            choixj1.rectTransform.anchoredPosition = new Vector2(420, -37);
        }

        else if (click == -1)
        {
            GameManager.instance.choixequipej1 = 3;
            choixj1.rectTransform.anchoredPosition = new Vector2(120, -37);
        }

        MajMaillot1();
        MajMaillot2();
        MajMaillot3();
        MajMaillot4();
    }

    private void Majj2()
    {
        if (clickj2 == 0)
        {
            GameManager.instance.choixequipej2 = 2;
            choixj2.rectTransform.anchoredPosition = new Vector2(-120f, -37f);
        }
        else if (clickj2 == 1 || clickj2 == -3)
        {
            clickj2 = 1;
            GameManager.instance.choixequipej2 = 1;
            choixj2.rectTransform.anchoredPosition = new Vector2(-420f, -37f);
        }
        else if (clickj2 == 2 || clickj2 == -2)
        {
            clickj2 = -2;
            GameManager.instance.choixequipej2 = 4;
            choixj2.rectTransform.anchoredPosition = new Vector2(480, -37);
        }

        else if (clickj2 == -1)
        {
            GameManager.instance.choixequipej2 = 3;
            choixj2.rectTransform.anchoredPosition = new Vector2(180, -37);
        }

        MajMaillot1();
        MajMaillot2();
        MajMaillot3();
        MajMaillot4();
    }

    private void MajMaillot1()
    {
        Debug.Log("Mise à jour du Maillot1");
        if ((GameManager.instance.choixequipej1 == 1) | (GameManager.instance.choixequipej2 == 1))
        {
            logo1.color = new Color(255, 255, 255, 1f);
            maillot1.color = new Color(255, 255, 255, 1f);
            text1.color = new Color(255, 255, 255, 1f);
        }
        else

        {
            logo1.color = new Color(255, 255, 255, 0.5f);
            maillot1.color = new Color(255, 255, 255, 0.5f);
            text1.color = new Color(255, 255, 255, 0.5f);
        }
    }

    private void MajMaillot2()
    {
        if ((GameManager.instance.choixequipej1 == 2) |
            (GameManager.twoPlayers && GameManager.instance.choixequipej2 == 2))
        {
            Debug.Log("Mise à jour du Maillot2");
            logo2.color = new Color(255, 255, 255, 1f);
            maillot2.color = new Color(255, 255, 255, 1f);
            text2.color = new Color(255, 255, 255, 1f);
        }
        else
        {
            logo2.color = new Color(255, 255, 255, 0.5f);
            maillot2.color = new Color(255, 255, 255, 0.5f);
            text2.color = new Color(255, 255, 255, 0.5f);
        }
    }

    private void MajMaillot3()
    {
        if ((GameManager.instance.choixequipej1 == 3) |
            (GameManager.twoPlayers && GameManager.instance.choixequipej2 == 3))
        {
            logo3.color = new Color(255, 255, 255, 1f);
            maillot3.color = new Color(255, 255, 255, 1f);
            text3.color = new Color(255, 255, 255, 1f);
        }
        else

        {
            logo3.color = new Color(255, 255, 255, 0.5f);
            maillot3.color = new Color(255, 255, 255, 0.5f);
            text3.color = new Color(255, 255, 255, 0.5f);
        }
    }

    private void MajMaillot4()
    {
        if ((GameManager.instance.choixequipej1 == 4) |
            (GameManager.twoPlayers && GameManager.instance.choixequipej2 == 4))
        {
            logo4.color = new Color(255, 255, 255, 1f);
            maillot4.color = new Color(255, 255, 255, 1f);
            text4.color = new Color(255, 255, 255, 1f);
        }
        else

        {
            logo4.color = new Color(255, 255, 255, 0.5f);
            maillot4.color = new Color(255, 255, 255, 0.5f);
            text4.color = new Color(255, 255, 255, 0.5f);
        }
    }
}