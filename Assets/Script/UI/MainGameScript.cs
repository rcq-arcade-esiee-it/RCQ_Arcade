using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>Class <c>MainGameScript</c> représentant la partie interaction avec le premier écran.</summary>
public class MainGameScript : MonoBehaviour
{
    
    private AudioSource audioWhistle;
    private double timer;

    // Start is called before the first frame update
    private void Start()
    {
        audioWhistle = GetComponent<AudioSource>();
    }

    /// <summary>Cette méthode fais clignoter le texte et lance un audio lors du clique d'une touche </summary>
    private void Update()
    {
        blinkText();

        if (Keyboard.current.anyKey.wasPressedThisFrame
           )
        {
            if (!audioWhistle.isPlaying) audioWhistle.Play();
            Invoke("ChangeMenu", 1);
        }
    }

    private void blinkText()
    {
        timer = timer + Time.deltaTime * 0.75;
        // Clignotement text ( a améliorer)
        if (timer >= 0.5) GetComponent<TextMeshProUGUI>().color = new Color32(46, 124, 51, 255);
        if (timer >= 1)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
            timer = 0;
        }
    }
    /// <summary>Cette méthode permet  de changer de menu </summary>

    private void ChangeMenu()
    {
        SceneManager.LoadScene("Scenes/GameMenu", LoadSceneMode.Single);
    }
}