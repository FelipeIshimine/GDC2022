using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnChangeUI : MonoBehaviour
{
    public float duration = 1.5f;
    public TextMeshProUGUI txt;

    private Action _callback;
    private IEnumerator _routine;
    
    public void Play(string text, Action callback)
    {
        Debug.Log("Start");
        gameObject.SetActive(true);
        _callback = callback;
        txt.text = text;
        this.PlayCoroutine(ref _routine, Animation);
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSecondsRealtime(duration);
        Debug.Log("End");
        gameObject.SetActive(false);
        _callback.Invoke();
    }
}
