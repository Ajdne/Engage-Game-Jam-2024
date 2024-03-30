using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private bool _canReadInput;
    public bool CanReadInput { get => _canReadInput; }

    [SerializeField] private float inputTime = 10;
    [Space(20)]
    [SerializeField] private PlayerInput p1Input;
    [SerializeField] private PlayerInput p2Input;

    private List<ICommand> _p1Commands = new();
    private List<ICommand> _p2Commands = new();

    [Space(20)]
    [SerializeField] private int _maxInputs = 1;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        EventManager.GameLoadedEvent += StartInputCoroutine;
        EventManager.StartMovementEvent += StartCommandExecution;
    }
    private void OnDisable()
    {
        EventManager.GameLoadedEvent -= StartInputCoroutine;
        EventManager.StartMovementEvent -= StartCommandExecution;
    }

    private void StartInputCoroutine()
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


        //for (int i = 0; i < functions.Count; i++)
        //{
        //    functions[i].Invoke();
        //}
    }

    public bool AddCommand(ICommand command, PlayerInput pInput)
    {
        if (pInput == p1Input
            && _p1Commands.Count < _maxInputs
            )
        {
            _p1Commands.Add(command);
            return true;
        }
        if (pInput == p2Input
            && _p2Commands.Count < _maxInputs
            )
        {
            _p2Commands.Add(command);
            return true;
        }
        return false;
    }
    private void StartCommandExecution()
    {
        StartCoroutine(ExecuteCommands());
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

        // Empty the Command lists
        _p1Commands.Clear();
        _p2Commands.Clear();
    }
}
