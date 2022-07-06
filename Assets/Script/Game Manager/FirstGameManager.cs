using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>Class <c>FirstGameManager</c> représentant une partie du premier jeu. Singleton qui se détruit si on cherche à l'instancier une nouvelle fois.</summary>

public class FirstGameManager : MonoBehaviour

{
    // instance unique de l'objet FirstGameManager
    public static FirstGameManager instance;
    
    // prefabs utilisées dans une premiere partie
    public GameObject playerPrefab;
    public GameObject gameManagerPrefab;
    public GameObject rugbyBallPrefab;
    public GameObject bombPrefab;
    public GameObject mainCanvasPrefab;

    // variables pour effectuer des tests
    public static bool startEnabled = true;
    public static bool updateEnabled = true;
    public static bool ballsEnabled = true;
    public static bool testEnabled;


    // variables liées à une partie

    public static Player player;
    public static int score;
    public bool partyFinished;
    public bool staunt;
    public float time;
    [HideInInspector] public float ballSpawnDelay = 1.0f;
    [HideInInspector] public float ballSpawnTimer;

  
    
    private GameManager gameManager;

    /// <summary>Cette méthode instancie au lencement d'une partie les joueurs
    /// </summary>
    private void Start()
    {
        gameManager = Instantiate(gameManagerPrefab).GetComponent<GameManager>();

        if (startEnabled)
            player = Instantiate(playerPrefab, new Vector2(-3230, 363), Quaternion.identity)
                .GetComponentInChildren<Player>();
        else
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity)
                .GetComponentInChildren<Player>();
        partyFinished = false;
        staunt = false;
    }

    /// <summary>Cette méthode représente le déroulement du jeu </summary>
    private void Update()
    {
        if (!partyFinished)
        {
            if (time <= 25) rugbyBallPrefab.GetComponentInChildren<Rigidbody2D>().gravityScale = 900000000000;
            UpdateTimers();
            if (ballSpawnTimer <= 0.0f && ballsEnabled && !rugbyBallPrefab.scene.IsValid()) SpawnRugbyBall();
            if (Random.Range(1, 2500) < 2) SpawnBomb();
        }
        else
        {
            StartCoroutine(PartyEnd());
        }
    }
    /// <summary>Cette méthode définie le gagnant de la partie </summary>

    private IEnumerator PartyEnd()
    {
        
        String text = "MEILLEUR JOUEUR";
        GameObject textObj = GameObject.Find("BestScore");

        if (score > 15)
        {
            textObj.SetActive(true);
            textObj.GetComponent<TextMeshProUGUI>().text = "BRAVO !";
            player.isWinner = true;
            yield return new WaitForSecondsRealtime(3);
        }
        else
        {
            player.isWinner = false;
            textObj.SetActive(true);
            textObj.GetComponent<TextMeshProUGUI>().text = "DOMMAGE !";
            yield return new WaitForSecondsRealtime(3);

        }
        GameManager.instance.LoadScene("Aff_Score");

    }
    /// <summary>Cette méthode instancie une seule fois la classe à son activation </summary>
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Time.timeScale = 1.0f;
        score = 0;
    }
    /// <summary>Cette méthode permet d'initialiser plusieurs paramètres de tests </summary>

    public static void InitializeTestingEnvironment(bool start, bool update, bool balls, bool test)
    {
        startEnabled = start;
        updateEnabled = update;
        ballsEnabled = balls;
        testEnabled = test;
    }

    /// <summary>Cette méthode met a jour l'instanciation des balles </summary>
    private void UpdateTimers()
    {
        if (ballSpawnTimer > 0.0f)
            ballSpawnTimer -= Time.deltaTime;
    }
    /// <summary>Cette méthode instancie des balles aléatoirement sur le terrain </summary>

    public void SpawnRugbyBall()
    {
        BallController ball;
        if (Camera.main != null)
        {
            // Position calculé au hasard par rapport au positionnement de la caméra
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(
                Random.Range(-0f, 1f),
                1f));
            ball = Instantiate(rugbyBallPrefab, spawnPosition
                ,
                Quaternion.identity).GetComponentInChildren<BallController>();
        }

        else
        {
            ball = Instantiate(rugbyBallPrefab, Vector2.zero, Quaternion.identity).GetComponent<BallController>();
        }


        ballSpawnTimer = ballSpawnDelay;
    }
    /// <summary>Cette méthode instancie des bombes aléatoirement sur le terrain </summary>

    public void SpawnBomb()
    {
        BombController bomb;

        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(
            Random.Range(-0f, 1f),
            1f));
        if (Camera.main != null)
            bomb = Instantiate(bombPrefab, spawnPosition
                ,
                Quaternion.identity).GetComponentInChildren<BombController>();

        else
            bomb = Instantiate(bombPrefab, Vector2.zero, Quaternion.identity).GetComponent<BombController>();


        ballSpawnTimer = ballSpawnDelay;
    }
}