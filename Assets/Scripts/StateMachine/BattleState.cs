using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

internal class BattleState : AsyncState
{
    private int PlayerHealth => _playerData.Health;

    private bool DidLose => PlayerHealth <= 0;

    private bool DidWin => _enemy.IsDead();

    private readonly Action _backToMenuCallback;
    private readonly Action _continueCallback;
    
    private readonly PlayerData _playerData;

    private readonly BattleUnit _playerUnit;
    private readonly Enemy _enemy;
    private readonly BattleLevel _battleLevel;

    private readonly DeckBattleData _deckBattleData;

    private int LevelId => _playerData.LevelId;
    
    public BattleState(PlayerData playerData, Action backToMenuCallback, Action continueCallback) : base(ScenesSettings.Levels[playerData.LevelId], LoadSceneMode.Additive)
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
        GoToPlayerTurnState();
    }

    protected override void Exit()
    {
    }

    [Button] private void GoToPlayerTurnState()
    {
        if (TryEndBattle()) return;
        SwitchState(new PlayerTurnState(_playerUnit, _enemy, _deckBattleData, GoToEnemyTurnState, _backToMenuCallback));
    }
    
    [Button] private void GoToEnemyTurnState()
    {
        if (TryEndBattle()) return;
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

    //public void GoToPlaying() => SwitchState(new PlayingState(GoToPause));
    //public void GoToPause() => SwitchState(new PauseState(GoToPlaying));
}

internal class DeckBattleData
{
    public readonly List<Coin> Deck;
    public readonly List<Coin> Discarded = new List<Coin>();
    public readonly List<Coin> Used = new List<Coin>();

    public DeckBattleData(List<Coin> coins)
    {
        Deck = new List<Coin>(coins);
        for (int i = Deck.Count - 1; i >= 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i);
            (Deck[i], Deck[j]) = (Deck[i], Deck[j]);
        }
    }
}


