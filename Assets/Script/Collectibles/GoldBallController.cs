using UnityEngine;


/// <summary>Class <c>BallController</c> qui gère les intéractions  d'une balle avec un Joueur.</summary>
public class GoldBallController : MonoBehaviour
{

  

    /// <summary>Cette méthode supprime l'objet instancié de la balle dans le jeu
    ///    si la balle n'est plus dans le périmètre de la caméra
    /// </summary>
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
    /// <summary>Cette méthode Augmente le score de la partie si la balle entre en collison avec le joueur </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si l'objet touché est un Joueur
        if (other.CompareTag("Player") ||other.CompareTag("Player 2") )
        {
            var instanceScorePlayer1 = other.CompareTag("Player") ?
                // Appel de l'instance courante ( la partie en cours )
                FirstGameManager.instance.scorePlayer1 += 3:
            
                FirstGameManager.instance.scorePlayer2 += 3;

            // Destruction de la totalité de l'objet
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}