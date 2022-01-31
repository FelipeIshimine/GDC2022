using System;
using Sirenix.OdinInspector;

internal class WinState : AsyncState
{
    private readonly Action _continueCallback;

    public WinState(Action continueCallback)
    {
        _continueCallback = continueCallback;
    }

    protected override void Enter()
    {
        Canvas_Gameplay.Win();
        Canvas_Gameplay.OnNextRequest += Continue;
    }

    protected override void Exit()
    {
        Canvas_Gameplay.OnNextRequest -= Continue;
    }

    [Button] private void Continue() => _continueCallback.Invoke();
}