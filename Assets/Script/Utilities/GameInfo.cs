using System.Diagnostics.CodeAnalysis;
using PlasticGui.WorkspaceWindow.Merge;
using UnityEngine;
 [ExcludeFromCodeCoverage]
[System.Serializable]
public class GameInfo
{
    public string title;
    public string description;
    public bool playerTwoModeAvailable;
    public string gameInfoScene;
    public string gameScene;

    public static GameInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameInfo>(jsonString);
    }
}