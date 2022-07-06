using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Class <c>Player</c> représentant les mouvements et évenements des joueurs.</summary>

public class Player : MonoBehaviour
{
    // variables définissants un joueurs
    [SerializeField] private float _speed;
    private Collider2D _collider2D;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    private PlayerActions playerActions;
    private  bool winner;

    
    // Getters et Setters

    public bool isWinner
    {
        get => winner;
        set => winner = value;
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public Collider2D Collider2D
    {
        get => _collider2D;
        set => _collider2D = value;
    }

    public Vector2 MoveInput
    {
        get => _moveInput;
        set => _moveInput = value;
    }

    public Rigidbody2D Rigidbody2D
    {
        get => _rigidbody2D;
        set => _rigidbody2D = value;
    }

    public Animator Animator
    {
        get => animator;
        set => animator = value;
    }

    public PlayerActions PlayerActions
    {
        get => playerActions;
        set => playerActions = value;
    }
    

    /// <summary>Cette méthode instancie les différents composants d'un joueur </summary>
    private void Awake()
    {
        playerActions = new PlayerActions();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        if (_rigidbody2D is null) Debug.Log("Pas de rigid body");
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
    }

    
    
    /// <summary>Cette méthode initialise plusieurs animations en fonction du joueur </summary>

    public IEnumerator Winner()
    {
        
        animator.SetBool("partyFinished", true);

        animator.SetBool("winner", winner);
        animator.SetBool("moving", false);

        _speed = 0;
        yield return new WaitForSeconds(2); //waits 1 seconds
    }
    

    
  
}