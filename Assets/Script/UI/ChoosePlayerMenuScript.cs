using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChoosePlayerMenuScript : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;
    private AudioSource audio;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisF1rame)
        {
            GameObject.Find("MainCanvas/" + buttonSelected("BT_P1")).GetComponent(Image);


        }
        else if (Keyboard.current.dKey.wasPressedThisFrale)
        {

        }
    }




    private string buttonSelected(string name)
    {
        var returnedPanel = "";
        switch (gameObject.name)
        {
            case "P1":
                returnedPanel = "BT_P1";
                break;
            case "P2":
                returnedPanel = "BT_P2";

                break;

                return returnedPanel;
        }
    }
}