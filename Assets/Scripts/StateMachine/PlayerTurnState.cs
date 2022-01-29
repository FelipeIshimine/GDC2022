using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

internal class PlayerTurnState : AsyncState
{
    private readonly Action _endTurnCallback;
    private readonly Action _backToMenuCallback;
    private readonly DeckBattleData _deckBattleData;

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
        _enemyUnit.SelectEffect();
        GoToCoinSelectionState();
    }

    private void GoToCoinSelectionState()
    {
        SwitchState(new CoinSelectionState(_deckBattleData, _playerUnit.HandSize, CoinSelected));
    }

    protected override void Exit()
    {
    }

    private void CoinSelected(int coinIndex)
    {
        _selectedCoin = coinIndex;
        SwitchState(new PlayCoinState(_playerUnit, _enemyUnit, _deckBattleData, _selectedCoin, CoinPlayed));
    }

    void CoinPlayed()
    {
        if (_playerUnit.ActionPoints < 0)
            throw new Exception("Action points below 0");
            
        if (_playerUnit.ActionPoints == 0)
            EndTurn();
        else
            GoToCoinSelectionState();
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