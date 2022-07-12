using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
///     Class <c>FirstGameManager</c> représentant une partie du premier jeu. Singleton qui se détruit si on cherche à
///     l'instancier une nouvelle fois.
/// </summary>
public class FirstGameManager : MonoBehaviour
{
    // instance unique de l'objet FirstGameManager
    public static FirstGameManager instance;

    // variables pour effectuer des tests
    public static bool startEnabled = true;
    public static bool updateEnabled = true;
    public static bool ballsEnabled = true;
    public static bool testEnabled;
    public static bool twoPlayerEnabled;


    // variables liées à une partie

    public static Player player;
    public static Player2Controller player2;

    public GameObject gameManagerPrefab;
    public GameObject rugbyBallPrefab;
    public GameObject bombPrefab;
    public GameObject mainCanvasPrefab;
    public GameObject GoldBall;

    // prefabs utilisées dans une premiere partie
    public GameObject playerPrefab;
    public GameObject player2Prefab;


    public int scorePlayer1;
    public int scorePlayer2;

    public bool partyFinished;
    public bool stauntPlayer1;
    public bool stauntPlayer2;

    public float time;
    [HideInInspector] public float ballSpawnDelay = 1.0f;
    [HideInInspector] public float ballSpawnTimer;
    private int randomvar;


    /// <summary>
    ///     Cette méthode instancie au lencement d'une partie les joueurs
    /// </summary>
    private void Start()
    {
        if (startEnabled)
        {
            player = Instantiate(playerPrefab, new Vector2(-3230, 363), Quaternion.identity)
                .GetComponentInChildren<Player>();

            if (GameManager.twoPlayers)
                player2 = Instantiate(player2Prefab, new Vector2(3230, 363), Quaternion.identity)
                    .GetComponentInChildren<Player2Controller>();
        }
        else
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity)
                .GetComponentInChildren<Player>();
            if (twoPlayerEnabled)
                player2 = Instantiate(player2Prefab, new Vector2(3230, 363), Quaternion.identity)
                    .GetComponentInChildren<Player2Controller>();
        }

        partyFinished = false;
        stauntPlayer1 = false;
        stauntPlayer2 = false;
    }

    /// <summary>Cette méthode représente le déroulement du jeu </summary>
    private void Update()
    {
        if (!partyFinished)
        {
            if (time <= 25) rugbyBallPrefab.GetComponentInChildren<Rigidbody2D>().gravityScale = 900000000000;
            UpdateTimers();
            if (ballSpawnTimer <= 0.0f && ballsEnabled && !rugbyBallPrefab.scene.IsValid())
            {
                // on fait un test sur 100. Si le resultat est supérieur à 10, on fait apparaitre une balle normal, sinon c'est une balle dorée qui donne + 10Points
                randomvar = Random.Range(0, 100);
                if (randomvar >= 10)
                    SpawnRugbyBall();
                else
                    SpawnGoldBall();
                Debug.Log(randomvar);
            }

            if (Random.Range(1, 2500) < 15) SpawnBomb();
        }
        else
        {
            StartCoroutine(PartyEnd());
        }
    }

    /// <summary>Cette méthode instancie une seule fois la classe à son activation </summary>
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Time.timeScale = 1.0f;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
    }

    /// <summary>Cette méthode définie le gagnant de la partie </summary>
    private IEnumerator PartyEnd()
    {
        rugbyBallPrefab.GetComponentInChildren<Rigidbody2D>().gravityScale = 500;

        var text = "MEILLEUR JOUEUR";
        var textObj = GameObject.Find("BestScore");
        if (!GameManager.twoPlayers && !twoPlayerEnabled)
        {
            // Fin de partie un joueur
            if (scorePlayer1 > 15)
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
                textObj.GetComponent<TextMeshProUGUI>().text = "TU AS PERDU !";
                yield return new WaitForSecondsRealtime(3);
            }

            PlayerScore.Score1 = scorePlayer1;
        }
        else
        {
            // Fin de partie deux joueurs

            if (scorePlayer1 > scorePlayer2)
            {
                textObj.SetActive(true);
                textObj.GetComponent<TextMeshProUGUI>().text = "BRAVO JOUEUR I";
                player.isWinner = true;
                yield return new WaitForSecondsRealtime(3);
            }
            else
            {
                player2.isWinner = true;
                textObj.SetActive(true);
                textObj.GetComponent<TextMeshProUGUI>().text = "BRAVO JOUEUR II";
                yield return new WaitForSecondsRealtime(3);
            }

            PlayerScore.Score1 = scorePlayer1;
            PlayerScore.Score2 = scorePlayer2;
            Debug.Log(PlayerScore.Score2 = scorePlayer2
            );
        }

        var fileName = Application.dataPath + "/Resources/Saves/" + "score_" + GameManager.gameInfo.GameScene + ".txt";
        // Si le fichier n'existe pas, il est crée
        using (var sr = new StreamReader(fileName))
        {
            var idx = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
                // Si le score de l'un des joueurs est inférieur aux score dans le fichier, alors l'écran Score ne s'affiche pas
                if (line.Length > 0 && idx <= 5)
                {
                    ++idx;
                    if (PlayerScore.Score1 > short.Parse(line.Split(" ")[1]) ||
                        (GameManager.twoPlayers && PlayerScore.Score2 > short.Parse(line.Split(" ")[1])))
                    {
                        GameManager.instance.LoadScene("Score");
                        DestroyImmediate(gameObject);
                    }
                }
        }

        GameManager.instance.LoadScene("Aff_Score");
        DestroyImmediate(gameObject);
    }

    /// <summary>Cette méthode permet d'initialiser plusieurs paramètres de tests </summary>
    public static void InitializeTestingEnvironment(bool start, bool update, bool balls, bool test, bool twoPlayer)
    {
        startEnabled = start;
        updateEnabled = update;
        ballsEnabled = balls;
        testEnabled = test;
        twoPlayerEnabled = twoPlayer;
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

    public void SpawnGoldBall()
    {
        BallController ball;
        if (Camera.main != null)
        {
            // Position calculé au hasard par rapport au positionnement de la caméra
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(
                Random.Range(-0f, 1f),
                1f));
            ball = Instantiate(GoldBall, spawnPosition
                ,
                Quaternion.identity).GetComponentInChildren<BallController>();
        }

        else
        {
            ball = Instantiate(GoldBall, Vector2.zero, Quaternion.identity).GetComponent<BallController>();
        }


        ballSpawnTimer = ballSpawnDelay;
    }

    /// <summary>Cette méthode instancie des bombes aléatoirement sur le terrain </summary>
    public void SpawnBomb()
    {
        FootballController bomb;

        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(
            Random.Range(-0f, 1f),
            1f));
        if (Camera.main != null)
            bomb = Instantiate(bombPrefab, spawnPosition
                ,
                Quaternion.identity).GetComponentInChildren<FootballController>();

        else
            bomb = Instantiate(bombPrefab, Vector2.zero, Quaternion.identity).GetComponent<FootballController>();


        ballSpawnTimer = ballSpawnDelay;
    }
}