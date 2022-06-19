using System.Collections;
using TMPro;
using UnityEngine;

public class UICount : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI count;

    private void Start()
    {
        count = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Truc1());
    }

    // Update is called once per frame
    private void Update()
    {
        count.text = FirstGameManager.instance.time.ToString();
        if (FirstGameManager.instance.time <= 0) FirstGameManager.instance.partyFinished = true;
    }


    private IEnumerator Truc1()
    {
        while (!FirstGameManager.instance.partyFinished)
        {
            FirstGameManager.instance.time -= 1;
            yield return new WaitForSeconds(1);
        }
    }
}