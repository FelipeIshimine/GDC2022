using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [ShowInInspector] public Action<CoinSlot,Vector3> OnCoinDrop;
    
    [SerializeField] private Transform spawnPoint;

    public float height = 2;

    [SerializeField] private AnimationCurve curveIn;
    [SerializeField] private float durationIn = .4f;

    [SerializeField] private AnimationCurve curveOut;
    [SerializeField] private float durationOut = .4f;

    public float rotationSpeed = 180;
    private IEnumerator _routine;

    [SerializeField] private CoinEntity coinEntity;

    private void Awake()
    {
        coinEntity.OnDragBegin += CoinDragBegin;
    }

    private void OnDestroy()
    {
        coinEntity.OnDragBegin -= CoinDragBegin;
    }

    private void CoinDragBegin()
    {
        if (_routine != null)
        {
            Debug.Log("Stopped");
            StopCoroutine(_routine);
            coinEntity.transform.rotation = Quaternion.identity;
        }
    }

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
        if (value)
        {
            InfoPanelUI.SetInfo(coinEntity.Coin);
            this.PlayCoroutine(ref _routine, HoverIn);
        }
        else if(!coinEntity.IsClicked)
        {
            //+InfoPanelUI.Clear();
            this.PlayCoroutine(ref _routine, HoverOut);
        }
    }

    IEnumerator HoverIn()
    {
        float t = 0;
        Vector3 startPosition = coinEntity.transform.localPosition;
        Vector3 endPosition = new Vector3(0, 0, height);
        do
        {
            t += Time.deltaTime / durationIn;
            coinEntity.transform.localPosition = Vector3.Lerp(startPosition,endPosition, curveIn.Evaluate(t));
            coinEntity.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime,0));
            yield return null;
        } while (t<1);

        while (true)
        {
            coinEntity.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime,0));
            yield return null;
        }
    }

    IEnumerator HoverOut()
    {
        float t = 0;
        Vector3 startPosition = coinEntity.transform.localPosition;
        Vector3 endPosition = new Vector3(0, 0, 0);
        Quaternion startRotation = coinEntity.transform.rotation;
        do
        {
            t += Time.deltaTime / durationOut;
            coinEntity.transform.localPosition = Vector3.Lerp(startPosition,endPosition, curveOut.Evaluate(t));
            coinEntity.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, t);
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        CoinHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CoinHover(false);
    }
}