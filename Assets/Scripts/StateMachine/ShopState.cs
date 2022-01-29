using System;
using Sirenix.OdinInspector;

public class ShopState : AsyncState
{
    private readonly Action _callback;
    public ShopState(Action callback)
    {
        _callback = callback;
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    [Button] private void Done() => _callback?.Invoke();
}