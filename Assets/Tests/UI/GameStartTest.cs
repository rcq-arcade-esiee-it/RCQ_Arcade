using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/// <summary>Class <c>AGameFirstStartTest</c> representant une suite de tests devant êtres lancées dans un ordre précis.</summary>
public class AGameFirstStartTest : InputTestFixture
{
    // Variables d'environnement de test
    private TextMeshProUGUI _textMeshProUGUI;
    private Keyboard keyboard;

    private LoadSceneParameters loadSceneParameters;
    // Initialisation des composant prefab

    private GameObject mainCanvasPrefab;
    private string mainScreenScenePath;

    /// <summary>
    ///     Cette méthode met en place les différents prefabs et objets dont auront besoin les tests
    /// </summary>
    public override void Setup()
    {
        base.Setup();
        MainGameScript.testing = true;
        keyboard = InputSystem.AddDevice<Keyboard>();

        loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None);

        var mainScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>().mainGameScene;
        mainScreenScenePath = AssetDatabase.GetAssetPath(mainScene);
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().mainCanvasPrefab;
    }

    [UnityTest]
    public IEnumerator _00_MainCanvasExistsInScene()
    {
        GameManager.gameInfo = null;

        EditorSceneManager.LoadSceneInPlayMode(mainScreenScenePath, loadSceneParameters);

        yield return null;

        Assert.NotNull(Object.FindObjectOfType<Canvas>());
    }

    [UnityTest]
    public IEnumerator _01_TestGameStart_texte_devrait_clignoter()
    {
        _textMeshProUGUI = GameObject.Find("MainCanvas/TextTypeStart").GetComponent<TextMeshProUGUI>();
        var firstColor = _textMeshProUGUI.color;
        yield return new WaitForSeconds(2f);
        Assert.That(firstColor, Is.Not.EqualTo(_textMeshProUGUI.color));
    }

    [UnityTest]
    public IEnumerator _03_GameManagerNotExistsInScene1()
    {
        yield return null;

        Assert.Null(Object.FindObjectOfType<GameManager>());
    }


    [UnityTest]
    public IEnumerator _03_TestGameStart_devrait_changer_decran_touche()
    {
        LogAssert.ignoreFailingMessages = true;

        var sceneName = SceneManager.GetActiveScene().name;

        Assert.That(sceneName, Is.EqualTo("MainScreen"));

        // Cette fonction simule un joueur appuyant sur la touche a de son clavier, a utiliser dans une méthode IEnumerator, et suivit d'un Yield return new WaitForSeconds()
        Press(keyboard.aKey);

        yield return new WaitForSeconds(2f);

        sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("GameMenu"));
        MainGameScript.testing = false;
    }
}