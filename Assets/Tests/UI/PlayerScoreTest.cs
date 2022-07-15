using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GPlayerScoreITest : InputTestFixture
{
    private GameManager gameManager;

    private GameObject gameManagerPrefab;
    private Keyboard keyboard;
    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;
    private string ScoreNamePath;
    private string ScoreViewPath;

    public override void Setup()
    {
        base.Setup();
        var scoreNameScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .scoreNameScene;
        var scoreViewScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .scoreViewScene;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();

        ScoreNamePath = AssetDatabase.GetAssetPath(scoreNameScene);
        ScoreViewPath = AssetDatabase.GetAssetPath(scoreViewScene);

        keyboard = InputSystem.AddDevice<Keyboard>();
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().mainCanvasPrefab;
    }

    [UnityTest]
    public IEnumerator _01_ScoreNameShoudBeInitailiseToAAAOnePlayer()
    {
        GameManager.twoPlayers = false;
        yield return new WaitForSeconds(2f);

        gameManager.LoadScene(ScoreNamePath);
        yield return new WaitForSeconds(2f);

        Assert.IsFalse(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas").activeSelf);
        ;
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/OnePlayerGameCanvas").activeSelf);
        ;

        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/OnePlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[1].text == "A");
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/OnePlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[2].text == "A");
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/OnePlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[3].text == "A");
        ;

        Press(keyboard.enterKey);
        yield return new WaitForSeconds(2f);
    }

    [UnityTest]
    public IEnumerator _02_ScoreNameShoudBeInitailiseToAAAAndTwoPlayer()
    {
        GameManager.twoPlayers = true;
        yield return new WaitForSeconds(2f);

        gameManager.LoadScene(ScoreNamePath);
        yield return new WaitForSeconds(2f);

        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas").activeSelf);
        ;
        Assert.IsFalse(GameObject.Find("MainCanvas/ScoreCanvas/OnePlayerGameCanvas").activeSelf);
        ;

        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[1].text == "A");
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[2].text == "A");
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player1")
            .GetComponentsInChildren<TextMeshProUGUI>()[3].text == "A");
        ;


        yield return new WaitForSeconds(2f);
    }

    [UnityTest]
    public IEnumerator _03_ScoreNameChangeFirstLetterIfKeyDownOrUpPressed()
    {
        yield return new WaitForSeconds(2f);

        gameManager.LoadScene(ScoreNamePath);


        yield return new WaitForSeconds(2f);
        Press(keyboard.upArrowKey);
        yield return new WaitForSeconds(2f);

        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2")
            .GetComponentsInChildren<TextMeshProUGUI>()[1].text == "Z");

        yield return new WaitForSeconds(2f);
        Press(keyboard.downArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2")
            .GetComponentsInChildren<TextMeshProUGUI>()[1].text == "A");


        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2")
            .GetComponentsInChildren<TextMeshProUGUI>()[2].text == "A");
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2")
            .GetComponentsInChildren<TextMeshProUGUI>()[3].text == "A");
        ;

        yield return new WaitForSeconds(2f);
    }

    [UnityTest]
    public IEnumerator _04_ScoreNameChangeLetterCaseIfLeftOrRightPressed()
    {
        yield return new WaitForSeconds(2f);

        gameManager.LoadScene(ScoreNamePath);


        yield return new WaitForSeconds(2f);
        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(2f);

        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2/ThirdLetterPlayer")
            .GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder == 1);
        Assert.IsTrue(GameObject.Find("MainCanvas/ScoreCanvas/TwoPlayerGameCanvas/Player2/FirstLetterPlayer")
            .GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder == 0);


        yield return new WaitForSeconds(2f);
    }

    [UnityTest]
    public IEnumerator _05_ScorePrintPlayer1AndPlayer2NamesAndScoresAndGOToGAmeMenu()
    {
        gameManager.LoadScene(ScoreViewPath);

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(GameObject.Find("MainCanvas/txt_aff_score").GetComponent<TextMeshProUGUI>().text.Length > 0);

        Press(keyboard.enterKey);
        yield return new WaitForSeconds(2f);

        Assert.IsTrue(GameManager.twoPlayers == false);
        var sceneName = SceneManager.GetActiveScene().name;

        Assert.That(sceneName, Is.EqualTo("GameMenu"));

        GameManager.gameInfo = null;
        GameManager.InitializeTestingEnvironment(true);

    }
}