using System.Collections;
using UnityEngine;

/// <summary>Class <c>BombController</c> qui gère les intéractions  d'une bomb avec un Joueur.</summary>
public class BombController : MonoBehaviour
{
    private Animator animator;

    // Start est appelé avant la mise à jour de la première image
    private void Start()
    {
        // Animation de la bombe 
        animator = GetComponent<Animator>();
        animator.SetBool("collide", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) StartCoroutine(GetStaunt());
    }
    
    /// <summary>Cette méthode supprime l'objet instancié de la bombe dans le jeu
    ///    si la bombe touche le joueur
    /// </summary>
    private IEnumerator GetStaunt()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        FirstGameManager.instance.staunt = true;

        animator.SetBool("collide", true);

        //play your sound
        yield return new WaitForSeconds(0.7f); //waits 1 seconds
        Destroy(gameObject.transform.parent.gameObject);
    }
}