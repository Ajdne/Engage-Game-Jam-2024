using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement pMovement;
    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 3) // player layer
        {
            pMovement.UndoMove();
        }
    }
}
