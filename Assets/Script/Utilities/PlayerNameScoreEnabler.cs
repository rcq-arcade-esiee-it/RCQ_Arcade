using UnityEngine;

public class PlayerNameScoreEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject onePlayerCanvas;
    public GameObject twoPlayerCanvas;

    private void Start()
    {
        if (!GameManager.twoPlayers)
        {
            onePlayerCanvas.SetActive(true);
            twoPlayerCanvas.SetActive(false);
        }
        else
        {
            onePlayerCanvas.SetActive(false);
            twoPlayerCanvas.SetActive(true);
        }
    }
}