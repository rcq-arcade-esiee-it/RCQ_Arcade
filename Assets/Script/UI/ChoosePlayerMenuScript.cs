// On Charge les différentes lib
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ChoosePlayerMenuScript : MonoBehaviour
{
    //On crée des variables Sprite et Image pour gérer l'affichage 
    public Image P1_image; // c'est l'entité "Joueur 1"
    public Image P2_image; // c'est l'entité "Joueur 2"
    public Sprite P1_1; //Image de joueur 1 valide
    public Sprite P1_2; // image de joueur 1 non valide
    public Sprite P2_1; // image de joueur 2 valide
    public Sprite P2_2; // image de joueur 2 non valide
    private AudioSource audio;

    void Start ()
    {
    }
    void Update () {
        if (Keyboard.current.aKey.wasPressedThisFrame) // si la touche "Q" (clavier qwerty) est appuyé, ça active le mode 1 joueur
        {
            GameManager.ModeDeuxJoueur = false; 
            Debug.Log("Mode 1 joueur");
            P1_image.sprite = P1_1; // changement des images afficher pour savoir visuellement l'état du mode un joueur ou deux joueur
            P2_image.sprite = P2_2;
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame) // si la touche "d" (clavier qwerty) est appuyé, ça active le mode 2 joueur
        {
            GameManager.ModeDeuxJoueur = true;
            P1_image.sprite = P1_2;
            P2_image.sprite = P2_1;
            Debug.Log("Mode 2 joueurs");
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame) // On valide avec la touche "entré" et on passe à la scène suivante
        {
            GameManager.instance.LoadScene("TeamChoose");
            Debug.Log("Validé");
        }
        
        else if (Keyboard.current.backspaceKey.wasPressedThisFrame) // si on veut changer de jeu, on retourne sur l'écran de choix dde jeu
        {
            GameManager.instance.LoadScene("GameMenu");
            Debug.Log("Validé");
        }
    }
}