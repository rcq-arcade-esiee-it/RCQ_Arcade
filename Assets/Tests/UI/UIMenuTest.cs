using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CUIMenuTest : InputTestFixture
{
    private string firstGameScenePath;

    private GameManager gameManager;
    private GameObject gameManagerPrefab;
    private string gameMenuScenePath;
    private Keyboard keyboard;

    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;

    public override void Setup()

    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();

        loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);

        var gameMenuScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .gameMenuScene;
        var firstGameScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .mainGameScene;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();
        firstGameScenePath = AssetDatabase.GetAssetPath(firstGameScene);
        gameMenuScenePath = AssetDatabase.GetAssetPath(gameMenuScene);
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().mainCanvasPrefab;
    }

    [UnityTest]
    public IEnumerator _01_OnSelect_devrait_changer_bouton_selectionne()
    {
        gameManager.LoadScene(gameMenuScenePath);
        yield return new WaitForSeconds(2f);

        Press(keyboard.enterKey);
        yield return new WaitForSecondsRealtime(2);

        // gameManager.LoadScene(firstGameScenePath);
        yield return new WaitForSeconds(1f);
        Debug.Log(gameManager.CurrentSceneName);
        Assert.That(SceneManager.GetActiveScene().name
            , Is.EqualTo("Game1"));
    }

    [UnityTest]
    public IEnumerator _02_OnSelect_devrait_changer_bouton_selectionne()
    {
        gameManager.LoadScene(gameMenuScenePath);
        yield return new WaitForSecondsRealtime(3);
        // Given
        Color selectedColor = new Color32(192, 44, 44, 255);
        Color deSelectedColorPanel = new Color32(255, 255, 255, 255);

        // first button
        var button1 = GameObject.Find("MainCanvas/PANEL_BOUTON_1/JEU_1").GetComponent<RectTransform>();
        var textButton1 = GameObject.Find("MainCanvas/PANEL_BOUTON_1/JEU_1").GetComponentInChildren<TextMeshProUGUI>();
        // second button
        var button2 = GameObject.Find("MainCanvas/PANEL_BOUTON_2/JEU_2").GetComponent<RectTransform>();
        var textButton2 = GameObject.Find("MainCanvas/PANEL_BOUTON_2/JEU_2").GetComponentInChildren<TextMeshProUGUI>();
        // third button
        var button3 = GameObject.Find("MainCanvas/PANEL_BOUTON_3/JEU_3").GetComponent<RectTransform>();
        var textButton3 = GameObject.Find("MainCanvas/PANEL_BOUTON_3/JEU_3").GetComponentInChildren<TextMeshProUGUI>();
        // fourth button
        var button4 = GameObject.Find("MainCanvas/PANEL_BOUTON_4/JEU_4").GetComponent<RectTransform>();
        var textButton4 = GameObject.Find("MainCanvas/PANEL_BOUTON_4/JEU_4").GetComponentInChildren<TextMeshProUGUI>();

        //When THen

        Assert.That(textButton1.color, Is.EqualTo(selectedColor));
        Assert.That(button1.sizeDelta, Is.EqualTo(new Vector2(268, 238)));

        Press(keyboard.rightArrowKey);
        yield return new WaitForSecondsRealtime(2);

        Assert.That(textButton1.color, Is.Not.EqualTo(selectedColor));
        Assert.That(textButton2.color, Is.EqualTo(selectedColor));
        Assert.That(button2.sizeDelta, Is.EqualTo(new Vector2(268, 238)));

        yield return new WaitForSeconds(2f);

        Press(keyboard.downArrowKey);
        yield return new WaitForSecondsRealtime(2);

        Assert.That(textButton2.color, Is.Not.EqualTo(selectedColor));
        Assert.That(textButton4.color, Is.EqualTo(selectedColor));
        Assert.That(button4.sizeDelta, Is.EqualTo(new Vector2(268, 238)));
    }
}