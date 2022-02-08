using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoinFlipUI : BaseMonoSingleton<CoinFlipUI>
{
    [Header("Flip animation")] 
    public float flipDuration = 1.2f;
    public float flipHeight = 3;
    public int haftRotations = 7;
    public AnimationCurve heightCurve;
    public AnimationCurve rotationCurve;
    private bool _head;

    public float endShowCoinDuration = 3;
    
    public float startShowCoinDuration = 1;
    public float showCoinDuration = 2.5f;
    public float startShowCoinPauseDuration = 1;
    public AnimationCurve showRotationCurve;

    private IEnumerator _routine;
    private Action _callback;
    [SerializeField] private CoinEntity coinEntity;
    
    public void SetCoin(Coin coin)
    {
        coinEntity.Initialize(coin, null);
    }
    
    [Button]
    public void FlipCoin(bool head, Action callback)
    {
        _callback = callback;
        _head = head;
        this.PlayCoroutine(ref _routine, FlipAnimation);
    }

    private IEnumerator FlipAnimation()
    {
        float t = 0;
        coinEntity.gameObject.SetActive(true);

        var coinTransform = coinEntity.transform;
        coinTransform.localRotation = Quaternion.identity;
        
        yield return new WaitForSeconds(startShowCoinDuration);

        Vector3 startRotation = new Vector3(0,0,0);
        Vector3 endRotation = new Vector3(0,0,180);
      
        do
        {
            t += Time.deltaTime / showCoinDuration;
            coinTransform.localRotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, showRotationCurve.Evaluate(t)));
            yield return null;
        } while (t<1);
        
        yield return new WaitForSeconds(startShowCoinPauseDuration);
        
        endRotation = new Vector3((_head?0:180),_head?0:180,0);
        startRotation = new Vector3(haftRotations*180*(_head?-1:1),_head?0:180,0);
        t = 0;
        do
        {
            t += Time.deltaTime / flipDuration;
            coinTransform.position = transform.position + new Vector3(0,  heightCurve.Evaluate(t) * flipHeight, 0);
            coinTransform.localRotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, rotationCurve.Evaluate(t)));
            yield return null;
        } while (t<1);


        yield return new WaitForSeconds(endShowCoinDuration);
        
        coinEntity.gameObject.SetActive(false);
        _callback?.Invoke();
    }
}
