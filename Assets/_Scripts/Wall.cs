using Timers;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private bool _trigerred;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement pMovement))
        {
            if (!_trigerred)
            {
                print("Im here");
                pMovement.UndoJump();
                _trigerred = true;

                TimersManager.SetTimer(this, 0.5f, delegate ()
                {
                    _trigerred = false;
                });
            }
        }
    }
}
