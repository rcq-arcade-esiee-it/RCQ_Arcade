using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMenuScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button _button;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);

        GameObject.Find("Canvas/" + panelSelected(gameObject.name)).GetComponent<Image>().color =
            new Color32(255, 255, 255, 255);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(290, 258);
    }


    public void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(192, 44, 44, 255);


        GameObject.Find("Canvas/" + panelSelected(gameObject.name)).GetComponent<Image>().color =
            new Color32(192, 44, 44, 255);

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(268, 238);
        audio.Play();
    }

    private string panelSelected(string name)
    {
        var returnedPanel = "";
        switch (gameObject.name)
        {
            case "JEU 1":
                returnedPanel = "PANEL_BOUTON_1";
                break;
            case "JEU 2":
                returnedPanel = "PANEL_BOUTON_2";

                break;
            case "JEU 3":
                returnedPanel = "PANEL_BOUTON_3";
                break;

            case "JEU 4":
                returnedPanel = "PANEL_BOUTON_4";
                break;
        }

        return returnedPanel;
    }
}