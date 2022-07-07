using PlasticGui.WorkspaceWindow.Merge;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
    private static string player1Name;
    private static int score1;
    private static string player2Name;
    private static int score2;

    public static string Player1Name
    {
        get => player1Name;
        set => player1Name = value;
    }

    public static int Score1
    {
        get => score1;
        set => score1 = value;
    }

    public static string Player2Name
    {
        get => player2Name;
        set => player2Name = value;
    }

    public static int Score2
    {
        get => score2;
        set => score2 = value;
    }

    public static void saveScoreToCurrentGame()
    {
        
    }
}