using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ESelectEquipeTest : InputTestFixture
{
    private GameManager gameManager;
    private GameObject gameManagerPrefab;
    private Keyboard keyboard;
    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;
    private string SelectEquipScenePath;

    public override void Setup()
    {
        base.Setup();

        var SelectEquipeScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .SelectEquipeScene;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();
        SelectEquipScenePath = AssetDatabase.GetAssetPath(SelectEquipeScene);
        keyboard = InputSystem.AddDevice<Keyboard>();
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().mainCanvasPrefab;
    }

    [UnityTest]
    public IEnumerator _01_OnSelect_Changer_Equipe_J1()
    {
        yield return new WaitForSeconds(2f);
        gameManager.LoadScene(SelectEquipScenePath);
        yield return new WaitForSeconds(2f);

        var maillot1 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_1").GetComponent<Image>();
        var maillot2 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_2").GetComponent<Image>();
        var maillot3 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_3").GetComponent<Image>();
        var maillot4 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_4").GetComponent<Image>();

        Press(keyboard.qKey);

        yield return new WaitForSecondsRealtime(2);
        Assert.That(GameManager.instance.choixequipej1 == 1);

        Assert.That(maillot1.color == new Color(255, 255, 255, 1f));
        Assert.That(maillot2.color == new Color(255, 255, 255, 0.5f));

        yield return new WaitForSecondsRealtime(2);

        Press(keyboard.dKey);
        yield return new WaitForSecondsRealtime(2);
        Assert.That(GameManager.instance.choixequipej1 == 2);

        Assert.That(maillot3.color == new Color(255, 255, 255, 0.5f));
        Assert.That(maillot2.color == new Color(255, 255, 255, 1f));
    }

    [UnityTest]
    public IEnumerator _03_OnSelect_Changer_Equipe_J2()
    {
        GameManager.twoPlayers = true;
        yield return new WaitForSeconds(2f);

        gameManager.LoadScene(SelectEquipScenePath);
        yield return new WaitForSeconds(2f);

        var maillot1 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_1").GetComponent<Image>();
        var maillot2 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_2").GetComponent<Image>();
        var maillot3 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_3").GetComponent<Image>();
        var maillot4 = GameObject.Find("MainCanvas/TeamsCanvas/Maillot_4").GetComponent<Image>();
        yield return new WaitForSeconds(2f);

        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 1);
        Assert.That(maillot1.color == new Color(255, 255, 255, 1f));
        Assert.That(maillot2.color == new Color(255, 255, 255, 1f));


        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 2);
        Assert.That(maillot3.color == new Color(255, 255, 255, 0.5f));
        Assert.That(maillot2.color == new Color(255, 255, 255, 1f));

        GameManager.twoPlayers = false;
    }

    [UnityTest]
    public IEnumerator _02_EnterKey_devrait_charger_ecran_suivant()
    {
        LogAssert.ignoreFailingMessages = true;
        gameManager.LoadScene(SelectEquipScenePath);
        yield return new WaitForSecondsRealtime(2);

        Press(keyboard.enterKey);
        yield return new WaitForSecondsRealtime(2);
        Assert.That(SceneManager.GetActiveScene().name == "Game1");
    }
}