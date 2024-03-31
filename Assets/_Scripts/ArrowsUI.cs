using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsUI : MonoBehaviour
{
    //[SerializeField] private GameObject arrowPrefab;
    [SerializeField] private List<Image> arrows = new List<Image>();
    private int currentArrowIndex;

    private void OnEnable()
    {
        EventManager.RoundOverEvent += ResetArrows;
    }

    private void OnDisable()
    {
        EventManager.RoundOverEvent -= ResetArrows;
    }

    private void Start()
    {
        ResetArrows();
        //// Instantiate arrow gameobjects
        //for (int i = 0; i < arrows.Count; i++)
        //{
        //    GameObject arrow = Instantiate(arrowPrefab, transform);
        //    arrow.SetActive(false);
        //    arrows.Add(arrow);
        //}
    }

    public void AddInputArrow(Sprite arrowPrefab)
    {
        if (currentArrowIndex < arrows.Count)
        {
            Image arrow = arrows[currentArrowIndex];
            arrow.sprite = arrowPrefab;

            currentArrowIndex++;
        }
    }

    private void ResetArrows()
    {
        currentArrowIndex = 0;  // Reset counter
        foreach (Image arrow in arrows)
        {
            arrow.enabled = false;
        }
    }
}
