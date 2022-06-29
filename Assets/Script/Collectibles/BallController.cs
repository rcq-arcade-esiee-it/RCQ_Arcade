using UnityEngine;

public class BallController : MonoBehaviour
{

  

    // Update is called once per frame
    private void Update()
    {
        if (Camera.main != null)
        {
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(
                Random.Range(-0f, 1f),
                0f));
            if (gameObject.transform.position.y < spawnPosition.y)
                DestroyImmediate(gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FirstGameManager.score += 1;
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}