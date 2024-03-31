using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject phase1;
    [SerializeField]
    private GameObject phase2;
    [SerializeField]
    private GameObject phase3;
    [SerializeField]
    private GameObject phase4;
    [SerializeField]
    private GameObject phase5;

    // make an int lenght filed for every phase
    [SerializeField]
    private int phase1Lenght;
    [SerializeField]
    private int phase2Lenght;
    [SerializeField]
    private int phase3Lenght;
    [SerializeField]
    private int phase4Lenght;
    [SerializeField]
    private int phase5Lenght;


    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private int phaseCounter;

    //in start make everything endColor
    private void Start()
    {
        phase1.GetComponent<Renderer>().material.color = startColor;
        phase2.GetComponent<Renderer>().material.color = startColor;
        phase3.GetComponent<Renderer>().material.color = startColor;
        phase4.GetComponent<Renderer>().material.color = startColor;
        phase5.GetComponent<Renderer>().material.color = startColor;
        phaseOneStart();
    }

    private void OnEnable()
    {
        EventManager.PhaseOverEvent += ChangePhase;
    }
    private void OnDisable()
    {
        EventManager.PhaseOverEvent -= ChangePhase;
    }

    private void ChangePhase()
    {
        switch (PhaseManager.Instance.CurrentPhase)
        {
            case 1:
                phaseTwoStart();
                break;
            case 2:
                phaseThreeStart();
                break;
            case 3:
                phaseFourStart();
                break;
            case 4:
                phaseFiveStart();
                break;
        }
    }

    private void phaseOneStart()
    {
        //lerp from startColor to endColor
        phase1.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, phase1Lenght);
    }

    private void phaseTwoStart()
    {
        //lerp from startColor to endColor
        phase2.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, phase2Lenght);
    }

    private void phaseThreeStart()
    {
        //lerp from startColor to endColor
        phase3.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, phase3Lenght);
    }

    private void phaseFourStart()
    {
        //lerp from startColor to endColor
        phase4.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, phase4Lenght);
    }

    private void phaseFiveStart()
    {
        //lerp from startColor to endColor
        phase5.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, phase5Lenght);
    }

}
