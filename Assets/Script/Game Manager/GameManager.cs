using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    private AsyncOperation async;
    private bool isReturning;

    public string CurrentSceneName { get; private set; }

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
    }

    private void Start()
    {
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.name;
        //instance.StartCoroutine(FadeIn(instance.faderObj, instance.faderImg));
    }


    public void LoadScene(string sceneName)
    {
        instance.StartCoroutine(Load(sceneName));
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    // Begin loading a scene with a specified string asynchronously
    private IEnumerator Load(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return async;
        isReturning = false;
    }

    // Allows the scene to change once it is loaded
    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
        // Quit the application
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        EditorApplication.isPlaying = false;
#endif
    }

    public void ReturnToMenu()
    {
        if (isReturning) return;

        if (CurrentSceneName != "MenuGame")
        {
            StopAllCoroutines();
            LoadScene("MenuGame");
            isReturning = true;
        }
    }
}