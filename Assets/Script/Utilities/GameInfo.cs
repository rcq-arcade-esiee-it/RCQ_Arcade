using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
 [ExcludeFromCodeCoverage]
[System.Serializable]
public class GameInfo
{
    
    [SerializeField]
    private string title;
    [SerializeField]

    private string description;
    [SerializeField]

    private bool playerTwoModeAvailable;
    [SerializeField]

    private string gameInfoScene;
    [SerializeField]

    private string gameScene;

    public GameInfo(string title, string description, bool playerTwoModeAvailable, string gameInfoScene, string gameScene)
    {
        this.title = title;
        this.description = description;
        this.playerTwoModeAvailable = playerTwoModeAvailable;
        this.gameInfoScene = gameInfoScene;
        this.gameScene = gameScene;
    }

    public  string Title
    {
        get => title;
        set => title = value;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }

    public bool PlayerTwoModeAvailable
    {
        get => playerTwoModeAvailable;
        set => playerTwoModeAvailable = value;
    }

    public string GameInfoScene
    {
        get => gameInfoScene;
        set => gameInfoScene = value;
    }

    public string GameScene
    {
        get => gameScene;
        set => gameScene = value;
    }

    public static GameInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameInfo>(jsonString);
    }
}