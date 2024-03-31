using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _pMovement;
    [Header("P1 Input Buttons")]
    [SerializeField] private KeyCode playerUpButton;
    [SerializeField] private KeyCode playerDownButton;
    [SerializeField] private KeyCode playerLeftButton;
    [SerializeField] private KeyCode playerRightButton;
    [Space(10)]
    [SerializeField] private ArrowsUI arrowsUI;
    [SerializeField] private ArrowImagesSO arrowImagesSO;

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
                if (_IM.AddCommand(moveUp, this)) arrowsUI.AddInputArrow(arrowImagesSO.UpArrowPrefab);
            }
            else if (Input.GetKeyDown(playerDownButton))
            {
                _inputOver = false;

                ICommand moveDown = _pMovement.MoveDownCommand;
                if (_IM.AddCommand(moveDown, this)) arrowsUI.AddInputArrow(arrowImagesSO.DownArrowPrefab);
            }
            else if (Input.GetKeyDown(playerRightButton))
            {
                _inputOver = false;

                // Right swipe;
                ICommand moveRight = _pMovement.MoveRightCommand;
                if (_IM.AddCommand(moveRight, this)) arrowsUI.AddInputArrow(arrowImagesSO.RightArrowPrefab);
            }
            else if (Input.GetKeyDown(playerLeftButton))
            {
                _inputOver = false;

                // Left swipe
                ICommand moveLeft = _pMovement.MoveLeftCommand;
                if (_IM.AddCommand(moveLeft, this)) arrowsUI.AddInputArrow(arrowImagesSO.LeftArrowPrefab);
            }

        }
        else if (
            Input.GetKeyUp(playerUpButton)
            || Input.GetKeyUp(playerDownButton)
            || Input.GetKeyUp(playerRightButton)
            || Input.GetKeyUp(playerLeftButton)
            )
        {
            _inputOver = true;
        }


    }
}
