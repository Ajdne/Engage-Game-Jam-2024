using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Arrow Input Images SO", menuName = "Scriptable Objects/Arrow Input Images")]
public class ArrowImagesSO : ScriptableObject
{
    [SerializeField] private Sprite upArrowPrefab;
    public Sprite UpArrowPrefab => upArrowPrefab;

    [SerializeField] private Sprite downArrowPrefab;
    public Sprite DownArrowPrefab => downArrowPrefab;

    [SerializeField] private Sprite leftArrowPrefab;
    public Sprite LeftArrowPrefab => leftArrowPrefab;

    [SerializeField] private Sprite rightArrowPrefab;
    public Sprite RightArrowPrefab => rightArrowPrefab;
}
