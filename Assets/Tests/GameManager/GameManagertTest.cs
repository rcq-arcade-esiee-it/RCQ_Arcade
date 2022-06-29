using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BGameManagerTest
{
    private GameManager gameManager;
    private GameObject gameManagerPrefab;
    private string gameMenuScenePath;
    private LoadSceneParameters loadSceneParameters;
    private string mainGameScenePath;

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