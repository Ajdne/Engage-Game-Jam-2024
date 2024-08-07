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
                pMovement.UndoMove();
                _trigerred = true;

                TimersManager.SetTimer(this, 0.3f, delegate ()
                {
                    _trigerred = false;
                });
            }
        }
    }
}
