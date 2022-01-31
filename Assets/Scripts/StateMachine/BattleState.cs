using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class BattleState : AsyncState
{
    private int PlayerHealth => _playerData.Health;

    private bool DidLose => PlayerHealth <= 0 || (_deckBattleData.IsDeckEmpty() && _deckBattleData.IsDiscardEmpty()); 

    private bool DidWin => _enemy.IsDead();

    private readonly Action _backToMenuCallback;
    private readonly Action _continueCallback;
    
    private readonly PlayerData _playerData;
    [ShowInInspector] private readonly BattleUnit _playerUnit;
    [ShowInInspector] private readonly Enemy _enemy;
    private readonly BattleLevel _battleLevel;

    private readonly DeckBattleData _deckBattleData;

    private int LevelId => _playerData.LevelId;
    
    public BattleState(PlayerData playerData, Action backToMenuCallback, Action continueCallback) : base(ScenesSettings.MainGame, LoadSceneMode.Single)
    {
        _playerData = playerData;
        _backToMenuCallback = backToMenuCallback;
        _continueCallback = continueCallback;
        _deckBattleData = new DeckBattleData(_playerData.Deck);
        
        _battleLevel = LevelsManager.BattleLevels[LevelId];

        _playerUnit = new BattleUnit(_playerData.Stats);
        _enemy = new Enemy(_battleLevel.enemyPreset);
    }

    protected override void Enter()
    {
        EnemyEntity.Instance.Initialize(_enemy);

        _playerUnit.OnStatsModify += OnPlayerStatsModify;
        
        GoToPlayerTurnState();
    }

    protected override void Exit()
    {
        _playerUnit.OnStatsModify -= OnPlayerStatsModify;
    }

    [Button] private void GoToPlayerTurnState()
    {
        if (TryEndBattle()) return;
        SwitchState(new PlayerTurnState(_playerUnit, _enemy, _deckBattleData, GoToEnemyTurnState, _backToMenuCallback));
    }
    
    [Button] private void GoToEnemyTurnState()
    {
        if (TryEndBattle()) return;
     
        Debug.Log("Enemy Turn");
        SwitchState(new EnemyTurnState(_playerUnit, _enemy, GoToPlayerTurnState));
    }
    
    private bool TryEndBattle()
    {
        if (DidWin)
        {
            GoToWin();
            return true;
        }
        if (DidLose)
        {
            GoToLose();
            return true;
        }
        return false;
    }
    
    [Button] private void GoToWin()
    {
        
        SwitchState(new WinState(_continueCallback));
    }
    
    [Button] private void GoToLose()
    {
        SwitchState(new LoseState(_backToMenuCallback));
    }

    private void OnPlayerStatsModify(string arg1, int arg2)
    {
        Debug.Log("Refresh");
        Canvas_Gameplay.Refresh(_playerUnit, _deckBattleData);
    }
    
    //public void GoToPlaying() => SwitchState(new PlayingState(GoToPause));
    //public void GoToPause() => SwitchState(new PauseState(GoToPlaying));
}