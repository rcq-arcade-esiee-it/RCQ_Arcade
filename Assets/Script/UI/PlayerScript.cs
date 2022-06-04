using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Update()
    {
        SceneChange();
    }

    public void SceneChange()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("eee");
        }
    }
}
