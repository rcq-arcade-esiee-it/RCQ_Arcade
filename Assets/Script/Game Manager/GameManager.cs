using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///     Class <c>GameManager</c> représentant la partie interaction entre les interfaces et les joueurs. Singleton qui
///     se détruit si on cherche à l'instancier une nouvelle fois.
/// </summary>
public class GameManager : MonoBehaviour
{
    // instance unique de l'objet GameManager
    public static GameManager instance;
    public static bool twoPlayers;

    public static GameInfo gameInfo;

    // prefabs utilisées pour le gameManager
    public GameObject faderObj;
    public Image faderImg;
    public float fadeSpeed = .02f;
    public int choixequipej1 = -100;
    public int choixequipej2 = -100;

    private readonly Color fadeTransparency = new(0, 0, 0, .04f);
    private AsyncOperation async;

    private bool idle;
    private bool isReturning;

    private DateTime time;
    private PlayerActions playerActions;
    
    // Getter et Setter retournant et créant nle nom de l'écran courant
    public string CurrentSceneName { get; set; }

    /// <summary>Cette méthode instancie une seule fois la classe à son activation </summary>
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = GetComponent<GameManager>();
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            twoPlayers = false;
            time = DateTime.Now;

            InputSystem.onAnyButtonPress.Call(control =>
                time = DateTime.Now
            );
            playerActions = new PlayerActions();
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.visible = true;
    }

    private void Update()
    {
        if (DateTime.Now.Minute - time.Minute >= 3)
        {
            LoadScene("MainScreen");
            DestroyImmediate(gameObject);
        }

        if (playerActions.UI.Shutdown.WasPressedThisFrame()) Shutdown.Main();
        if (playerActions.UI.ResetScore.WasPressedThisFrame()) PlayerScore.resetCurrentScore("Game1");

        
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.name;
    }

    public void LoadScene(string sceneName)
    {
        instance.StartCoroutine(Load(sceneName));
        ActivateScene();
    }

    public void LoadSceneWithParameters(string parameters)
    {
        gameInfo = GameInfo.CreateFromJSON(parameters);
        Debug.Log(gameInfo.Description);
        LoadScene(gameInfo.GameInfoScene);
    }

    private IEnumerator Load(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return async;
        isReturning = false;
    }

    private void OnEnable()
    {
playerActions.UI.Enable();    }

    private void OnDisable()
    {
        playerActions.UI.Disable();    }
    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }
}