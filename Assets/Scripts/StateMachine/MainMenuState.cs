using System;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class MainMenuState : AsyncState
{
    private readonly Action _gameCallback;
    private readonly Action _quitCallback;
    public MainMenuState(Action gameCallback, Action quitCallback) : base(ScenesSettings.MainMenu, LoadSceneMode.Single)
    {
        _gameCallback = gameCallback;
        _quitCallback = quitCallback;
    }

    protected override void Enter()
    {
        Canvas_MainMenu.OnPlayPressed += GoToMainGameState;
    }

    protected override void Exit()
    {
        Canvas_MainMenu.OnPlayPressed -= GoToMainGameState;
    }

    [Button]
    public void GoToMainGameState() => _gameCallback?.Invoke();
}