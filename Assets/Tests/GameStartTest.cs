using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Please : InputTestFixture
{
    private TextMeshProUGUI _textMeshProUGUI;
    private Keyboard keyboard;

    public override void Setup()
    {
        base.Setup();
        SceneManager.LoadScene("Scenes/MainScreen");
        

        keyboard = InputSystem.AddDevice<Keyboard>();
        Debug.Log(keyboard);
    }

    [UnityTest]
    public IEnumerator TestGameStart()
    {
       
        yield return new WaitForSeconds(2f);

        
    }

    [UnityTest]
    public IEnumerator TestGameStart_texte_devrait_clignoter()
    {
      
        yield return new WaitForSeconds(2f);
    }
}