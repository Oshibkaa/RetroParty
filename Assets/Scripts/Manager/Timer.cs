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
    private float _timerUpdate;
    [SerializeField]
    private float _updateTime;
    [SerializeField]
    private Text _textTimer;

    private string _seconds;
    private string _minutes;

    private void Start()
    {
        _timerUpdate = _updateTime;
        StartCoroutine(StartTimer());
    }

    public void StartTimer(float seconds)
    {
        _timerUpdate = seconds;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _seconds = (_timerUpdate % 60).ToString("00.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        _minutes = ((int)_timerUpdate / 60).ToString("00");
        _textTimer.text = _minutes + ":" + _seconds;

        if (_timerUpdate <= 0)
        {
            _uiManager.Death();
        }
    }

    public void UpdateTimeLvl()
    {
        _timerUpdate = _updateTime;
    }

    public IEnumerator StartTimer()
    {
        while (_timerUpdate > 0)
        {
            _timerUpdate -= Time.deltaTime;
            UpdateTimer();
            yield return null;
        }
    }
}
