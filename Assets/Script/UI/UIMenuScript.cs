using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
/// <summary>Class <c>UIMenuScript</c> représentant les selections pouvant êtres faites dans le menu des jeux </summary>
public class UIMenuScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    


    /// <summary>Cette méthode supprime modifie l'objet déselectionné avec plusieurs critères
    /// </summary>
    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);

        GameObject.Find("MainCanvas/" + panelSelected(gameObject.name)).GetComponent<Image>().color =
            new Color32(255, 255, 255, 255);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(290, 258);
    }

    /// <summary>Cette méthode  modifie l'objet selectionné avec plusieurs critères
    /// </summary>
    public void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(192, 44, 44, 255);


        GameObject.Find("MainCanvas/" + panelSelected(gameObject.name)).GetComponent<Image>().color =
            new Color32(192, 44, 44, 255);

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(268, 238);
        audio.Play();
    }

    /// <summary>Cette méthode  permet de connaitre le nom du composant selectionné
    /// <returns> le nom du panneau</returns>
    /// </summary>
    private string panelSelected(string name)
    {
        var returnedPanel = "";
        switch (gameObject.name)
        {
            case "JEU_1":
                returnedPanel = "PANEL_BOUTON_1";
                break;
            case "JEU_2":
                returnedPanel = "PANEL_BOUTON_2";

                break;
            case "JEU_3":
                returnedPanel = "PANEL_BOUTON_3";
                break;

            case "JEU_4":
                returnedPanel = "PANEL_BOUTON_4";
                break;
        }

        return returnedPanel;
    }
}