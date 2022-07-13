using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
class Shutdown
{
     public static void Main()
    {
       //création de la commande que l'o nsouhaite lancer
        var output = ExecuteBashCommand("shutdown now");

        //on écrit le résultat dans la console c#
        Console.WriteLine(output);
    }

    static string ExecuteBashCommand(string command)
    {
        // en accord avec : https://stackoverflow.com/a/15262019/637142
        // grâce à cela, nous passerons tout en une seule commande
        command = command.Replace("\"","\"\"");

        //on crée un nouveau processus qui va lancer le terminal de commande et executer la commande.
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = "-c \""+ command + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        proc.Start(); //on lance le processus
        proc.WaitForExit(); // on attend que le processus ce termine

        return proc.StandardOutput.ReadToEnd(); //et on lit le resultat
    }
}
