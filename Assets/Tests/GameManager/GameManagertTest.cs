using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
/// <summary>Class <c>BGameManagerTest</c> representant une suite de tests devant êtres lancées dans un ordre précis.</summary>
public class BGameManagerTest
{
    // Variables d'environnement de test

    private GameManager gameManager;
    private string gameMenuScenePath;
    private LoadSceneParameters loadSceneParameters;
    private string mainGameScenePath;
    // Initialisation des composant prefab

    private GameObject gameManagerPrefab;

    /// <summary>Cette méthode met en place les différents prefabs et objets dont auront besoin les tests
    /// </summary>   
    [SetUp]
    public void Setup()
    {
        loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);

        var mainGameScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .mainGameScene;
        var gameMenuScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .gameMenuScene;
        mainGameScenePath = AssetDatabase.GetAssetPath(mainGameScene);
        gameMenuScenePath = AssetDatabase.GetAssetPath(gameMenuScene);
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();
    }

    [Test]
    public void _01_GameManagerPrefabExists()
    {
        Assert.NotNull(gameManagerPrefab);
    }


    [Test]
    public void _02_GameManagerPrefabHasRequiredComponentScript()
    {
        Assert.IsNotNull(gameManagerPrefab.GetComponent<GameManager>());
    }

    [UnityTest]
    public IEnumerator _04_GameManagerExistsInScene2()
    {
        yield return null;

        Assert.NotNull(Object.FindObjectOfType<GameManager>());
    }
}