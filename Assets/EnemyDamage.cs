using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float duration = .2f;
    public AnimationCurve scaleCurve;
    public Transform target;

    public SpriteRenderer spriteRenderer;
    public int flickeringCount = 5;
    
    private IEnumerator _routine;

    [Button]
    public void Play()
    {
        this.PlayCoroutine(ref _routine, DamageAnimation);
    }

    private IEnumerator DamageAnimation()
    {
        float t = 0;
        do
        {
            t += Time.deltaTime / duration;
            spriteRenderer.color = new Color(1, 1, 1, Mathf.RoundToInt(Mathf.PingPong(t*flickeringCount, 1)));
            target.localScale = Vector3.one * scaleCurve.Evaluate(t);
            yield return null;
        } while (t<1);
    }
}