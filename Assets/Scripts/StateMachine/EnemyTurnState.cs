using System;
using Sirenix.OdinInspector;

internal class EnemyTurnState : AsyncState
{
    private readonly BattleUnit _player;
    private readonly Enemy _enemy;
    private readonly Action _endTurnCallback;

    public EnemyTurnState(BattleUnit player, Enemy enemy, Action endTurnCallback)
    {
        _player = player;
        _enemy = enemy;
        _endTurnCallback = endTurnCallback;
    }

    protected override void Enter()
    {
        _enemy.SelectedEffect.Apply(_enemy,_player, _enemy.SelectedTier);
        _enemy.ClearEffect();
        EndTurn();
    }

    protected override void Exit()
    {
    }
    
    
    [Button] protected void EndTurn() => _endTurnCallback.Invoke();
    
}