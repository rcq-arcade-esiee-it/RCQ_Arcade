using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Class <c>GameManager</c> représentant la partie interaction entre les interfaces et les joueurs. Singleton qui se détruit si on cherche à l'instancier une nouvelle fois.</summary>
public class GameManager : MonoBehaviour
{
    // instance unique de l'objet GameManager
    public static GameManager instance;
    // prefabs utilisées pour le gameManager
    public GameObject faderObj;
    public Image faderImg;
    public float fadeSpeed = .02f;
    private readonly Color fadeTransparency = new(0, 0, 0, .04f);
    private AsyncOperation async;
    private bool isReturning; 

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
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.visible = true;
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
    
}