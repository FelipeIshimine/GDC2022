using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class EnemyMiss : MonoBehaviour
{
    public float duration = .2f;
    public AnimationCurve textCurve;
    public AnimationCurve alphaCurve;
    public TextMeshPro text;
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
            text.color = new Color(1, 1, 1, alphaCurve.Evaluate(t));
            text.transform.localPosition = Vector3.up * textCurve.Evaluate(t);
            yield return null;
        } while (t<1);
    }
}