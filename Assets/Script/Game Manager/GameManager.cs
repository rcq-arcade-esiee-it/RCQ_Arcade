using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject faderObj;
    public Image faderImg;
    public float fadeSpeed = .02f;


    private readonly Color fadeTransparency = new(0, 0, 0, .04f);
    private AsyncOperation async;

    private bool isReturning;

    // TEst>
    // Get the current scene name
    public string CurrentSceneName { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = GetComponent<GameManager>();
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.visible = true;
    }

    private void Start()
    {
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.name;
        instance.StartCoroutine(FadeIn(instance.faderObj, instance.faderImg));
    }

    public void LoadScene(string sceneName)
    {
        instance.StartCoroutine(Load(sceneName));
        instance.StartCoroutine(FadeOut(instance.faderObj, instance.faderImg));
    }

    // public void ReloadScene()
    // {
    //     LoadScene(SceneManager.GetActiveScene().name);
    // }

    // Begin loading a scene with a specified string asynchronously
    private IEnumerator Load(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return async;
        isReturning = false;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    private IEnumerator FadeOut(GameObject faderObject, Image fader)
    {
        faderObject.SetActive(true);
        while (fader.color.a < 1)
        {
            fader.color += fadeTransparency;
            yield return new WaitForSeconds(fadeSpeed);
        }

        ActivateScene(); //Activate the scene when the fade ends
    }

    private IEnumerator FadeIn(GameObject faderObject, Image fader)
    {
        while (fader.color.a > 0)
        {
            fader.color -= fadeTransparency;
            yield return new WaitForSeconds(fadeSpeed);
        }

        faderObject.SetActive(false);
    }

//     public void ExitGame()
//     {
//         // If we are running in a standalone build of the game
// #if UNITY_STANDALONE
//         // Quit the application
//         Application.Quit();
// #endif
//
//         // If we are running in the editor
// #if UNITY_EDITOR
//         // Stop playing the scene
//         EditorApplication.isPlaying = false;
// #endif
//     }

    // public void ReturnToMenu()
    // {
    //     if (isReturning) return;
    //
    //     if (CurrentSceneName != "MenuGame")
    //     {
    //         StopAllCoroutines();
    //         LoadScene("MenuGame");
    //         isReturning = true;
    //     }
    // }
    //
    // public void LoadSceneWithParameters(string sceneName, string jsonString)
    // {
    //     // ENvoyer un JSON
    //     instance.StartCoroutine(Load(sceneName));
    //     instance.StartCoroutine(FadeOut(instance.faderObj, instance.faderImg));
    // }
}