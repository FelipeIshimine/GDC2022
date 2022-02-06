using System;

public class TurnState : AsyncState
{
    public enum TurnType { Player, Enemy }
    private readonly TurnType _turnType;
    private readonly BattleUnit[] _units;
    private readonly DeckBattleData _deckBattleData;
    private readonly Action _onDoneCallback;
    private readonly Action _onQuitCallback;
        
    public TurnState(BattleUnit[] units, TurnType turnType, DeckBattleData deckBattleData,Action onQuitCallback, Action onDoneCallback)
    {
        _units = units;
        _turnType = turnType;
        _deckBattleData = deckBattleData;
        _onQuitCallback = onQuitCallback;
        _onDoneCallback = onDoneCallback;
    }

    protected override void Enter()
    {
        if (_turnType == TurnType.Enemy)
            SwitchState(new EnemyTurnState(_units, _units[0], (Enemy)_units[1], _onQuitCallback,  _onDoneCallback));
        else
            SwitchState(new PlayerTurnState(_units, _units[0], (Enemy)_units[1], _deckBattleData, _onQuitCallback, _onDoneCallback));
    }

    protected override void Exit()
    {
        foreach (BattleUnit battleUnit in _units)
        {
            battleUnit.TurnEnd();
        }

    }
}