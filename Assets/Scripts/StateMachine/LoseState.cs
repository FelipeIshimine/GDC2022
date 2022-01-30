using System;
using Sirenix.OdinInspector;

internal class LoseState : AsyncState
{
    private readonly Action _menuCallback;

    public LoseState(Action menuCallback)
    {
        _menuCallback = menuCallback;
    }

    protected override void Enter()
    {
        Canvas_Gameplay.Lose();
        Canvas_Gameplay.OnRetryRequest += BackToMenu;
    }

    protected override void Exit()
    {
        Canvas_Gameplay.OnRetryRequest -= BackToMenu;
    }
    [Button] private void BackToMenu() => _menuCallback.Invoke();
}