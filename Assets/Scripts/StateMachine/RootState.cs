using System;
using System.Collections;
using System.Collections.Generic;
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
    }

}

public class MainScreenState : AsyncState
{
    private readonly Action _completeCallback;
    public MainScreenState(Action completeCallback) : base(ScenesSettings.MainMenu, LoadSceneMode.Single)
    {
        _completeCallback = completeCallback;
    }

    protected override void Enter()
    {
    }

    private void DisableEveryCanvas()
    {
        Debug.Log("DisableEveryCanvas");
        var allCanvas = UnityEngine.Object.FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvas)
            canvas.gameObject.SetActive(false);
    }

    protected override void Exit()
    {
    }

    [Button] private void LevelCompleted()
    {
        _completeCallback?.Invoke();
    }
    
    [Button]  public void GoToShop() => SwitchState(new ShopState());

    public void GoToPlaying() => SwitchState(new PlayingState(GoToPause));
    public void GoToPause() => SwitchState(new PauseState(GoToPlaying));
}


public class PlayingState : AsyncState
{
    private readonly Action _pauseCallback;
    public PlayingState(Action callback)
    {
        _pauseCallback = callback;
    }
    
    protected override void Enter()
    {
        
    }

    protected override void Exit()
    {
    }
}

public class PauseState : AsyncState
{
    private readonly Action _pauseCallback;
    public PauseState(Action callback)
    {
    }
    
    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }
}





public class ShopState : AsyncState
{
    public ShopState() : base()
    {
    }
    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }
    
    
}

