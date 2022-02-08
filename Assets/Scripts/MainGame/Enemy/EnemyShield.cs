using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private SpriteRenderer shineRender;
    [SerializeField] private Color activeShieldColor;
    
    [Header("Appear")]
    [FoldoutGroup("Animations"), SerializeField] private float appearDuration = .5f; 
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve appearScaleCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve appearAlphaCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve appearShineAlphaCurve;

    [Header("Disappear")]
    [FoldoutGroup("Animations"), SerializeField] private float disappearDuration = .5f; 
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve disappearScaleCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve disappearAlphaCurve;
    
    [Header("Block")]
    [FoldoutGroup("Animations"), SerializeField] private float blockDuration = .5f; 
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve blockScaleCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve blockShineAlphaCurve;
    
    [Header("Break")]
    [FoldoutGroup("Animations"), SerializeField] private float breakDuration = .5f; 
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve breakScaleCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve breakAlphaCurve;
    [FoldoutGroup("Animations"), SerializeField] private AnimationCurve breakShineAlphaCurve;
    [FoldoutGroup("Animations"), SerializeField] private ParticleSystem breakParticles;
    
    private IEnumerator _routine;

    [Button]
    public void PlayAppearAnimation() => this.PlayCoroutine(ref _routine, AppearAnimation);
    IEnumerator AppearAnimation()
    {
        Debug.Log("Appear".ApplyColor(UnityStringExtensions.StringColor.White));
        float t = 0;

        Color startColor = render.color;
        Color endColor = activeShieldColor;
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
        Debug.Log("DisappearAnimation".ApplyColor(UnityStringExtensions.StringColor.White));
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
        Debug.Log("BlockAnimation".ApplyColor(UnityStringExtensions.StringColor.White));
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
        Debug.Log("BreakAnimation".ApplyColor(UnityStringExtensions.StringColor.White));
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
