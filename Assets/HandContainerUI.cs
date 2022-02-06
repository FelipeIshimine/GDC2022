using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HandContainerUI : BaseMonoSingleton<HandContainerUI>
{
    [ShowInInspector] public static Action<int> OnDiscardRequest;
    [ShowInInspector] public static Action<int> OnPlayRequest;
    
    [ShowInInspector] private List<CoinSlot> slots;

    public GameObject proto;
    public Transform container;

    public Bounds playArea;
    public Bounds discardArea;

    protected override void Awake()
    {
        base.Awake();
        proto.gameObject.SetActive(false);
    }

    [Button]
    public void Initialize(int count)
    {
        slots ??= new List<CoinSlot>();

        int difference = count - slots.Count;

        for (int i = 0; i < difference; i++)
        {
            CoinSlot coinSlot = Instantiate(proto, container).GetComponent<CoinSlot>();
            slots.Add(coinSlot);
            coinSlot.gameObject.SetActive(true);
            coinSlot.Initialize();

            coinSlot.OnCoinDrop = OnCoinDrop;
            coinSlot.name = $"Slot {i}";
        }

        for (int i = difference; i < 0; i--)
        {
            slots[i].OnCoinDrop -= OnCoinDrop;
            Destroy(slots[i].gameObject);
            slots.RemoveAt(i);
        }
    }

    private void OnCoinDrop(CoinSlot slot, Vector3 position)
    {
        if (IsInsidePlayArea(position))
            PlayFrom(slot);
        else if (IsInsideDiscardArea(position))
            DiscardFrom(slot);
        else
            slot.ResetCoinPosition();
    }

    private void DiscardFrom(CoinSlot slot)
    {
        slot.HideCoin();
        OnDiscardRequest?.Invoke(slots.IndexOf(slot));
        
    }

    private void PlayFrom(CoinSlot slot)
    {
        slot.HideCoin();
        OnPlayRequest?.Invoke(slots.IndexOf(slot));
    }
    
    [Button] private bool IsInsideDiscardArea(Vector3 worldPosition) => discardArea.Contains(worldPosition);

    [Button] private bool IsInsidePlayArea(Vector3 position)=> playArea.Contains(position);

    public void Set(List<Coin> coins)
    {
        for (int i = 0; i < coins.Count; i++)
        {
            slots[i].SetCoin(coins[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.green.r,Color.green.g,Color.green.b, .2f);
        Gizmos.DrawCube(playArea.center,playArea.size);
        Gizmos.color = new Color(Color.red.r,Color.red.g,Color.red.b, .2f);
        Gizmos.DrawCube(discardArea.center,discardArea.size);
    }
}
