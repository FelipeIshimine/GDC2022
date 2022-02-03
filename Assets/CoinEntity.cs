using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CoinEntity : MonoBehaviour/*, IPointerUpHandler, IPointerDownHandler*/
{
    [ShowInInspector] private event Action CoinDroppedCallback;
    [ShowInInspector] public event Action OnDragBegin;
    
    
    [ShowInInspector] public event Action<bool> OnHover;

    [SerializeField]private Coin coin;
    public Coin Coin => coin;
 
    [SerializeField]private MeshRenderer meshRenderer;

    private Material _material;
    private Material Mat=> _material ??= meshRenderer.material;
    
    [ShowInInspector] public bool IsPickedUp { get; private set; }  = false;

    [ShowInInspector] private bool _isHover = false;
    private static readonly int BaseText = Shader.PropertyToID("BaseTex");
    private static readonly int IconHeadTex = Shader.PropertyToID("IconHeadTex");
    private static readonly int IconTailTex = Shader.PropertyToID("IconTailTex");

    
    public bool IsHover
    {
        get => _isHover && !IsPickedUp;
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
    
    private void FixedUpdate()
    {
        if (IsPickedUp)
            PositionAtMouse();
    }

    private void PositionAtMouse()
    {
        float z = transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHover = true;
        OnHover?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHover = false;
        OnHover?.Invoke(false);
    }

    public void DropCoin() => CoinDroppedCallback?.Invoke();
/*
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
        if (IsPickedUp)
        {
            IsPickedUp = false;
            CoinDroppedCallback?.Invoke();
        }
    }
*/
    /*   public void PickUp()
       {
           IsPickedUp = true;
           PositionAtMouse();
       }*/
/*
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }*/
}


