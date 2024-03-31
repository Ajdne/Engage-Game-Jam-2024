using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        EventManager.PhaseOverEvent += ShakeCamera;
    }
    private void OnDisable()
    {
        EventManager.PhaseOverEvent -= ShakeCamera;
    }
    public void ShakeCamera()
    {
        this.transform.DOShakePosition(1,0.4f);
        this.transform.DOShakeRotation(1,0.4f);
    }
}
