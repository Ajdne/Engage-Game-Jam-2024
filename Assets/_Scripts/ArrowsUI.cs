using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsUI : MonoBehaviour
{
    [SerializeField] private Image[] pInputImages;
    private int _currentImage;

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
        // Disable all images at start
        foreach (Image image in pInputImages)
        {
            image.enabled = false;
        }
    }

    public void AddInputArrow(Sprite arrowSprite)
    {
        if (pInputImages.Length > _currentImage)
        {
            pInputImages[_currentImage].enabled = true;
            pInputImages[_currentImage].sprite = arrowSprite;

            _currentImage++;
        }
    }

    private void ResetArrows()
    {
        _currentImage = 0;  // Reset counter
        foreach (Image image in pInputImages)
        {
            image.enabled = false;
        }
    }
}
