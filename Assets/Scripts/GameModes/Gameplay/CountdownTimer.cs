using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _remainingTime;
    public float remainingTime => _remainingTime;
    private Coroutine _coroutine;
    public Action timeOut;

    public void Init(float totalTime)
    {
        _remainingTime = totalTime;
        SetTimeVisual();
    }

    public void StartTimer()
    {
        _coroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        StopCoroutine(_coroutine);
        //timerText.text = "00:00";
    }

    IEnumerator UpdateTimer()
    {
        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            SetTimeVisual();
            yield return null;
        }

        timerText.text = "00:00";
        timeOut?.Invoke();
    }

    private void SetTimeVisual()
    {
        int seconds = Mathf.Max((int)_remainingTime, 0);
        int milliseconds = Mathf.Max((int)((_remainingTime - seconds) * 100), 0);

        timerText.text = string.Format("{0:D2}:{1:D2}", seconds, milliseconds);
    }
}
