using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CoinEntity : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [ShowInInspector]  private event Action _coinDroppedCallback;

    public Coin coin;
    
    [ShowInInspector] public bool IsClicked { get; private set; }  = false;

    [ShowInInspector]    private bool _isHover = false;
    public bool IsHover
    {
        get => _isHover && !IsClicked;
        private set => _isHover = value;
    }

    public void Initialize(Coin nCoin, Action coinDroppedCallback)
    {
        coin = nCoin;
        _coinDroppedCallback = coinDroppedCallback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDOwn");
        IsClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(">>>> PointerUp");

        if(!IsClicked)
            return;
        IsClicked = false;
        
        _coinDroppedCallback.Invoke();
    }

    private void FixedUpdate()
    {
        if (IsClicked)
        {
            float z = transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        IsHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        IsHover = false;
    }
}


