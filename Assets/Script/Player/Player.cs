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
    public float speedAccess
    {
        get => _speed;
        set => _speed = value;
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


    /// <summary>Cette méthode permet de faire bouger le joueur </summary>
    private void FixedUpdate()
    {
        _moveInput = playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput.y = 0f;
        _rigidbody2D.velocity = _moveInput * _speed;
        if (_moveInput != Vector2.zero)
        {
            animator.SetFloat("moveX", _moveInput.x);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
        
            
        if (FirstGameManager.instance.partyFinished) StartCoroutine(Winner());
        
        if (FirstGameManager.instance.staunt)StartCoroutine(Staunt());
            
    }
    
    /// <summary>Cette méthode initialise plusieurs animations en fonction du joueur </summary>

    private IEnumerator Winner()
    {
        
        animator.SetBool("partyFinished", true);

        animator.SetBool("winner", winner);
        animator.SetBool("moving", false);

        speedAccess = 0;
        yield return new WaitForSeconds(2); //waits 1 seconds
    }
    
    /// <summary>Cette méthode active le controle du joueur  </summary>

    private void OnEnable()
    {
        playerActions.Player_Map.Enable();
    }
    /// <summary>Cette méthode désactive le controle du joueur  </summary>

    private void OnDisable()
    {
        playerActions.Player_Map.Disable();
    }
    /// <summary>Cette méthode initialise plusieurs animations en fonction du joueur </summary>

    private IEnumerator Staunt()
    {
        FirstGameManager.score -= 3;
        FirstGameManager.instance.staunt = false;
        _collider2D.enabled = !_collider2D.enabled;
        animator.SetBool("staunt", true);
        speedAccess = 0;
        yield return new WaitForSeconds(2); //waits 1 seconds
        speedAccess = 5000;
        animator.SetBool("staunt", false);
        _collider2D.enabled = !_collider2D.enabled;
    }
}