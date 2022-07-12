using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Class <c>Player</c> représentant les mouvements et évenements des joueurs.</summary>
public class Player1Controller : Player
{
    private void Start()
    {
        ChangeTeams(GameManager.instance.choixequipej1);
    }

    private void FixedUpdate()
    {

        if ((Joystick.current.name == PlayerJoystick.FirstJoystick)
            )
        {
        MoveInput = PlayerActions.Player_Map.Movement.ReadValue<Vector2>();

            Rigidbody2D.velocity = MoveInput * Speed;
            if (MoveInput != Vector2.zero)
            {
                Animator.SetFloat("moveX", MoveInput.x);
                Animator.SetBool("moving", true);
            }
            else
            {
                Animator.SetBool("moving", false);
            }
        }


        if (FirstGameManager.instance.partyFinished) StartCoroutine(Winner());

        if (FirstGameManager.instance.stauntPlayer1) StartCoroutine(Staunt());
    }

    private void OnEnable()
    {
        PlayerActions.Player_Map.Enable();
    }

    /// <summary>Cette méthode désactive le controle du joueur  </summary>
    private void OnDisable()
    {
        PlayerActions.Player_Map.Disable();
    }

    /// <summary>Cette méthode initialise plusieurs animations en fonction du joueur </summary>
    public IEnumerator Staunt()
    {
        FirstGameManager.instance.scorePlayer1 -= 3;
        FirstGameManager.instance.stauntPlayer1 = false;
        Collider2D.enabled = !Collider2D.enabled;
        Animator.SetBool("staunt", true);
        Speed = 0;
        yield return new WaitForSeconds(1); //waits 1 seconds
        Speed = 5000;
        Animator.SetBool("staunt", false);
        Collider2D.enabled = !Collider2D.enabled;
    }
}