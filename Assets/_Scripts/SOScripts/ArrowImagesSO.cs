using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Arrow Input Images SO", menuName = "Scriptable Objects/Arrow Input Images")]
public class ArrowImagesSO : ScriptableObject
{
    [SerializeField] private GameObject upArrowPrefab;
    public GameObject UpArrowPrefab => upArrowPrefab;

    [SerializeField] private GameObject downArrowPrefab;
    public GameObject DownArrowPrefab => downArrowPrefab;

    [SerializeField] private GameObject leftArrowPrefab;
    public GameObject LeftArrowPrefab => leftArrowPrefab;

    [SerializeField] private GameObject rightArrowPrefab;
    public GameObject RightArrowPrefab => rightArrowPrefab;
}
