using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField]
    private UIManager _uiManager;

    [Header("Oprions")]

    [SerializeField]
    private float _amountTime;
    [SerializeField]
    private Text _timerText;

    private float _timer;
    private string _seconds;
    private string _minutes;

    private void Start()
    {
        _timer = _amountTime;
        StartCoroutine(CoroutineTimer());
    }

    IEnumerator CoroutineTimer()
    {
        while (_timer > 0f)
        {
            _timer += Time.deltaTime;
            UpdateTimer();
            yield return null;
        }
    }

    private void UpdateTimer()
    {
        _seconds = (_timer % 60).ToString("00.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        _minutes = ((int)_timer / 60).ToString("00");
        _timerText.text = _minutes + ":" + _seconds;

        if (_timer <= 0f)
        {
            _uiManager.Death();
            _timerText.text = "00" + ":" + "00.00";
        }
    }
}
