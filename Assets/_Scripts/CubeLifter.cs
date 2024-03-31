using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class CubeLifter : MonoBehaviour
{
    [SerializeField] private List<Transform> liftObjects = new();
    private List<float> _initialPositionsY = new();

    private void Start()
    {
        for (int i = 0; i < liftObjects.Count; i++)
        {
            _initialPositionsY.Add(liftObjects[i].position.y);
        }

        LiftUpCubesAtStart();
    }

    private void LiftUpCubesAtStart()
    {
        for (int i = 0; i < liftObjects.Count; i++)
        {
            liftObjects[i].position += new Vector3(0, UnityEngine.Random.Range(-5, -11), 0);
            //liftObjects[i].DOMoveY(_initialPositionsY[i], UnityEngine.Random.Range(0.5f, 2f));

            Transform trans = liftObjects[i];
            liftObjects[i].DOMoveY(UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(0.5f, 2f))
                .SetEase(Ease.OutSine)   // InSine
                .OnComplete(() =>
                {
                    trans.DOMoveY(0, 0.3f)
                    .SetEase(Ease.InSine)
                    ;
                })
                ;
        }
        TimersManager.SetTimer(this, 3f, delegate { EventManager.GameLoadedEvent?.Invoke(); });
    }

    private void LiftUpAllObjects()
    {
        TimersManager.SetTimer(this, 2f, delegate
        {
            foreach (Transform trans in liftObjects)
            {
                trans.DOMove(trans.position - new Vector3(0, UnityEngine.Random.Range(0.5f, 2f), 0), UnityEngine.Random.Range(0.25f, 1f))
                    .OnComplete(() =>
                    {
                        trans.DOMove(trans.position + new Vector3(0, 25, 0), UnityEngine.Random.Range(0.5f, 3f));

                    });
            }

            //TimersManager.SetTimer(this, 2f, delegate { EventManager.LoadNextLevelEvent?.Invoke(); });
        });

    }
}
