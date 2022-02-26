using System;
using Sirenix.OdinInspector;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class MainGameState : AsyncState
{
    private readonly Action _retryCallback;
    private readonly Action _quitCallback;
    [ShowInInspector] private PlayerData _playerData;

    public MainGameState(Action retryCallback, Action quitCallback) 
    {
        _retryCallback = retryCallback;
        _quitCallback = quitCallback;
    }

    protected override void Enter()
    {
        _playerData = new PlayerData(-1, DeckManager.CreateDefaultDeck(), StatsManager.CreateDefaultStats());
        GoToNextBattle();
    }

    protected override void Exit()
    {
    }

    [Button] private void GoToShop() => SwitchState(new ShopState(GoToNextBattle));

    [Button] private void GoToNextBattle()
    {
        _playerData.LevelId++;
        if (_playerData.LevelId >= 3)
            GoToEndingState();
        else
            SwitchState(new BattleState(_playerData, _retryCallback, GoToNextBattle));
    }

    private void GoToEndingState()
    {
        SwitchState(new EndingState());
    }
}

internal class EndingState : AsyncState
{
    public EndingState() : base(ScenesSettings.EndingScene, LoadSceneMode.Single)
    {
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }
}