using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("collide", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) StartCoroutine(Die());
    }

    // Update is called once per frame


    //And function itself
    private IEnumerator Die()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        FirstGameManager.instance.staunt = true;

        animator.SetBool("collide", true);

        //play your sound
        yield return new WaitForSeconds(0.7f); //waits 1 seconds
        Destroy(gameObject.transform.parent.gameObject);
    }
}