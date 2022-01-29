using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

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
        var selectedCoin = _deckBattleData.Deck.Take(_coinIndex);
        _deckBattleData.Used.Add(selectedCoin);

        int selectedFace = UnityEngine.Random.Range(0, 2);
        var effect = selectedFace == 0?selectedCoin.headEffect:selectedCoin.tailEffect;
        effect.Apply(_source, _target, selectedCoin.tier);
    }

    protected override void Exit()
    {
    }

    [Button] private void Done() => _coinPlayedCallback?.Invoke();

}