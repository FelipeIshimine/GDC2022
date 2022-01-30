using System;
using Sirenix.OdinInspector;
using UnityEngine;

internal class CoinSelectionState : AsyncState
{
    private readonly DeckBattleData _deckBattleData;
    [ShowInInspector] private readonly Action<int> _coinSelectedCallback;
    [ShowInInspector] private readonly Action<int> _coinDiscardCallback;

    [ShowInInspector] private Coin[] _availableCoins;
    
    public CoinSelectionState(DeckBattleData deckBattleData, int handSize, Action<int> coinSelectedCallback, Action<int> coinDiscardCallback)
    {
        _deckBattleData = deckBattleData;
        _coinSelectedCallback = coinSelectedCallback;
        _coinDiscardCallback = coinDiscardCallback;

        int count = Mathf.Min(deckBattleData.Deck.Count, handSize);
        
        _availableCoins = new Coin[count];
        for (int i = 0; i < count; i++)
            _availableCoins[i] = deckBattleData.Deck[i];
    }

    protected override void Enter()
    {
        HandContainerUI.OnPlayRequest += PlayCoin;
        HandContainerUI.OnDiscardRequest += DiscardCoin;
    }

    protected override void Exit()
    {
        HandContainerUI.OnPlayRequest -= PlayCoin;
        HandContainerUI.OnDiscardRequest -= DiscardCoin;
    }

    [Button] private void DiscardCoin(int obj) => _coinDiscardCallback(obj);

    [Button] public void PlayCoin(int index) => _coinSelectedCallback(index);
}