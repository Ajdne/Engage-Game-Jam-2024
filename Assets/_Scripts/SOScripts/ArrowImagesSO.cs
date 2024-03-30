using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Arrow Input Images SO", menuName = "Scriptable Objects/Arrow Input Images")]
public class ArrowImagesSO : ScriptableObject
{
    [SerializeField] private Sprite upArrow;
    public Sprite UpArrow => upArrow;

    [SerializeField] private Sprite downArrow;
    public Sprite DownArrow => downArrow;

    [SerializeField] private Sprite leftArrow;
    public Sprite LeftArrow => leftArrow;

    [SerializeField] private Sprite rightArrow;
    public Sprite RightArrow => rightArrow;
}
