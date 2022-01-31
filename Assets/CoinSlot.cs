using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoinSlot : MonoBehaviour
{
    [ShowInInspector] public Action<CoinSlot,Vector3> OnCoinDrop;
    
    [SerializeField] private Transform spawnPoint;

    public float height = 2;

    [SerializeField] private AnimationCurve curveIn;
    [SerializeField] private float durationIn = .4f;

    [SerializeField] private AnimationCurve curveOut;
    [SerializeField] private float durationOut = .4f;
    
    private IEnumerator _routine;

    [SerializeField] private CoinEntity coinEntity;

    private void CoinDropped()
    {
        OnCoinDrop?.Invoke(this, coinEntity.transform.position);
    }
    
    public void Initialize()
    {
        coinEntity.gameObject.SetActive(false);
        ResetCoinPosition();
    }

    public void SetCoin(Coin coin)
    {
        Debug.Log($"{this} SetCoin({coin})");
        coinEntity.Initialize(coin, CoinDropped);
        coinEntity.gameObject.SetActive(true);
        ResetCoinPosition();
    }

    public void CoinHover(bool value)
    {
        if(value)
            this.PlayCoroutine(ref _routine, HoverIn);
        else
            this.PlayCoroutine(ref _routine, HoverOut);
    }

    IEnumerator HoverIn()
    {
        float t = 0;
        Vector3 startPosition = coinEntity.transform.localPosition;
        Vector3 endPosition = new Vector3(0, height, 0);
        do
        {
            t += Time.deltaTime / durationIn;
            coinEntity.transform.localPosition = Vector3.Lerp(startPosition,endPosition, curveIn.Evaluate(t));
            yield return null;
        } while (t<1);
    }

    IEnumerator HoverOut()
    {
        float t = 0;
        Vector3 startPosition = coinEntity.transform.localPosition;
        Vector3 endPosition = new Vector3(0, 0, 0);
        do
        {
            t += Time.deltaTime / durationOut;
            coinEntity.transform.localPosition = Vector3.Lerp(startPosition,endPosition, curveOut.Evaluate(t));
            yield return null;
        } while (t<1);
    }



    public void ResetCoinPosition()
    {
        coinEntity.transform.SetParent(spawnPoint);
        coinEntity.transform.localScale = Vector3.one;
        coinEntity.transform.localPosition = Vector3.zero;
    }

    public void HideCoin()
    {
        coinEntity.gameObject.SetActive(false);
    }
}