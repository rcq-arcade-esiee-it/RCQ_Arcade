using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class DSelectionTwoPlayerTest : InputTestFixture
{
    private Keyboard keyboard;
    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;
    private string twoPlayerScenePath;
    private GameObject gameManagerPrefab;
    private GameManager gameManager;

    public override void Setup()
    {
        base.Setup();
        var twoPlayerScene = ((GameObject)Resources.Load("TestsReferences")).GetComponent<TestsReferences>()
            .twoPlayerScene;
        gameManagerPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject)))
            .GetComponent<TestsReferences>().gameManagerPrefab;
        gameManager = Object.Instantiate(gameManagerPrefab).GetComponent<GameManager>();

        twoPlayerScenePath = AssetDatabase.GetAssetPath(twoPlayerScene);
        keyboard = InputSystem.AddDevice<Keyboard>();
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject))).GetComponent<TestsReferences>().mainCanvasPrefab;
        EditorSceneManager.LoadSceneInPlayMode(twoPlayerScenePath, loadSceneParameters);

    }
        [UnityTest]
    public IEnumerator _01_OnSelect_devrait_changer_bouton_selectionne()
    {
        gameManager.LoadScene(twoPlayerScenePath);
        var button1 = GameObject.Find("MainCanvas/BT_P1/Image_P1").GetComponent<Image>();
        var button2 = GameObject.Find("MainCanvas/BT_P2/Image_P2").GetComponent<Image>();

		 Press(keyboard.dKey);
		 yield return new WaitForSeconds(2f);
         Assert.That(button1.sprite != GameObject.Find("MainCanvas/BT_P1/Image_P1").GetComponent<Image>().sprite);
         Assert.IsTrue(gameManager.ModeDeuxJoueur == true);

         
         Press(keyboard.aKey);
         yield return new WaitForSeconds(2f);
         Assert.That(button2.sprite != GameObject.Find("MainCanvas/BT_P1/Image_P1").GetComponent<Image>().sprite);

    }
    [UnityTest]
    public IEnumerator _02_EnterKey_devrait_charger_ecran_suivant()
    {
        Press(keyboard.enterKey);
        yield return new WaitForSeconds(2f);
        Assert.That( SceneManager.GetActiveScene().name
            == "TeamChoose");

    }
    [UnityTest]
    public IEnumerator _03_BackSpace_devrait_charger_ecran_menu()
    {
        gameManager.LoadScene(twoPlayerScenePath);

        Press(keyboard.backspaceKey);
        yield return new WaitForSeconds(2f);
        Assert.That( SceneManager.GetActiveScene().name
                     == "GameMenu");

    }
}