using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float remainingTime;
    private Coroutine coroutine;
    public Action timeOut;

    public void Init(float totalTime)
    {
        remainingTime = totalTime;
        SetTimeVisual();
    }

    public void StartTimer()
    {
        coroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        StopCoroutine(coroutine);
        //timerText.text = "00:00";
    }

    IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            SetTimeVisual();
            yield return null;
        }

        timerText.text = "00:00";
        timeOut?.Invoke();
    }

    private void SetTimeVisual()
    {
        int seconds = Mathf.Max((int)remainingTime, 0);
        int milliseconds = Mathf.Max((int)((remainingTime - seconds) * 100), 0);

        timerText.text = string.Format("{0:D2}:{1:D2}", seconds, milliseconds);
    }
}
