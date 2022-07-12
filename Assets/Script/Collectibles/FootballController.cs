using System;
using System.Collections;
using UnityEngine;

/// <summary>Class <c>BombController</c> qui gère les intéractions  d'une bomb avec un Joueur.</summary>
public class FootballController : MonoBehaviour
{
    private Animator animator;

    // Start est appelé avant la mise à jour de la première image
    private void Start()
    {
        // Animation de la bombe 
       // animator = GetComponent<Animator>();
        //animator.SetBool("collide", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player 2")) StartCoroutine(GetStaunt(other.tag));
        
    }
    
    /// <summary>Cette méthode supprime l'objet instancié de la bombe dans le jeu
    ///    si la bombe touche le joueur
    /// </summary>
    private IEnumerator GetStaunt(String PlayerStaunted)
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Debug.Log(PlayerStaunted);
        var instanceStauntPlayer1 = PlayerStaunted == "Player"
            ? FirstGameManager.instance.stauntPlayer1 = true
            : FirstGameManager.instance.stauntPlayer2 = true;

     //   animator.SetBool("collide", true);

        //play your sound
        yield return null; //waits 1 seconds
        Destroy(gameObject.transform.parent.gameObject);
    }
}