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
        var sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneManager.GetActiveScene().path);

        Assert.That(sceneName, Is.EqualTo("MainScreen"));
        // Cette fonction simule un joueur appuyant sur la touche a de son clavier
        Press(keyboard.aKey);
        yield return new WaitForSeconds(2f);

        sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("GameMenu"));
    }

    [UnityTest]
    public IEnumerator TestGameStart_texte_devrait_clignoter()
    {
        _textMeshProUGUI = GameObject.Find("Canvas/TextTypeStart").GetComponent<TextMeshProUGUI>();
        var firstColor = _textMeshProUGUI.color;
        yield return new WaitForSeconds(2f);
        Assert.That(firstColor, Is.Not.EqualTo(_textMeshProUGUI.color));
    }
}