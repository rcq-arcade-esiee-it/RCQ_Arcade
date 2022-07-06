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

    // Initialisation des composant prefab
    public GameObject bombPrefab;
    private GameObject firstGameManagerPrefab;
    private GameObject gameManagerPrefab;
    private GameObject mainCanvasPrefab;
    private GameObject playerPrefab;
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

        firstGameScenePath = AssetDatabase.GetAssetPath(firstGameScene);
        firstGameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().firstGameManagerPrefab;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        playerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().playerPrefab;
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
        FirstGameManager.InitializeTestingEnvironment(false, false, false, true);

        EditorSceneManager.LoadSceneInPlayMode(firstGameScenePath, loadSceneParameters);

        yield return null;
        Assert.NotNull(firstGameManagerPrefab);

        Assert.NotNull(Object.FindObjectOfType<FirstGameManager>());
        Assert.IsNotNull(firstGameManagerPrefab.GetComponent<FirstGameManager>());
    }

    [UnityTest]
    public IEnumerator _02_FirstGameManagerCanSpawnPlayerOnLoad()
    {
        FirstGameManager.InitializeTestingEnvironment(false, false , false, true);

        Object.Instantiate(firstGameManagerPrefab).GetComponent<FirstGameManager>();
        FirstGameManager.InitializeTestingEnvironment(true, false, false, true);

        yield return null;

        var player = Object.FindObjectOfType<Player>();
        Assert.IsTrue(player != null);
    }

    [UnityTest]
    public IEnumerator _03_FirstGameManagerSpawnsBall()
    {
        Object.Instantiate(firstGameManagerPrefab);
        FirstGameManager.InitializeTestingEnvironment(false, true, true, true);

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
        var score = FirstGameManager.score;
        yield return new WaitForSeconds(2);

        Assert.IsTrue(score != FirstGameManager.score);

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

        Assert.IsFalse(FirstGameManager.instance.staunt);

        Assert.IsTrue(FirstGameManager.score < 1);


        yield return null;
    }
    
    [UnityTest]
    public IEnumerator _06_FirstGameManagerPlayerWinIfScoreIs15()
    {
        ClearScene();
        var player = Object.FindObjectOfType<Player>();
        Object.Instantiate(firstGameManagerPrefab);
        FirstGameManager.instance.time = 3;

        FirstGameManager.score = 20;
        yield return new WaitForSecondsRealtime(4);

        Assert.IsFalse(FirstGameManager.instance.staunt);
        Assert.IsTrue(FirstGameManager.instance.partyFinished);

        Assert.IsTrue(player.speedAccess == 0);
        yield return new WaitForSecondsRealtime(4);


        yield return null;
    }
}