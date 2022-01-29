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
    }

    protected override void Exit()
    {
    }
    [Button] private void BackToMenu() => _menuCallback.Invoke();
}