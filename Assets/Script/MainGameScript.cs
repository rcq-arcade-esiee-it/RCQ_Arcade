
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    private double timer;
    AudioSource audioWhistle;

    // Start is called before the first frame update
    void Start()
    {
        audioWhistle = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime * 0.75;
        // Clignotement text ( a améliorer)
        if (timer>= 0.5)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(46,124,51,255);

        } if (timer >=1)
        {
            GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,0);
            timer = 0;
        }
        
        if (Input.anyKey)
        {
            if (!audioWhistle.isPlaying)
            {
                audioWhistle.Play();
            }       
            Invoke("ChangeMenu", 1);//this will happen after 2 seconds

        }
    }
    
    void ChangeMenu()
    {
        SceneManager.LoadScene("Scenes/GameMenu", LoadSceneMode.Single);

    }
}
