using System;
using Sirenix.OdinInspector;
using UnityEngine;

internal class CoinSelectionState : AsyncState
{
    private readonly DeckBattleData _deckBattleData;
    private readonly Action _endTurnCallback;
    [ShowInInspector] private readonly Action<int> _coinSelectedCallback;
    [ShowInInspector] private readonly Action<int> _coinDiscardCallback;

    [ShowInInspector] private Coin[] _availableCoins;
    
    public CoinSelectionState(DeckBattleData deckBattleData, int handSize, Action endTurnCallback, Action<int> coinSelectedCallback, Action<int> coinDiscardCallback)
    {
        _deckBattleData = deckBattleData;
        _endTurnCallback = endTurnCallback;
        _coinSelectedCallback = coinSelectedCallback;
        _coinDiscardCallback = coinDiscardCallback;

        int count = Mathf.Min(deckBattleData.Deck.Count, handSize);
        
        _availableCoins = new Coin[count];
        for (int i = 0; i < count; i++)
            _availableCoins[i] = deckBattleData.Deck[i];
        
    }

    protected override void Enter()
    {
        CameraController.SetRaycast(true);
        Canvas_Gameplay.HandContainer.Enable();
        Canvas_Gameplay.SetCoinsLeft(_deckBattleData.Deck.Count);
        Canvas_Gameplay.OnEndTurnRequest += EndTurn;

        HandContainerUI.OnPlayRequest += PlayCoin;
        HandContainerUI.OnDiscardRequest += DiscardCoin;
    }

    protected override void Exit()
    {
        Canvas_Gameplay.HandContainer.Disable();
        
        Canvas_Gameplay.OnEndTurnRequest -= EndTurn;
        HandContainerUI.OnPlayRequest -= PlayCoin;
        HandContainerUI.OnDiscardRequest -= DiscardCoin;
        CameraController.SetRaycast(false);
    }

    private void EndTurn() => _endTurnCallback();

    [Button]
    private void DiscardCoin(int obj)
    {
        if(_deckBattleData.IsDeckEmpty() && _deckBattleData.IsDiscardEmpty())
            return;
        _coinDiscardCallback(obj);
    } 
    [Button] public void PlayCoin(int index) => _coinSelectedCallback(index);
}