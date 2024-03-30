using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _pMovement;
    [SerializeField] private string inputAxisHorizontal;
    [SerializeField] private string inputAxisVertical;
    private InputManager _IM;


    private void Start()
    {
        _pMovement = GetComponent<PlayerMovement>();
        _IM = InputManager.Instance;
    }

    private void Update()
    {
        if (!_pMovement.CanMove) return;
        if (!_IM.CanReadInput) return;


        // PREDUGO TRAJE INPUT - OGRANICITI KLIK VREMENSKI
        // PREDUGO TRAJE INPUT - OGRANICITI KLIK VREMENSKI
        // PREDUGO TRAJE INPUT - OGRANICITI KLIK VREMENSKI


        float inputHorizontal = Input.GetAxis(inputAxisHorizontal);
        float inputVertical = Input.GetAxis(inputAxisVertical);

        if (inputVertical > 0)
        {
            // Swipe up;
            ICommand moveUp = _pMovement.MoveUpCommand;
            _IM.AddCommand(moveUp, this);

            //EventManager.JumpStartedEvent?.Invoke();
        }
        else if (
            inputVertical < 0
            )
        {
            ICommand moveDown = _pMovement.MoveDownCommand;
            _IM.AddCommand(moveDown, this);
        }
        else if (
            inputHorizontal > 0
            )
        {
            // Right swipe;
            ICommand moveRight = _pMovement.MoveRightCommand;
            _IM.AddCommand(moveRight, this);

            //EventManager.JumpStartedEvent?.Invoke();
        }
        else if (
            inputHorizontal < 0
            )
        {
            // Left swipe
            ICommand moveLeft = _pMovement.MoveLeftCommand;
            _IM.AddCommand(moveLeft, this);

            //EventManager.JumpStartedEvent?.Invoke();
        }
    }
}
