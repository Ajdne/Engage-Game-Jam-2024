using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class InputCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private InputManager _IM;

    private void OnEnable()
    {
        EventManager.GameLoadedEvent += StartCountdownCoroutine;
        EventManager.RoundOverEvent += StartCountdownCoroutine;
    }
    private void OnDisable()
    {
        EventManager.GameLoadedEvent -= StartCountdownCoroutine;
        EventManager.RoundOverEvent -= StartCountdownCoroutine;
    }

    private void Start()
    {
        countdownText.enabled = false;
        _IM = InputManager.Instance;
    }
    private void StartCountdownCoroutine()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.enabled = true;

        for (int i = _IM.InputTime; i > -1; i--)
        {
            countdownText.text = i.ToString();
            countdownText.rectTransform.DOPunchScale(Vector3.one * 0.7f, 1f, 5);
            //AudioManager.Instance.PlayClockSound();
            EventManager.Instance.startSFXEvent("Clock");
            yield return new WaitForSeconds(1f);
        }
        countdownText.enabled = false;
        EventManager.StartMovementEvent?.Invoke();
    }
}
