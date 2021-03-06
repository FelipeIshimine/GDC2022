using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class BattleState : AsyncState
{
    private int PlayerHealth => _playerUnit.Health;

    private bool DidLose => PlayerHealth <= 0 || (_deckBattleData.IsDeckEmpty() && _deckBattleData.IsDiscardEmpty()); 

    private bool DidWin => _enemy.IsDead();

    private readonly Action _backToMenuCallback;
    private readonly Action _continueCallback;
    
    private readonly PlayerData _playerData;
    [ShowInInspector] private readonly BattleUnit _playerUnit;
    [ShowInInspector] private readonly Enemy _enemy;
    private readonly BattleUnit[] _units;

    private readonly DeckBattleData _deckBattleData;

    private int _turn = 0;

    private int LevelId => _playerData.LevelId;
    
    public BattleState(PlayerData playerData, Action backToMenuCallback, Action continueCallback) : base(ScenesSettings.MainGame, LoadSceneMode.Single)
    {
        _playerData = playerData;
        _backToMenuCallback = backToMenuCallback;
        _continueCallback = continueCallback;
        _deckBattleData = new DeckBattleData(_playerData.Deck);
        
        var battleLevel = LevelsManager.BattleLevels[LevelId];

        _playerUnit = new BattleUnit(_playerData.Stats);
        _enemy = new Enemy(battleLevel.enemyPreset);
        _units = new[] { _playerUnit, _enemy };
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
        _turn++;
        SwitchState(new TurnState(_units, TurnState.TurnType.Player, _deckBattleData, _backToMenuCallback, GoToEnemyTurnState));
    }
    
    [Button] private void GoToEnemyTurnState()
    {
        if (TryEndBattle()) return;
        _turn++;
        SwitchState(new TurnState(_units, TurnState.TurnType.Enemy, _deckBattleData, _backToMenuCallback, GoToPlayerTurnState));
    }

    private bool TryEndBattle()
    {
        Debug.Log($"DidWin:{DidWin} DidLose:{DidLose} {PlayerHealth})");
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
        Canvas_Gameplay.HandContainer.Terminate();
        SwitchState(new WinState(_continueCallback));
    }
    
    [Button] private void GoToLose()
    {
        Canvas_Gameplay.HandContainer.Terminate();
        SwitchState(new LoseState(_backToMenuCallback));
    }

    private void OnPlayerStatsModify(string id, int amount)
    {
        Debug.Log("Refresh");
        Canvas_Gameplay.Refresh(_playerUnit, _deckBattleData);

        if (id == StatsManager.Health && amount == 0)
            EnemyEntity.Instance.miss.Play();
    }
    
    //public void GoToPlaying() => SwitchState(new PlayingState(GoToPause));
    //public void GoToPause() => SwitchState(new PauseState(GoToPlaying));
}