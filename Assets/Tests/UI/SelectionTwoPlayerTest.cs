using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SelectionTwoPlayerTest : InputTestFixture
{
    private Keyboard keyboard;
    private LoadSceneParameters loadSceneParameters;
    private GameObject mainCanvasPrefab;

    private void Setup
    {
        keyboard = InputSystem.AddDevice<Keyboard>();
        mainCanvasPrefab = ((GameObject)Resources.Load("TestsReferences", typeof(GameObject))).GetComponent<TestsReferences>().mainCanvasPrefab;
    }
        [UnityTest]
    public IEnumerator _02_OnSelect_devrait_changer_bouton_selectionne()
    {

         var button1 = GameObject.Find("MainCanvas/BT_P1/JEU_1").GetComponent<Sprite>();
		 Press(keyboard.dKey);
		 yield return new WaitForSeconds(2f);
		 Assert.That(button1.sprite != GameObject.Find("MainCanvas/PANEL_BOUTON_1/JEU_1").GetComponent<Image>().sprite)
    
 
       
    }
}