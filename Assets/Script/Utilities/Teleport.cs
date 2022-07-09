using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[ExcludeFromCodeCoverage]
public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player" || other.tag == "Player 2") && !FirstGameManager.testEnabled)
            other.transform.position = teleportTarget.transform.position;
    }
}