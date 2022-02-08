using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float duration = .2f;
    public AnimationCurve attackScaleCurve;
    public Transform target;

    
    [Range(0,1)]public float applyPoint = .75f;
    private IEnumerator _routine;
    private Action _callback;
    private Action _applyCallback;

    [Button]
    public void Play(Action applyCallback, Action callback)
    {
        _applyCallback = applyCallback;
        _callback = callback;
        this.PlayCoroutine(ref _routine, AttackAnimation);
    }

    private IEnumerator AttackAnimation()
    {
        bool isReady = true;
        float t = 0;
        do
        {
            t += Time.deltaTime / duration;
            target.localScale = Vector3.one * attackScaleCurve.Evaluate(t);
            if (isReady && t > applyPoint)
            {
                isReady = false;
                CameraController.Shake();
                _applyCallback?.Invoke();
            }
            yield return null;
        } while (t<1);
        _callback?.Invoke();
    }
}