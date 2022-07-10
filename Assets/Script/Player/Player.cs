using System.Collections;
using UnityEngine;

/// <summary>Class <c>Player</c> représentant les mouvements et évenements des joueurs.</summary>
public class Player : MonoBehaviour
{
    // variables définissants un joueurs
    [SerializeField] private float _speed;
    public SpriteSwap SpriteSwap;

    // Getters et Setters

    public bool isWinner { get; set; }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public Collider2D Collider2D { get; set; }

    public Vector2 MoveInput { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }

    public Animator Animator { get; set; }

    public PlayerActions PlayerActions { get; set; }


    /// <summary>Cette méthode instancie les différents composants d'un joueur </summary>
    private void Awake()
    {
        PlayerActions = new PlayerActions();

        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        if (Rigidbody2D is null) Debug.Log("Pas de rigid body");
        Animator = GetComponent<Animator>();
        Animator.SetFloat("moveX", 0);
    }

    protected void ChangeTeams(int instanceChoixequipej1)
    {
        switch (instanceChoixequipej1)
        {
            case 1:
                SpriteSwap.SpriteSheetName = "CHATEAULIN";
                break;
            case 2:
                SpriteSwap.SpriteSheetName = "QUIMPER";
                break;
            case 3:
                SpriteSwap.SpriteSheetName = "BREST";
                break;
            case 4:
                SpriteSwap.SpriteSheetName = "CONGARNEAU";
                break;
            default:
                SpriteSwap.SpriteSheetName = "QUIMPER";
                break;
        }
    }


    /// <summary>Cette méthode initialise plusieurs animations en fonction du joueur </summary>
    public IEnumerator Winner()
    {
        Animator.SetBool("partyFinished", true);

        Animator.SetBool("winner", isWinner);
        Animator.SetBool("moving", false);

        _speed = 0;
        yield return new WaitForSeconds(2); //waits 1 seconds
    }
}