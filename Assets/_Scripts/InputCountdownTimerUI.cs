using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class InputCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private GameManager _GM;

    private void OnEnable()
    {
        EventManager.GameLoadedEvent += StartCountdownCoroutine;
    }
    private void OnDisable()
    {
        EventManager.GameLoadedEvent -= StartCountdownCoroutine;
    }

    private void Start()
    {
        countdownText.enabled = false;
        _GM = GameManager.Instance;
    }
    private void StartCountdownCoroutine()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.enabled = true;

        for (int i = _GM.TimeForInput; i > -1; i--)
        {
            countdownText.text = i.ToString();
            countdownText.rectTransform.DOPunchScale(Vector3.one, 1f, 5);
            yield return new WaitForSeconds(1f);
        }
        countdownText.enabled = false;
        EventManager.StartMovementEvent?.Invoke();
    }
}
