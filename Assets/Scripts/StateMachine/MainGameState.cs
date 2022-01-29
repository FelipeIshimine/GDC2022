using System;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class MainGameState : AsyncState
{
    private readonly Action _backToMenuCallback;
    private readonly Action _quitCallback;
    [ShowInInspector] private PlayerData _playerData;

    public MainGameState(Action backToMenuCallback, Action quitCallback) : base(ScenesSettings.MainGame, LoadSceneMode.Single)
    {
        _backToMenuCallback = backToMenuCallback;
        _quitCallback = quitCallback;
    }

    protected override void Enter()
    {
        _playerData = new PlayerData()
        {
            LevelId = 0,
            Deck = DeckManager.CreateDefaultDeck(),
            Stats = StatsManager.CreateDefaultStats()
        };
    }

    protected override void Exit()
    {
    }

    [Button] private void GoToShop() => SwitchState(new ShopState(GoToNextBattle));

    [Button] private void GoToNextBattle()
    {
        SwitchState(new BattleState(_playerData, _backToMenuCallback, GoToShop));
    }
}