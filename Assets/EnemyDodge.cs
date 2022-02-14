using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class EnemyDodge : MonoBehaviour
{
    public float duration = .2f;
    public AnimationCurve movementCurve;
    public Transform target;

    public AnimationCurve textPositionCurve;
    public AnimationCurve textAlphaCurve;
    public SpriteRenderer spriteRenderer;
    public int flickeringCount = 5;

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
        Vector3 startPosition = target.localPosition;
        
        do
        {
            t += Time.deltaTime / duration;
            spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(t*flickeringCount,1)+.2f);
            text.transform.localPosition = Vector3.up * textPositionCurve.Evaluate(t);
            text.color = new Color(1, 1, 1, textAlphaCurve.Evaluate(t));
            target.localPosition = startPosition + Vector3.right * movementCurve.Evaluate(t);
            yield return null;
            
        } while (t<1);
        
        
    }
}