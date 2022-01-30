using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

internal class PlayCoinState : AsyncState
{
    private readonly int _coinIndex;
    private readonly Action _coinPlayedCallback;
    private readonly BattleUnit _source;
    private readonly BattleUnit _target;
    private readonly DeckBattleData _deckBattleData;
    
    public PlayCoinState(BattleUnit source, BattleUnit target, DeckBattleData deckBattleData, int coinIndex, Action coinPlayedCallback)
    {
        _source = source;
        _target = target;
        _deckBattleData = deckBattleData;
        _coinIndex = coinIndex;
        _coinPlayedCallback = coinPlayedCallback;
    }

    protected override void Enter()
    {
        Debug.Log(">>>PlayCoinState");
        var hand = _deckBattleData.Hand;

        Coin selectedCoin = hand.Take(_coinIndex, false);

        if (selectedCoin == null)
            Debug.Log("MONEDA NULA");
        else if (selectedCoin.headEffect == null || selectedCoin.tailEffect == null)
            Debug.Log("EFECTO DE MONEDA NULO");

        _deckBattleData.Used.Add(selectedCoin);

        int selectedFace = UnityEngine.Random.Range(0, 2);

        var effect = selectedFace == 0 ? selectedCoin.headEffect : selectedCoin.tailEffect;
        effect.Apply(_source, _target, selectedCoin.tier);

        Debug.Log($"Selected coin:{selectedFace}");

        Done();
        
    }

    private async void WaitAndDone()
    {
        await Task.Yield();
        Done();
    }
    
    protected override void Exit()
    {
    }

    [Button] private void Done() => _coinPlayedCallback?.Invoke();

}