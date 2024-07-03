using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private PlayerMovementSO playerMovementSO;
    [SerializeField] private Animator animator;
    [Space(20)]
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip jumpSound;
    //[SerializeField] private float initialJumpSoundPitch = 0.6f;
    private bool _canMove = true; // Used to prevent the tweens from stacking up
    public bool CanMove { get => _canMove; }

    private bool _fallTriggered;

    public static PlayerMovement Instance;

    private int jumpComboCount = 0;

    private int _movesMade;
    public bool IsDisorianted { get; set; }
    public bool IsStuned { get; set; }

    private Rigidbody _rb;
    [Space(10)]
    [SerializeField] private Transform modelTransform;
    private Vector3 _initialModelScale;
    [SerializeField] private ParticleSystem popParticles;

    [Space(10)]
    [SerializeField] private GameObject stunParticles;
    private List<Action<Vector3, float>> possibleMoves = new();

    // This part is for tracking the last movement needed for Spring
    private Vector3 _startingVector;
    private Vector3 _endingVector;

    #region Move Commands
    private MoveForwardCommand _moveUpCommand;
    public MoveForwardCommand MoveUpCommand => _moveUpCommand;

    private MoveBackwardCommand _moveDownCommand;
    public MoveBackwardCommand MoveDownCommand => _moveDownCommand;

    private MoveLeftCommand _moveLeftCommand;
    public MoveLeftCommand MoveLeftCommand => _moveLeftCommand;

    private MoveRightCommand _moveRightCommand;
    public MoveRightCommand MoveRightCommand => _moveRightCommand;
    #endregion Move Commands

    private GridManager _grid;

    private void OnEnable()
    {
        EventManager.GameLoadedEvent += SpawnPlayerModel;
    }
    private void OnDisable()
    {
        EventManager.GameLoadedEvent -= SpawnPlayerModel;
    }
    private void Start()
    {
        // Save initial scale, to do the upscaling spawn effect
        _initialModelScale = modelTransform.localScale;
        modelTransform.localScale = Vector3.zero;

        _rb = gameObject.GetComponent<Rigidbody>();

        _moveUpCommand = GetComponent<MoveForwardCommand>();
        _moveDownCommand = GetComponent<MoveBackwardCommand>();
        _moveLeftCommand = GetComponent<MoveLeftCommand>();
        _moveRightCommand = GetComponent<MoveRightCommand>();

        _grid = GridManager.Instance;
    }

    private void SpawnPlayerModel()
    {
        modelTransform.DOScale(_initialModelScale, 0.5f);
        Timers.TimersManager.SetTimer(this, 0.3f, delegate
        {
            popParticles.Play();
        });
    }

    public void MoveBackward()  // Called as input movement method
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(0, 0, -(1 + _grid.HalfYSpacing));

        transform.DOKill();

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DOJump(_endingVector, playerMovementSO.JumpPower, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });

        transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    }

    #region OLD CODE - DOUBLE JUMP
    //public void DoubleJumpMovement(Vector3 targetPos, float duration)   // Same as JumpMovement, but double duration and jump power
    //{
    //    _canMove = false;

    //    _startingVector = transform.position;
    //    _endingVector = targetPos;

    //    transform.parent = null;
    //    transform.DOKill();

    //    // Increment jump count
    //    jumpComboCount++;

    //    // ComboJumpEvent?
    //    //EventManager.JumpEvent?.Invoke(jumpComboCount);

    //    transform.DORotate(new Vector3(0, 0, 0), duration * 2, RotateMode.Fast);
    //}

    //public void DoubleJumpBasedOnDirection(Vector3 pickablePos)    // Used for Sprint or Trampoline
    //{
    //    _canMove = false;

    //    Vector3 jumpVect;

    //    transform.parent = null;
    //    transform.DOKill();

    //    // Increment jump count
    //    jumpComboCount++;

    //    //EventManager.JumpEvent?.Invoke(jumpComboCount);

    //    // Determine the direction the player is jumping and complete the rotation canceled by DOKill()
    //    if (_startingVector.x - _endingVector.x < 0)
    //    {
    //        jumpVect = new Vector3(2, 0, 0);
    //        transform.DORotate(new Vector3(0, -90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    //    }
    //    else if (_startingVector.x - _endingVector.x > 0)
    //    {
    //        jumpVect = new Vector3(-2, 0, 0);
    //        transform.DORotate(new Vector3(0, 90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    //    }
    //    else if (_startingVector.z - _endingVector.z < 0)
    //    {
    //        jumpVect = new Vector3(0, 2, 2);
    //        transform.DORotate(new Vector3(0, 180, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    //    }
    //    else
    //    {
    //        jumpVect = new Vector3(0, -2, -2);
    //        transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    //    }

    //    _endingVector = pickablePos + jumpVect;
    //}
    #endregion OLD CODE - DOUBLE JUMP

    public void MoveForward()  // Called as input movement
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(0, 0, 1 + _grid.HalfYSpacing);

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DOJump(_endingVector, playerMovementSO.JumpPower, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });

        transform.DORotate(new Vector3(0, 180, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    }

    public void MoveLeft()     // Input method
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(-(1 + _grid.HalfXSpacing), 0, 0);

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // MOVE RIGHT
        if (_endingVector.x > transform.position.x)
        {
            transform.DORotate(new Vector3(0, -90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        // MOVE LEFT
        else if (_endingVector.x < transform.position.x)
        {
            transform.DORotate(new Vector3(0, 90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }

        transform.DOJump(_endingVector, playerMovementSO.JumpPower / 2, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });
    }
    public void MoveRight()     // Input method
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(1 + _grid.HalfXSpacing, 0, 0);

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // MOVE RIGHT
        if (_endingVector.x > transform.position.x)
        {
            transform.DORotate(new Vector3(0, -90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        // MOVE LEFT
        else if (_endingVector.x < transform.position.x)
        {
            transform.DORotate(new Vector3(0, 90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }

        transform.DOJump(_endingVector, playerMovementSO.JumpPower / 2, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });
    }
    public void UndoMove()
    {
        _canMove = false;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        if (_startingVector != Vector3.zero)
        {
            transform.DOJump(_startingVector, playerMovementSO.JumpPower / 2, 1, playerMovementSO.NormalJumpDuration * 0.6f)
                .OnComplete(() =>
                {
                    FinishJump();
                });
        }
        else ChangeCanMove();
    }
    private void FinishJump()
    {
        // Calling this to update score with combo jumps and reset the combo value.
        InvokeJumpOverEvent();

        _startingVector = Vector3.zero;
        //if (transform.position.y > 1.51f)
        //{
        //    transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        //}    
        ChangeCanMove();
    }

    private void InvokeJumpOverEvent()
    {
        EventManager.JumpOverEvent?.Invoke(jumpComboCount);

        // Reset the jump combo value
        jumpComboCount = 0;
    }
    public void SlideMovement(Vector3 iceTilePosition)
    {
        _canMove = false;

        transform.parent = null;
        transform.DOKill();

        Vector3 moveVect;
        if (_startingVector.x - _endingVector.x < 0)
        {
            moveVect = new Vector3(1, 0, 0);
        }
        else if (_startingVector.x - _endingVector.x > 0)
        {
            moveVect = new Vector3(-1, 0, 0);
        }
        else if (_startingVector.z - _endingVector.z < 0)
        {
            moveVect = new Vector3(0, 0, 1);
            transform.DORotate(new Vector3(0, 180, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        else
        {
            moveVect = new Vector3(0, 0, -1);
            transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }

        animator.Play("Slide");
        ParticleManager.Instance.PlaySlideParticle(this.transform.position);
        transform.DOMove(iceTilePosition + moveVect + new Vector3(0, 1.5f, 0), playerMovementSO.SlideDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });
    }

    public void HandleFall()
    {
        if (!_fallTriggered)
        {
            // Prevent the player from moving
            _canMove = false;

            _fallTriggered = true;

            // de-parent
            transform.SetParent(null);

            // so the step starts falling down
            _rb.useGravity = true;
            _rb.isKinematic = false;
            _rb.velocity += new Vector3(0, -5, 0);

            // deactivate this script
            this.enabled = false;
        }
    }

    public void HandleStun()
    {
        if (!_fallTriggered)
        {
            IsDisorianted = true;

            // Activate particles
            stunParticles.SetActive(true);
        }
    }

    public void DoRandomMovement()
    {
        int randomMovement = UnityEngine.Random.Range(0, possibleMoves.Count);
        int randomX = UnityEngine.Random.Range(-1, 2);
        int randomYnZ = 0;

        if (randomX == 0)
        {
            randomYnZ = UnityEngine.Random.Range(-1, 1);
        }

        //Action<Vector3, float> movement = 
        possibleMoves[randomMovement].Invoke(transform.position + new Vector3(randomX, randomYnZ, randomYnZ), playerMovementSO.NormalJumpDuration);

        //movement(transform.position + new Vector3(-1, 0, 0), playerMovementSO.NormalJumpDuration);
        print("Random Movement");

        stunParticles.SetActive(false);
    }

    private void ChangeCanMove()
    {
        _canMove = !_canMove;
    }

    #region ------------- JUMP AUDIO ---------------
    //private void PlayJumpSound(int comboValue)
    //{
    //    if (jumpSound != null)
    //    {
    //        // Change the pitch according to the number of jumps
    //        audioSource.pitch = initialJumpSoundPitch + comboValue * 0.1f;

    //        //audioSource.clip = jumpSound;
    //        audioSource.PlayOneShot(jumpSound);
    //    }
    //}
    #endregion
}
