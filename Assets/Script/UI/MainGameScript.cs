using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainGameScript : MonoBehaviour
{
    private AudioSource audioWhistle;

    // Start is called before the first frame update
    private double timer;

    // Start is called before the first frame update
    private void Start()
    {
        // InputSystem.onAnyButtonPress
        //     .CallOnce(ctrl => Invoke("ChangeMenu", 1));
        audioWhistle = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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

    private void ChangeMenu()
    {
        SceneManager.LoadScene("Scenes/GameMenu", LoadSceneMode.Single);
    }
}