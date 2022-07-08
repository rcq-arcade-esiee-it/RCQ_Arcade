using System;
using System.Diagnostics;
namespace Script.Utilities

{
    public class Shutdown
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
    }
    }
/*#!/usr/bin/csexec -r:System.Windows.Forms.dll -r:System.Drawing.dll                                                                   
using System;                                                                                                                                
using System.Drawing;                                                                                                                        
using System.Windows.Forms;                                                                                                                  
public class Program                                                                                                                         
{                                                                                                                                            
    public static void Main(string[] args)                                                                                                     
    {                                                                                                                                          
        Console.WriteLine("Hello Console");                                                                                                      
        Console.WriteLine("Arguments: " + string.Join(", ", args));                                                                              
        MessageBox.Show("Hello GUI");                                                                                                            
    }                                                                                                                                          
}        */