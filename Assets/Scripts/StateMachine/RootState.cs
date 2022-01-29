using System;
using System.Collections;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootState : AsyncState
{
    private static RootState _instance; 
    public static void Initialize()
    {
        _instance = new RootState();
        
        _instance.InitializeAsRootAsync();
    }

    protected override void Enter()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate * 2;
        RuntimeScriptableSingletonInitializer.WhenInitializationIsDone(GoToMainMenu);
    }

    protected override void Exit()
    {
    }

    private void GoToMainMenu()
    {
        SwitchState(new MainMenuState(GoToMainGame,Quit));
    }
    
    private void GoToMainGame()
    {
        SwitchState(new MainGameState(GoToMainMenu,Quit));
    }

    private void Quit()
    {
        Application.Quit();
    }
}

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
    }

    protected override void Exit()
    {
    }

    [Button]
    public void GoToMainGameState() => _gameCallback?.Invoke();
}
