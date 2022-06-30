using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teloport : MonoBehaviour
{

    public Transform teleportTarget;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")

            other.transform.position = teleportTarget.transform.position;

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
