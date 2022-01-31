using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

internal class PlayerTurnState : AsyncState
{
    private readonly Action _endTurnCallback;
    private readonly Action _backToMenuCallback;
    [ShowInInspector]private readonly DeckBattleData _deckBattleData;

    private readonly BattleUnit _playerUnit;
    private readonly Enemy _enemyUnit;

    private int _selectedCoin = -1;

    public PlayerTurnState(BattleUnit playerUnit, Enemy enemyUnit, DeckBattleData deckBattleData, Action endTurnCallback, Action backToMenuCallback)
    {
        _enemyUnit = enemyUnit;
        _playerUnit = playerUnit;
        _deckBattleData = deckBattleData;
        _endTurnCallback = endTurnCallback;
        _backToMenuCallback = backToMenuCallback;
    }
   
    protected override void Enter()
    {
        RefreshHand();

        _enemyUnit.SelectEffect();
        
        _playerUnit.Set(StatsManager.ActionPoints, _playerUnit.MaxActionPoints);

        GoToCoinSelectionState();
    }

    private void RefreshHand()
    {
        var handContainer = Canvas_Gameplay.Instance.handContainer;
        handContainer.Initialize(_playerUnit.HandSize);
        var hand = _deckBattleData.Hand;
        int difference = _playerUnit.HandSize - hand.Count;
        for (int i = 0; i < difference; i++)
            hand.Add(null);
        for (int i = difference; i < 0; i--)
            hand.RemoveAt(hand.Count - 1);
        for (int i = 0; i < hand.Count; i++)
            hand[i] ??= _deckBattleData.TakeNextDeckCard();
        handContainer.Set(hand);
    }

    protected override void Exit()
    {
    }
    
    private void GoToCoinSelectionState()
    {
        if(_enemyUnit.Health <= 0)
            EndTurn();
        else
            SwitchState(new CoinSelectionState(_deckBattleData, _playerUnit.HandSize, CoinSelected, CoinDiscard));
    }

    private void CoinDiscard(int index)
    {
        Debug.Log("COIN DISCARD");
        _playerUnit.Modify(StatsManager.ActionPoints,-1);
        
        var discardedCoin = _deckBattleData.Hand.Take(index, false); //Posision anulada
        _deckBattleData.Discarded.Add(discardedCoin);

        _deckBattleData.Hand[index] = _deckBattleData.TakeNextDeckCard();
        
        RefreshHand();
        
        GoToCoinSelectionState();
    }

    [Button]
    private void CoinSelected(int coinIndex)
    {
        Debug.Log("COIN SELECTED");
        
        _playerUnit.Modify(StatsManager.ActionPoints,-1);
        _selectedCoin = coinIndex;
        SwitchState(new PlayCoinState(_playerUnit, _enemyUnit, _deckBattleData, _selectedCoin, CoinPlayed));
    }

    void CoinPlayed()
    {
        
        
        
        if (_playerUnit.ActionPoints < 0)
            throw new Exception("Action points below 0");

        if (_playerUnit.ActionPoints == 0)
        {
            Debug.Log("EndTurn");
            EndTurn();
        }
        else
        {
            Debug.Log("Continue turn");
            GoToCoinSelectionState();
        }
    }
        
    [Button] protected void EndTurn() => _endTurnCallback.Invoke();
    [Button] protected void BackToMenu() => _backToMenuCallback.Invoke();
}

internal class TargetSelection : AsyncState
{
    private readonly List<BattleUnit> _units;
    private readonly Coin _coin;
    private readonly Action<int> _targetSelected;

    public TargetSelection(List<BattleUnit> units, Coin coin, Action<int> targetSelected)
    {
        _units = units;
        _coin = coin;
        _targetSelected = targetSelected;
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    [Button] public void SelectUnit(int index) => _targetSelected.Invoke(index);
}