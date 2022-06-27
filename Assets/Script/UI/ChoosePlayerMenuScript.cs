
using UnityEngine;
using UnityEngine.UI;
public class ChoosePlayerMenuScript : MonoBehaviour
{
    static int VarPlayer = 1;
    public Image P1_image;
    public Image P2_image;
    public Sprite P1_1;
    public Sprite P1_2;
    public Sprite P2_1;
    public Sprite P2_2;
    private AudioSource audio;



    void Start ()
    {

    }


    void ChangeImage()
    {
        if (VarPlayer == 1)
        {
           
            
            

        }
        else if (VarPlayer == 2)
        {
            
            
        }
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            VarPlayer = 1;
            Debug.Log("Mode 1 joueur");
            P1_image.sprite = P1_1;
            P2_image.sprite = P2_2;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            VarPlayer = 2;
            P1_image.sprite = P1_2;
            P2_image.sprite = P2_1;
            Debug.Log("Mode 2 joueurs");
        }
    }
}