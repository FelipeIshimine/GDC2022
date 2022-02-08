using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

internal class EnemyTurnState : AsyncState
{
    private readonly BattleUnit[] _units;
    private readonly BattleUnit _player;
    private readonly Enemy _enemy;
    private readonly Action _endTurnCallback;
    private readonly Action _quitCallback;

    public EnemyTurnState(BattleUnit[] units, BattleUnit player, Enemy enemy, Action quitCallback,Action endTurnCallback)
    {
        _units = units;
        _player = player;
        _enemy = enemy;
        _quitCallback = quitCallback;
        _endTurnCallback = endTurnCallback;
    }

    protected override async void Enter()
    {
        _enemy.Set(StatsManager.Defense, 0);
        _enemy.Set(StatsManager.Speed, 0);

        foreach (BattleUnit battleUnit in _units)
            battleUnit.TurnStarted();

        await Task.Yield();
        
        await _enemy.SelectedEffect.ApplyAsync(_enemy,_player, _enemy.SelectedTier);
        
        _enemy.ClearEffect();
        EndTurn();
    }
    
    
    

    protected override void Exit()
    {
    }
    
    
    [Button] protected void EndTurn() => _endTurnCallback.Invoke();
    
}