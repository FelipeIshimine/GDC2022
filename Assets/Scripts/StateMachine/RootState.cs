using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

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
        SwitchState(new MainGameState(GoToMainGame,Quit));
    }

    private void Quit()
    {
        Application.Quit();
    }
}