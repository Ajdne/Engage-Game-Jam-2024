using DG.Tweening;
using System;
using System.Collections.Generic;
using Timers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private PlayerMovementSO playerMovementSO;

    [Space(10)]
    [SerializeField] private Animator animator;
    [Space(20)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float initialJumpSoundPitch = 0.6f;
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
    [SerializeField] private GameObject stunParticles;
    private List<Action<Vector3, float>> possibleMoves = new();

    // This part is for tracking the last movement needed for Spring
    private Vector3 _startingVector;
    private Vector3 _endingVector;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody>();

        //possibleMoves.Add(JumpMovement);
        //possibleMoves.Add(SidewaysMovement);
    }

    private void OnEnable()
    {
        //EventManager.JumpEvent += PlayJumpSound;
    }

    private void OnDisable()
    {
        //EventManager.JumpEvent -= PlayJumpSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Stairs layer
        {
            transform.parent = other.transform;

            //if (other.TryGetComponent(out Cracked cracked))
            //{
            //    cracked.WatchForPlayerJump();
            //}
        }
    }

    public void JumpMovement(Vector3 targetPos, float duration) // Called when interacting with pickables
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = targetPos;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DOJump(_endingVector, playerMovementSO.JumpPower, 1, duration)
            .OnComplete(() =>
            {
                FinishJump();
            });

        transform.DORotate(new Vector3(0, 0, 0), duration, RotateMode.Fast);
    }

    public void JumpMovement()  // Called as input movement method
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(0, 0, -1);

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DOJump(transform.position + new Vector3(0, 0, -1), playerMovementSO.JumpPower, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });

        transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    }

    public void DoubleJumpMovement(Vector3 targetPos, float duration)   // Same as JumpMovement, but double duration and jump power
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = targetPos;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DORotate(new Vector3(0, 0, 0), duration * 2, RotateMode.Fast);
    }

    public void DoubleJumpBasedOnDirection(Vector3 pickablePos)    // Used for Sprint or Trampoline
    {
        _canMove = false;

        Vector3 jumpVect;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        // Determine the direction the player is jumping and complete the rotation canceled by DOKill()
        if (_startingVector.x - _endingVector.x < 0)
        {
            jumpVect = new Vector3(2, 0, 0);
            transform.DORotate(new Vector3(0, -90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        else if (_startingVector.x - _endingVector.x > 0)
        {
            jumpVect = new Vector3(-2, 0, 0);
            transform.DORotate(new Vector3(0, 90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        else if (_startingVector.z - _endingVector.z < 0)
        {
            jumpVect = new Vector3(0, 2, 2);
            transform.DORotate(new Vector3(0, 180, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        else
        {
            jumpVect = new Vector3(0, -2, -2);
            transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }

        _endingVector = pickablePos + jumpVect;
    }

    public void JumpBackMovement()  // Called as input movement
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = transform.position + new Vector3(0, 0, 1);

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // ComboJumpEvent?
        //EventManager.JumpEvent?.Invoke(jumpComboCount);

        transform.DOJump(transform.position + new Vector3(0, 0, 1), playerMovementSO.JumpPower, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });

        transform.DORotate(new Vector3(0, 180, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
    }

    public void SidewaysMovement(Vector3 targetPos, float duration)
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = targetPos;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // MOVE RIGHT
        if (targetPos.x > transform.position.x)
        {
            transform.DORotate(new Vector3(0, -90, 0), duration, RotateMode.Fast);
        }
        // MOVE LEFT
        else if (targetPos.x < transform.position.x)
        {
            transform.DORotate(new Vector3(0, 90, 0), duration, RotateMode.Fast);
        }

        transform.DOJump(targetPos, playerMovementSO.JumpPower / 2, 1, duration)
            .OnComplete(() =>
            {
                FinishJump();
            });
    }

    public void SidewaysMovement(Vector3 targetPos)     // Input method
    {
        _canMove = false;

        _startingVector = transform.position;
        _endingVector = targetPos;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        // MOVE RIGHT
        if (targetPos.x > transform.position.x)
        {
            transform.DORotate(new Vector3(0, -90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }
        // MOVE LEFT
        else if (targetPos.x < transform.position.x)
        {
            transform.DORotate(new Vector3(0, 90, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
        }

        transform.DOJump(targetPos, playerMovementSO.JumpPower / 2, 1, playerMovementSO.NormalJumpDuration)
            .OnComplete(() =>
            {
                FinishJump();
            });
    }
    private void FinishJump()
    {
        // Calling this to update score with combo jumps and reset the combo value.
        InvokeJumpOverEvent();

        _movesMade++;

        ChangeCanMove();
    }
    private void InvokeJumpOverEvent()
    {
        EventManager.JumpOverEvent?.Invoke(jumpComboCount);

        // Reset the jump combo value
        jumpComboCount = 0;
    }
    public void DynamitePlungerJump(Vector3 targetPos)
    {
        _canMove = false;

        transform.parent = null;
        transform.DOKill();

        // Increment jump count
        jumpComboCount++;

        transform.DORotate(new Vector3(0, 0, 0), playerMovementSO.NormalJumpDuration, RotateMode.Fast);
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

    public void HandleBomb()
    {
        if (!_fallTriggered)
        {
            // de-parent
            transform.SetParent(null);
            transform.DOKill();

            // Prevent the player from moving
            _canMove = false;

            _fallTriggered = true;

            float randomVal = UnityEngine.Random.Range(-1f, 3f);
            _rb.useGravity = true;
            _rb.isKinematic = false;
            _rb.AddExplosionForce(800, transform.position + new Vector3(randomVal, -2f, randomVal * 2), 13);

            // Invoke the Falling Event
            //EventManager.PlayerFallingEvent?.Invoke();

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

    private void PlayAnimationAndStartEndingRun()
    {
        transform.parent = null;
        animator.enabled = true;

        //TimersManager.SetTimer(this, 2f, delegate
        //{
        //    EventManager.StartEndingRun?.Invoke();

        //    //animator.enabled = false;   // So we can re-anble it later
        //});
    }

    private void ActivateFinalCelebration()
    {
        animator.enabled = true;
        //animator.Play("Jump");
    }

    public void ExplodeByBomb()
    {
        HandleBomb();
    }

    #region ------------- JUMP AUDIO ---------------
    private void PlayJumpSound(int comboValue)
    {
        if (jumpSound != null)
        {
            // Change the pitch according to the number of jumps
            audioSource.pitch = initialJumpSoundPitch + comboValue * 0.1f;

            //audioSource.clip = jumpSound;
            audioSource.PlayOneShot(jumpSound);
        }
    }
    #endregion
}