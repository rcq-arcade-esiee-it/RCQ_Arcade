using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : Player
{
    private void Start()
    {
        ChangeTeams(GameManager.instance.choixequipej2);
    }

    /// <summary>Cette méthode permet de faire bouger le joueur </summary>
    private void FixedUpdate()
    {
        MoveInput = PlayerActions.Player2_Map.Movement.ReadValue<Vector2>();

        if ((Joystick.current.name == PlayerJoystick.SecondJoystick) )
        {
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

        if (FirstGameManager.instance.stauntPlayer2) StartCoroutine(Staunt());
    }

    private void OnEnable()
    {
        PlayerActions.Player2_Map.Enable();
    }

    /// <summary>Cette méthode désactive le controle du joueur  </summary>
    private void OnDisable()
    {
        PlayerActions.Player2_Map.Disable();
    }

    /// <summary>Cette méthode active le controle du joueur  </summary>
    public IEnumerator Staunt()
    {
        FirstGameManager.instance.scorePlayer2 -= 3;
        FirstGameManager.instance.stauntPlayer2 = false;
        Collider2D.enabled = !Collider2D.enabled;
        Animator.SetBool("staunt", true);
        Speed = 0;
        yield return new WaitForSeconds(1); //waits 1 seconds
        Speed = 5000;
        Animator.SetBool("staunt", false);
        Collider2D.enabled = !Collider2D.enabled;
    }
}