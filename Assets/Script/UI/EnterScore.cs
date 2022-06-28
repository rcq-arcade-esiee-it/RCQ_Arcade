using UnityEngine;
using System.Collections;
using System.IO; 
using System.Text;

public class EnterScore: MonoBehaviour 
{
    void Start ()
    {
        TextWriter writer;
        string fileName = "fichier.txt";
        float f=12.58f;
        writer = new StreamWriter(fileName);
               
        writer.Write(f);
        writer.Close();     
    }
}