using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

[ExcludeFromCodeCoverage]
[Serializable]
public class PlayerScore
{
    // Pour enregistrer le score d'une partie
    private static string player1Name = "";
    private static int score1;
    private static string player2Name = "";
    private static int score2;
    private static List<PlayerScore> scores = new();

    // Pour accèder aux scores d'une partie
    private string name;
    private int score;

    public PlayerScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

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

    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Score
    {
        get => score;
        set => score = value;
    }

    public static void resetCurrentScore(string gameName)
    {
        var fileName = Application.dataPath + "/Resources/Saves/" + "score_" + gameName + ".txt";

        File.WriteAllText(fileName, string.Empty);

    }

    public static void saveScoreToCurrentGame(string gameName, string playerScore)
    {
        Debug.Log(playerScore);

        if (playerScore == "Player1") scores.Add(new PlayerScore(player1Name, score1));
        else
            scores.Add(new PlayerScore(player2Name, score2));


        // Étape 1 : Récuperer le fichier des scores en fonction du nom du jeu
        var varTemp = 0;

        if (!Directory.Exists(Application.dataPath + "/Resources/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Resources/Saves");


        var fileName = Application.dataPath + "/Resources/Saves/" + "score_" + gameName + ".txt";
        // Si le fichier n'existe pas, il est crée
        var streamWriter = File.Exists(fileName) ? File.AppendText(fileName) : File.CreateText(fileName);
        streamWriter.Close();
        using (var sr = new StreamReader(fileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
                if (line.Length > 0)
                    scores.Add(new PlayerScore(line.Split(" ")[0], short.Parse(line.Split(" ")[1])));
        }

        scores.Sort(new ScoreComparer());
        File.WriteAllText(fileName, string.Empty);
        using (var sw = File.AppendText(fileName))
        {
            foreach (var player in scores) sw.WriteLine(player.Name + " " + player.Score);
        }

        Erase();
    }

    private static void Erase()
    {
        scores = new List<PlayerScore>();
    }
}