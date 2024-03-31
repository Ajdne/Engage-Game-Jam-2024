using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsUI : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    private List<GameObject> arrows = new List<GameObject>();
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
        // Instantiate arrow gameobjects
        for (int i = 0; i < arrows.Count; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.SetActive(false);
            arrows.Add(arrow);
        }
    }

    public void AddInputArrow(GameObject arrowPrefab)
    {
        if (currentArrowIndex < arrows.Count)
        {
            GameObject arrow = arrows[currentArrowIndex];
            arrow.SetActive(true);
            arrow = arrowPrefab;

            currentArrowIndex++;
        }
    }

    private void ResetArrows()
    {
        currentArrowIndex = 0;  // Reset counter
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
}
