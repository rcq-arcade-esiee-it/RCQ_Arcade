using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ESelectEquipeTest : InputTestFixture
{
    private Keyboard keyboard;
    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;
    private string SelectEquipScenePath;
    private GameObject gameManagerPrefab;
    private GameManager gameManager;

    public override void Setup()
    {
        LogAssert.ignoreFailingMessages = true;
        loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
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
        EditorSceneManager.LoadSceneInPlayMode(SelectEquipScenePath, loadSceneParameters);
        Debug.Log("file:"+SelectEquipScenePath);

    }

    [UnityTest]
    public IEnumerator _01_OnSelect_Changer_Equipe_J1()
    {
        LogAssert.ignoreFailingMessages = true;
        gameManager.LoadScene(SelectEquipScenePath);
        var maillot1 = GameObject.Find("MainCanvas/Maillot_1").GetComponent<Image>();
        var maillot2 = GameObject.Find("MainCanvas/Maillot_2").GetComponent<Image>();
        var maillot3 = GameObject.Find("MainCanvas/Maillot_3").GetComponent<Image>();
        var maillot4 = GameObject.Find("MainCanvas/Maillot_4").GetComponent<Image>();

        Press(keyboard.leftArrowKey);
        yield return new WaitForSecondsRealtime(2);
        Assert.That(GameManager.instance.choixequipej1 == 1);
        
        Assert.That(maillot1.color == new Color(255,255,255,1f));
        Assert.That(maillot2.color == new Color(255,255,255,0.5f));

        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 4);
        
        Assert.That(maillot4.color == new Color(255,255,255,1f));
        Assert.That(maillot1.color == new Color(255,255,255,0.5f));

        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 3);

        Assert.That(maillot3.color == new Color(255,255,255,1f));
        Assert.That(maillot4.color == new Color(255,255,255,0.5f));
        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 2);

        Assert.That(maillot2.color == new Color(255,255,255,1f));
        Assert.That(maillot3.color == new Color(255,255,255,0.5f));


        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 3);
        
        Assert.That(maillot3.color == new Color(255,255,255,1f));
        Assert.That(maillot2.color == new Color(255,255,255,0.5f));

        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 4);
        
        Assert.That(maillot4.color == new Color(255,255,255,1f));
        Assert.That(maillot3.color == new Color(255,255,255,0.5f));
        

        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 1);
        
        Assert.That(maillot1.color == new Color(255,255,255,1f));
        Assert.That(maillot4.color == new Color(255,255,255,0.5f));

        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej1 == 2);
        
        Assert.That(maillot2.color == new Color(255,255,255,1f));
        Assert.That(maillot1.color == new Color(255,255,255,0.5f));
    }

    [UnityTest]
    public IEnumerator _02_OnSelect_Changer_Equipe_J2()
    {
        LogAssert.ignoreFailingMessages = true;
        gameManager.LoadScene(SelectEquipScenePath);
        var maillot1 = GameObject.Find("MainCanvas/Maillot_1").GetComponent<Image>();
        var maillot2 = GameObject.Find("MainCanvas/Maillot_2").GetComponent<Image>();
        var maillot3 = GameObject.Find("MainCanvas/Maillot_3").GetComponent<Image>();
        var maillot4 = GameObject.Find("MainCanvas/Maillot_4").GetComponent<Image>();

        Press(keyboard.qKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 1);
        Assert.That(maillot1.color == new Color(255,255,255,1f));
        Assert.That(maillot2.color == new Color(255,255,255,0.5f));

        Press(keyboard.qKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 4);
        Assert.That(maillot4.color == new Color(255,255,255,1f));
        Assert.That(maillot1.color == new Color(255,255,255,0.5f));

        Press(keyboard.qKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 3);
        Assert.That(maillot3.color == new Color(255,255,255,1f));
        Assert.That(maillot4.color == new Color(255,255,255,0.5f));

        Press(keyboard.qKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 2);
        Assert.That(maillot2.color == new Color(255,255,255,1f));
        Assert.That(maillot3.color == new Color(255,255,255,0.5f));



        Press(keyboard.dKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 3);
        Assert.That(maillot3.color == new Color(255,255,255,1f));
        Assert.That(maillot2.color == new Color(255,255,255,0.5f));

        Press(keyboard.dKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 4);
        Assert.That(maillot4.color == new Color(255,255,255,1f));
        Assert.That(maillot3.color == new Color(255,255,255,0.5f));

        Press(keyboard.dKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 1);
        Assert.That(maillot1.color == new Color(255,255,255,1f));
        Assert.That(maillot4.color == new Color(255,255,255,0.5f));

        Press(keyboard.dKey);
        yield return new WaitForSeconds(2f);
        Assert.That(GameManager.instance.choixequipej2 == 2);
        Assert.That(maillot2.color == new Color(255,255,255,1f));
        Assert.That(maillot1.color == new Color(255,255,255,0.5f));
    }

    [UnityTest]
    
    public IEnumerator _03_EnterKey_devrait_charger_ecran_suivant()
    {
        LogAssert.ignoreFailingMessages = true;
        gameManager.LoadScene(SelectEquipScenePath);
        Press(keyboard.enterKey);
        yield return new WaitForSecondsRealtime(2);
        Assert.That(SceneManager.GetActiveScene().name == "Game1");
    }
}