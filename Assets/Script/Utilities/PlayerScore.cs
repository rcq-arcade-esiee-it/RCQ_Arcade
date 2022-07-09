using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using PlasticGui.WorkspaceWindow.Merge;
using UnityEngine;
[ExcludeFromCodeCoverage]

[System.Serializable]
public class PlayerScore
{
    // Pour enregistrer le score d'une partie
    private static string player1Name="";
    private static int score1;
    private static string player2Name ="";
    private static int score2;
    private static List<PlayerScore> scores =  new List<PlayerScore>();

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

    public static void saveScoreToCurrentGame(String gameName)
    {
        if (player1Name.Length > 0 )  scores.Add(new PlayerScore(player1Name,score1));
        if (player2Name.Length > 0 )
            scores.Add(new PlayerScore(player2Name,score2));



        // Étape 1 : Récuperer le fichier des scores en fonction du nom du jeu
        int varTemp = 0;
        string fileName = Application.dataPath + "/Saves/" + "score_"+ gameName+".txt";
        TextReader reader;
        reader = new  StreamReader(fileName);
        string line;
        while (true)
        {
            // lecture de la ligne
            line=reader.ReadLine();
            // si la ligne est vide on arrête
            if (line==null ) break;
            // on affiche la ligne
            
            if(line.Length>0) scores.Add(new PlayerScore(line.Split(" ")[0],Int16.Parse(line.Split(" ")[1]))) ;
                
        }
        reader.Close();
        // Étape 2 : Met tout dans un nouveu tableu de PlayerScore et vide le fichier txt
        scores.Sort(new ScoreComparer());
        // Étape 3 :  Trier ce tableau
        // Étape 4: Remettre lme tableau dans le txt
        
        TextWriter tw = new StreamWriter(fileName, false);
        tw.Write(string.Empty);
        foreach (var  player in scores)
        {
            tw.Write(player.Name + " " + player.Score + "\n");
        }
        tw.Close();
        Erase();
    }

    private static void Erase()
    {
        scores = new List<PlayerScore>();

        player1Name = "";
        player2Name = "";
        score1 = 0 ;
        score2 = 0;



    }
}
