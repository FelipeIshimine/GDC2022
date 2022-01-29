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
    }

    protected override void Exit()
    {
    }

    [Button] private void Continue() => _continueCallback.Invoke();
}