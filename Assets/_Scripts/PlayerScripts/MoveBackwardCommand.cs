using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwardCommand : MonoBehaviour, ICommand
{
    private PlayerMovement _playerMovement;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Execute()
    {
        _playerMovement.MoveBackward();
    }
}
