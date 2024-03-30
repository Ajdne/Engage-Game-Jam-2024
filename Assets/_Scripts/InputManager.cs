using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private bool _canReadInput = true;
    public bool CanReadInput { get => _canReadInput; }

    [SerializeField] private float inputTime = 10;
    [Space(20)]
    [SerializeField] private PlayerInput p1Input;
    [SerializeField] private PlayerInput p2Input;
    [Space(10)]
    [SerializeField] private ArrowImagesSO arrowImagesData;
    [Space(10)]
    [SerializeField] private Image[] p1InputImages;
    [SerializeField] private Image[] p2InputImages;

    private List<ICommand> _p1Commands = new();
    private List<ICommand> _p2Commands = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InputCoroutine());
    }

    private IEnumerator InputCoroutine()
    {
        _canReadInput = true;
        yield return new WaitForSeconds(inputTime);

        // Time for reading inputs is over
        _canReadInput = false;

        yield return new WaitForSeconds(2);

        StartCoroutine(ExecuteCommands());

        //for (int i = 0; i < functions.Count; i++)
        //{
        //    functions[i].Invoke();
        //}
    }

    public void AddCommand(ICommand command, PlayerInput pInput)
    {
        if (pInput == p1Input)
        {
            _p1Commands.Add(command);
        }
        if (pInput == p2Input)
        {
            _p2Commands.Add(command);
        }
    }

    private IEnumerator ExecuteCommands()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < _p1Commands.Count)
            {
                print(_p1Commands[i]);
                _p1Commands[i].Execute();
            }
            if (i < _p2Commands.Count)
            {
                print(_p2Commands[i]);
                _p2Commands[i].Execute();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
