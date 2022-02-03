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

    private int _selectedFace;
    private Coin _selectedCoin;
    
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
        HandContainerUI.SetActive(false);
        Debug.Log(">>>PlayCoinState");

        var hand = _deckBattleData.Hand;
        _selectedCoin = hand.Take(_coinIndex, false);
        
        _selectedFace = UnityEngine.Random.Range(0, 2);
        Debug.Log($"Selected coin:{_selectedFace}");
 
        CoinFlipUI.Instance.SetCoin(_selectedCoin);
        CoinFlipUI.Instance.FlipCoin(_selectedFace==0, AnimationDone);

    }

    void AnimationDone()
    {
        ApplyEffect();
        Done();
    }

    private void ApplyEffect()
    {
        _deckBattleData.Used.Add(_selectedCoin);
        
        Debug.Log($"Selected{_selectedFace} Head:{_selectedCoin.headEffect} Tail:{_selectedCoin.tailEffect}");
        
        var effect = _selectedFace == 0 ? _selectedCoin.headEffect : _selectedCoin.tailEffect;
        effect.Apply(_source, _target, _selectedCoin.tier);
    }

    protected override void Exit()
    {
    }

    [Button] private void Done()
    {
        HandContainerUI.SetActive(true);
        _coinPlayedCallback?.Invoke();
    }
}