using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : NonPersistentSingleton<InputManager>
{
    private bool _canReadInput;
    public bool CanReadInput { get => _canReadInput; }

    [SerializeField] private int inputTime = 10;
    public int InputTime => inputTime;
    [Space(20)]
    [SerializeField] private PlayerInput p1Input;
    [SerializeField] private PlayerInput p2Input;

    private List<ICommand> _p1Commands = new();
    private List<ICommand> _p2Commands = new();

    [Space(20)]
    [SerializeField] private PhaseManager PhaseManager;
    [SerializeField] private float timeBetweenCommandExecutions = 0.5f;

    private void Awake()
    {
        Initialize();
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
        yield return new WaitForSeconds(0.1f);

        _canReadInput = true;
        yield return new WaitForSeconds(inputTime);

        // Time for reading inputs is over
        _canReadInput = false;

        yield return new WaitForSeconds(1);
    }

    public bool AddCommand(ICommand command, PlayerInput pInput)
    {
        if (pInput == p1Input
            && _p1Commands.Count < PhaseManager.GetMaxMoves()
            )
        {
            _p1Commands.Add(command);
            return true;
        }
        if (pInput == p2Input
            && _p2Commands.Count < PhaseManager.GetMaxMoves()
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
        for (int i = 0; i < PhaseManager.GetMaxMoves(); i++)
        {
            if (i < _p1Commands.Count)
            {
                _p1Commands[i].Execute();
            }
            if (i < _p2Commands.Count)
            {
                _p2Commands[i].Execute();
            }
            yield return new WaitForSeconds(timeBetweenCommandExecutions);
        }

        // Empty the Command lists
        _p1Commands.Clear();
        _p2Commands.Clear();

        EventManager.RoundOverEvent?.Invoke();

        /////////////////////////////////////////
        StartInputCoroutine();
    }
}
