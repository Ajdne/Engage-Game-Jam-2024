using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _pMovement;
    [Header("P1 Input Buttons")]
    [SerializeField] private KeyCode playerUpButton;
    [SerializeField] private KeyCode playerDownButton;
    [SerializeField] private KeyCode playerLeftButton;
    [SerializeField] private KeyCode playerRightButton;

    private InputManager _IM;
    private bool _inputOver = true;

    private void Start()
    {
        _pMovement = GetComponent<PlayerMovement>();
        _IM = InputManager.Instance;
    }

    private void Update()
    {
        if (!_pMovement.CanMove) return;
        if (!_IM.CanReadInput) return;
        if (_inputOver)
        {
            if (Input.GetKeyDown(playerUpButton))
            {
                _inputOver = false;

                // Swipe up;
                ICommand moveUp = _pMovement.MoveUpCommand;
                _IM.AddCommand(moveUp, this);

                //EventManager.JumpStartedEvent?.Invoke();
            }
            else if (Input.GetKeyDown(playerDownButton))
            {
                _inputOver = false;

                ICommand moveDown = _pMovement.MoveDownCommand;
                _IM.AddCommand(moveDown, this);
            }
            else if (Input.GetKeyDown(playerRightButton))
            {
                _inputOver = false;

                // Right swipe;
                ICommand moveRight = _pMovement.MoveRightCommand;
                _IM.AddCommand(moveRight, this);

                //EventManager.JumpStartedEvent?.Invoke();
            }
            else if (Input.GetKeyDown(playerLeftButton))
            {
                _inputOver = false;

                // Left swipe
                ICommand moveLeft = _pMovement.MoveLeftCommand;
                _IM.AddCommand(moveLeft, this);

                //EventManager.JumpStartedEvent?.Invoke();
            }

        }
        else if (
            Input.GetKeyUp(playerUpButton)
            || Input.GetKeyUp(playerDownButton)
            || Input.GetKeyUp(playerRightButton)
            || Input.GetKeyUp(playerLeftButton)
            )
        {
            print("Input over!");
            _inputOver = true;
        }


    }
}
