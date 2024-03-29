using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement pMovement;
    [SerializeField] private string inputAxisHorizontal;
    [SerializeField] private string inputAxisVertical;

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!pMovement.CanMove) return;


        float inputHorizontal = Input.GetAxis(inputAxisHorizontal);
        float inputVertical = Input.GetAxis(inputAxisVertical);
        //if (inputHorizontal != 0 || inputVertical != 0) return;
        print(inputVertical);
        print(inputHorizontal);
        if (inputVertical > 0)
        {
            // Swipe up;
            pMovement.JumpBackMovement();

            //EventManager.JumpStartedEvent?.Invoke();
        }
        else if (
            inputVertical < 0
            )
        {
            pMovement.JumpMovement();
        }
        else if (
            inputHorizontal > 0
            )
        {
            // Right swipe;
            pMovement.SidewaysMovement(pMovement.transform.position + new Vector3(1, 0, 0));

            //EventManager.JumpStartedEvent?.Invoke();
        }
        else if (
            inputHorizontal < 0
            )
        {
            // Left swipe
            pMovement.SidewaysMovement(pMovement.transform.position + new Vector3(-1, 0, 0));

            //EventManager.JumpStartedEvent?.Invoke();
        }
    }
}
