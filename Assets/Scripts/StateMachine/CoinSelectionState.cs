using System;
using Sirenix.OdinInspector;
using UnityEngine;

internal class CoinSelectionState : AsyncState
{
    private readonly DeckBattleData _deckBattleData;
    private readonly Action<int> _coinSelectedCallback;

    [ShowInInspector] private Coin[] _availableCoins;
    
    public CoinSelectionState(DeckBattleData deckBattleData, int handSize, Action<int> coinSelectedCallback)
    {
        _deckBattleData = deckBattleData;
        _coinSelectedCallback = coinSelectedCallback;

        int count = Mathf.Min(deckBattleData.Deck.Count, handSize);
        
        _availableCoins = new Coin[count];
        for (int i = 0; i < count; i++)
            _availableCoins[i] = deckBattleData.Deck[i];
        
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    [Button] public void SelectCoin(int index) => _coinSelectedCallback(index);
}