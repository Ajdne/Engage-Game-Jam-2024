using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownCommand : MonoBehaviour, ICommand
{
    private PlayerMovement _playerMovement;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Execute()
    {
        _playerMovement.MoveDown();
    }
}
