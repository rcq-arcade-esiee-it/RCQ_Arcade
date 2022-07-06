using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
/// <summary>Class <c>DFirstGameManagerTest</c> representant une suite de tests devant êtres lancées dans un ordre précis.</summary>
public class EFirstGameManagerTest : InputTestFixture
{
    
    // Variables d'environnement de test
    private LoadSceneParameters loadSceneParameters;
    private string firstGameScenePath;
    private Keyboard keyboard;
    private GameManager gameManager;

    // Initialisation des composant prefab
    public GameObject bombPrefab;
    private GameObject firstGameManagerPrefab;
    private GameObject gameManagerPrefab;
    private GameObject mainCanvasPrefab;
    private GameObject playerPrefab;
    private GameObject player2Prefab;

    public GameObject rugbyBallPrefab;


    /// <summary>Cette méthode met en place les différents prefabs et objets dont auront besoin les tests
    /// </summary>   
    public override void Setup()
    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();

        loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None);
        var firstGameScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .firstGameScene;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();
        firstGameScenePath = AssetDatabase.GetAssetPath(firstGameScene);
        firstGameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().firstGameManagerPrefab;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        playerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().playerPrefab;
        player2Prefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().player2Prefab;
        rugbyBallPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().rugbyBallPrefab;

        bombPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().bombPrefab;
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().mainCanvasPrefab;
        

    }

    //Enleve tout objet de la scene
    private void ClearScene()
    {
        var balls = Object.FindObjectsOfType<BallController>();
        // var player = Object.FindObjectOfType<Player>();
        // Object.DestroyImmediate(player.transform.parent.gameObject);

        foreach (var obj in balls)

            if (obj != null)
                Object.DestroyImmediate(obj.transform.parent.gameObject);
        // Si l'objet a un parent 
    }


    [UnityTest]
    public IEnumerator _01_FirstGameManagerExistsInScene()
    {
        FirstGameManager.InitializeTestingEnvironment(false, false, false, true, false);

        EditorSceneManager.LoadSceneInPlayMode(firstGameScenePath, loadSceneParameters);

        yield return null;
        Assert.NotNull(firstGameManagerPrefab);

        Assert.NotNull(Object.FindObjectOfType<FirstGameManager>());
        Assert.IsNotNull(firstGameManagerPrefab.GetComponent<FirstGameManager>());
    }

    [UnityTest]
    public IEnumerator _02_FirstGameManagerCanSpawnPlayerOnLoad()
    {
        FirstGameManager.InitializeTestingEnvironment(false, false , false, true, false);

        Object.Instantiate(firstGameManagerPrefab).GetComponent<FirstGameManager>();
        FirstGameManager.InitializeTestingEnvironment(true, false, false, true, false);

        yield return null;

        var player = Object.FindObjectOfType<Player>();
        Assert.IsTrue(player != null);
    }

    [UnityTest]
    public IEnumerator _03_FirstGameManagerSpawnsBall()
    {
        Object.Instantiate(firstGameManagerPrefab);
        FirstGameManager.InitializeTestingEnvironment(false, true, true, true, false);

        yield return null;

        var balls = Object.FindObjectsOfType<BallController>();
        Assert.IsTrue(balls.Length > 0);
    }

    [UnityTest]
    public IEnumerator _04_FirstGameManagerScoreIsIncreasedAfterBallAreTuched()
    {
        ClearScene();
        Object.Instantiate(firstGameManagerPrefab);

        yield return null;

        var ball = Object.Instantiate(rugbyBallPrefab, Vector3.zero, Quaternion.identity)
            .GetComponent<BallController>();
        var score = FirstGameManager.instance.scorePlayer1;
        yield return new WaitForSeconds(2);

        Assert.IsTrue(score != FirstGameManager.instance.scorePlayer1);

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator _05_FirstGameManagerPlayerIsStauntAfterTouchedByBomb()
    {
        ClearScene();
        Object.Instantiate(firstGameManagerPrefab);

        yield return null;

        var bomb = Object.Instantiate(bombPrefab, Vector3.zero, Quaternion.identity)
            .GetComponent<BombController>();

        yield return new WaitForSeconds(3);

        Assert.IsFalse(FirstGameManager.instance.stauntPlayer1);

        Assert.IsTrue(FirstGameManager.instance.scorePlayer1 < 1);


        yield return null;
    }
    
    [UnityTest]
    public IEnumerator _06_FirstGameManagerPlayerWinIfScoreIs15()
    {
        LogAssert.ignoreFailingMessages = true;

        var player = Object.FindObjectOfType<Player>();
        Object.Instantiate(firstGameManagerPrefab);
        FirstGameManager.instance.time = 3;

        FirstGameManager.instance.scorePlayer1 = 20;
        yield return new WaitForSecondsRealtime(4);

        Assert.IsFalse(FirstGameManager.instance.stauntPlayer1);
        Assert.IsTrue(FirstGameManager.instance.partyFinished);

        Assert.IsTrue(player.Speed == 0);
        yield return new WaitForSecondsRealtime(4);


        yield return null;
    }
    [UnityTest]
    public IEnumerator _07_FirstGameManagerReturnLoseIfScoreIsLessThan15()
    {        
        LogAssert.ignoreFailingMessages = true;

        FirstGameManager.InitializeTestingEnvironment(false, true, true, true, false);
        
        Object.Instantiate(firstGameManagerPrefab);

        yield return new WaitForSecondsRealtime(1);

        EditorSceneManager.LoadSceneInPlayMode(firstGameScenePath, loadSceneParameters);
        yield return new WaitForSecondsRealtime(1);
        

        var player1 = Object.FindObjectOfType<Player1Controller>();

 
        FirstGameManager.instance.time = 3;
        
        FirstGameManager.instance.scorePlayer1 = 2;
        yield return new WaitForSecondsRealtime(4);

        Assert.IsFalse(FirstGameManager.instance.stauntPlayer1);

        Assert.IsTrue(FirstGameManager.instance.partyFinished);

        Assert.IsTrue(player1.Speed == 0);

        Assert.IsFalse(player1.isWinner);
        
    }
    
    [UnityTest]
    public IEnumerator _08_FirstGameManagerReturnWithTwoPlayers()
    {        
        LogAssert.ignoreFailingMessages = true;

        FirstGameManager.InitializeTestingEnvironment(false, true, true, true, true);
        
        Object.Instantiate(firstGameManagerPrefab);

        yield return new WaitForSecondsRealtime(1);

        EditorSceneManager.LoadSceneInPlayMode(firstGameScenePath, loadSceneParameters);
        yield return new WaitForSecondsRealtime(1);
        

        var player1 = Object.FindObjectOfType<Player1Controller>();
        var player2 = Object.FindObjectOfType<Player2Controller>();
        
        Assert.IsNotNull(player1);
        Assert.IsNotNull(player2);

    }
    
    [UnityTest]
    public IEnumerator _09_FirstGameManagerPlayer1WinIfScoreIsMoreThanPlayer2()
    {        
        LogAssert.ignoreFailingMessages = true;

        var player1 = Object.FindObjectOfType<Player1Controller>();
        var player2 = Object.FindObjectOfType<Player2Controller>();
        
        yield return new WaitForSecondsRealtime(1);
        
        var bomb = Object.Instantiate(bombPrefab, new Vector2(3230, 363), Quaternion.identity)
            .GetComponent<BombController>();

        yield return new WaitForSeconds(3);

        Assert.IsFalse(FirstGameManager.instance.stauntPlayer1);
        
        FirstGameManager.instance.time = 3;
        
        FirstGameManager.instance.scorePlayer1 = 20;
        yield return new WaitForSecondsRealtime(4);

     
        Assert.IsTrue(FirstGameManager.instance.partyFinished);
        
        Assert.IsTrue(player1.isWinner);
        Assert.IsTrue(!player2.isWinner);


    }
    
    [UnityTest]
    public IEnumerator _10_FirstGameManagerPlayer2WinIfScoreIsMoreThanPlayer1()
    {        
        LogAssert.ignoreFailingMessages = true;

        FirstGameManager.InitializeTestingEnvironment(false, true, true, true, true);
        
        Object.Instantiate(firstGameManagerPrefab);

        yield return new WaitForSecondsRealtime(1);

        EditorSceneManager.LoadSceneInPlayMode(firstGameScenePath, loadSceneParameters);
        yield return new WaitForSecondsRealtime(1);
        

        var player1 = Object.FindObjectOfType<Player1Controller>();
        var player2 = Object.FindObjectOfType<Player2Controller>();
        

    
        yield return new WaitForSeconds(3);

        
        FirstGameManager.instance.time = 3;
        
        FirstGameManager.instance.scorePlayer2 = 20;
        yield return new WaitForSecondsRealtime(4);

   
        Assert.IsTrue(FirstGameManager.instance.partyFinished);


        Assert.IsTrue(player2.isWinner);
        Assert.IsTrue(!player1.isWinner);

    }
}