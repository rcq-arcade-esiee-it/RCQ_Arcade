using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

[ExcludeFromCodeCoverage]
public class SpriteCopy : EditorWindow
{
    private Object copyFrom;
    private Object copyTo;

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Copy from:", EditorStyles.boldLabel);
        copyFrom = EditorGUILayout.ObjectField(copyFrom, typeof(Texture2D), false, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Copy to:", EditorStyles.boldLabel);
        copyTo = EditorGUILayout.ObjectField(copyTo, typeof(Texture2D), false, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.Space(25f);
        if (GUILayout.Button("Copy pivots and slices")) CopyPivotsAndSlices();
    }

    // Creates a new option in "Windows"
    [MenuItem("Window/Copy Spritesheet pivots and slices")]
    private static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = (SpriteCopy)GetWindow(typeof(SpriteCopy));
        window.Show();
    }

    private void CopyPivotsAndSlices()
    {
        if (!copyFrom || !copyTo)
        {
            Debug.Log("Missing one object");
            return;
        }

        if (copyFrom.GetType() != typeof(Texture2D) || copyTo.GetType() != typeof(Texture2D))
        {
            Debug.Log("Cant convert from: " + copyFrom.GetType() + "to: " + copyTo.GetType() +
                      ". Needs two Texture2D objects!");
            return;
        }

        var copyFromPath = AssetDatabase.GetAssetPath(copyFrom);
        var ti1 = AssetImporter.GetAtPath(copyFromPath) as TextureImporter;
        ti1.isReadable = true;

        var copyToPath = AssetDatabase.GetAssetPath(copyTo);
        var ti2 = AssetImporter.GetAtPath(copyToPath) as TextureImporter;
        ti2.isReadable = true;

        ti2.spriteImportMode = SpriteImportMode.Multiple;

        var newData = new List<SpriteMetaData>();

        Debug.Log("Amount of slices found: " + ti1.spritesheet.Length);

        for (var i = 0; i < ti1.spritesheet.Length; i++)
        {
            var d = ti1.spritesheet[i];
            newData.Add(d);
        }

        ti2.spritesheet = newData.ToArray();

        AssetDatabase.ImportAsset(copyToPath, ImportAssetOptions.ForceUpdate);
    }
}