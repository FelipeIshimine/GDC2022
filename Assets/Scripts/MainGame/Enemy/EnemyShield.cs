using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public SpriteRenderer render;
    public SpriteRenderer shineRender;

    [Header("Appear")]
    [FoldoutGroup("Animations")] public float appearDuration = .5f; 
    [FoldoutGroup("Animations")] public AnimationCurve appearScaleCurve;
    [FoldoutGroup("Animations")] public AnimationCurve appearAlphaCurve;
    [FoldoutGroup("Animations")] public AnimationCurve appearShineAlphaCurve;

    [Header("Disappear")]
    [FoldoutGroup("Animations")] public float disappearDuration = .5f; 
    [FoldoutGroup("Animations")] public AnimationCurve disappearScaleCurve;
    [FoldoutGroup("Animations")] public AnimationCurve disappearAlphaCurve;
    
    [Header("Block")]
    [FoldoutGroup("Animations")] public float blockDuration = .5f; 
    [FoldoutGroup("Animations")] public AnimationCurve blockScaleCurve;
    [FoldoutGroup("Animations")] public AnimationCurve blockShineAlphaCurve;
    
    [Header("Break")]
    [FoldoutGroup("Animations")] public float breakDuration = .5f; 
    [FoldoutGroup("Animations")] public AnimationCurve breakScaleCurve;
    [FoldoutGroup("Animations")] public AnimationCurve breakAlphaCurve;
    [FoldoutGroup("Animations")] public AnimationCurve breakShineAlphaCurve;
    [FoldoutGroup("Animations")] public ParticleSystem breakParticles;
    
    private IEnumerator _routine;

    [Button]
    public void PlayAppearAnimation() => this.PlayCoroutine(ref _routine, AppearAnimation);
    IEnumerator AppearAnimation()
    {
        float t = 0;

        Color startColor = render.color;
        Color endColor = new Color(1, 1, 1, 1);
        Color endShineColor = new Color(1, 1, 1, 0);
        do
        {
            render.color = Color.Lerp(startColor, endColor, appearAlphaCurve.Evaluate(t));
            render.transform.localScale = Vector3.one * appearScaleCurve.Evaluate(t);
            shineRender.color = Color.Lerp(Color.white, endShineColor, appearShineAlphaCurve.Evaluate(t));
            t += Time.deltaTime / appearDuration;
            yield return null;
        } while (t<1);
    }
    
    [Button]
    public void PlayDisappearAnimation() => this.PlayCoroutine(ref _routine, DisappearAnimation);
    IEnumerator DisappearAnimation()
    {
        float t = 0;
        Color startColor = render.color;
        Color endColor = new Color(1, 1, 1, 0);
        do
        {
            render.color = Color.Lerp(startColor, endColor, disappearAlphaCurve.Evaluate(t));
            render.transform.localScale = Vector3.one * disappearScaleCurve.Evaluate(t);
            t += Time.deltaTime / disappearDuration;
            yield return null;
        } while (t<1);
    }
    
    [Button]
    public void PlayBlockAnimation() => this.PlayCoroutine(ref _routine, BlockAnimation);

    IEnumerator BlockAnimation()
    {
        float t = 0;
        Color endColor = new Color(1, 1, 1, 0);
        do
        {
            t += Time.deltaTime / blockDuration;
            shineRender.color = Color.Lerp(Color.white, endColor, blockShineAlphaCurve.Evaluate(t));
            render.transform.localScale = Vector3.one * blockScaleCurve.Evaluate(t);
            yield return null;
        } while (t<1);
    }
    
    [Button]
    public void PlayBreakAnimation() => this.PlayCoroutine(ref _routine, BreakAnimation);
    IEnumerator BreakAnimation()
    {
        float t = 0;
        Color endColor = new Color(1, 1, 1, 0);
        Color startShineColor = shineRender.color; 
        do
        {
            t += Time.deltaTime / breakDuration;
            render.transform.localScale = Vector3.one * breakScaleCurve.Evaluate(t);
            render.color = Color.Lerp(Color.white, endColor, breakAlphaCurve.Evaluate(t));
            shineRender.color = Color.Lerp(startShineColor, Color.white, breakShineAlphaCurve.Evaluate(t));
            yield return null;
        } while (t<1);

        shineRender.color = new Color(1, 1, 1, 0);
        breakParticles.Play();
    }
}
