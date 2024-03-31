using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Settings", menuName = "Scriptable Objects/Player Movement Settings")]
public class PlayerMovementSO : ScriptableObject
{
    [SerializeField] private float jumpPower = 2;
    public float JumpPower { get => jumpPower; }

    [SerializeField] private float normalJumpDuration = 0.35f;
    public float NormalJumpDuration { get => normalJumpDuration; }

    [SerializeField] private float pickableJumpDuration = 0.5f;
    public float PickableJumpDuration { get => pickableJumpDuration; }

    [SerializeField] private float slideDuration;
    public float SlideDuration { get => slideDuration;}
}
