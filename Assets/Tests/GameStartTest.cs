using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Please : InputTestFixture
{
    public override void Setup()
    {
        base.Setup();
        SceneManager.LoadScene("Scenes/MainScreen");
    }

    [UnityTest]
    public IEnumerator TestGameStart()
    {
        yield return null;
    }
}