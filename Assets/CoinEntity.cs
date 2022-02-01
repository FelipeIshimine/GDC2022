using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CoinEntity : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [ShowInInspector] private event Action CoinDroppedCallback;
    [ShowInInspector] public event Action OnDragBegin;
    
    
    [ShowInInspector] public event Action<bool> OnHover;

    [SerializeField]private Coin coin;
    public Coin Coin => coin;
 
    [SerializeField]private MeshRenderer meshRenderer;

    private Material _material;
    private Material Mat=> _material ??= meshRenderer.material;
    
    [ShowInInspector] public bool IsClicked { get; private set; }  = false;

    [ShowInInspector] private bool _isHover = false;
    private static readonly int BaseText = Shader.PropertyToID("BaseTex");
    private static readonly int IconHeadTex = Shader.PropertyToID("IconHeadTex");
    private static readonly int IconTailTex = Shader.PropertyToID("IconTailTex");

    public bool IsHover
    {
        get => _isHover && !IsClicked;
        private set => _isHover = value;
    }
    
    public void Initialize(Coin nCoin, Action coinDroppedCallback)
    {
        coin = nCoin;
        CoinDroppedCallback = coinDroppedCallback;
        
        SetBaseTexture(coin.tier);
        SetHeadTexture(coin.HeadCoinIcon);
        SetTailTexture(coin.TailCoinIcon);
    }

    [Button] private void SetBaseTexture(int index) => Mat.SetTexture(BaseText, CoinManager.GetCoinBaseTextureFromTier(index));
    [Button] private void SetHeadTexture(Texture texture) => Mat.SetTexture(IconHeadTex, texture);
    [Button] private void SetTailTexture(Texture texture) => Mat.SetTexture(IconTailTex, texture);
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDragBegin?.Invoke();
        Debug.Log("PointerDOwn");
        IsClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(">>>> PointerUp");

        if(!IsClicked)
            return;
        IsClicked = false;
        CoinDroppedCallback.Invoke();
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
        OnHover?.Invoke(true);
        InfoPanelUI.SetInfo(coin);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        IsHover = false;
        OnHover?.Invoke(false);
        InfoPanelUI.Clear();
    }

}


