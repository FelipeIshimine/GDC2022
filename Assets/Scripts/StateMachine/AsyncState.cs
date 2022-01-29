using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public abstract class AsyncState
{
    public static event Action<AsyncState> OnAnySwitchState;

    public AsyncState Root { get; private set; } = null;

    private AsyncState _parent = null;
    
    private AsyncState _current;

    protected InnerState State;

    public bool IsBusy => _current is { IsBusy: true } || 
                          State == InnerState.Entering ||
                          State == InnerState.Exiting;

    public bool IsReady => _current is { IsBusy: false } &&
                           State == InnerState.Active;

    private readonly AssetReference _singleSceneReference;

    private readonly AssetReference[] _sceneReferences = null;
    private readonly SceneInstance[] _sceneInstances = null;

    protected AsyncState() 
    {
        State = InnerState.Inactive;
    }

    protected AsyncState(AssetReference[] sceneReferences) : this()
    {
        _sceneReferences = sceneReferences;
        _sceneInstances = new SceneInstance[sceneReferences?.Length ?? 0];
    }

    protected AsyncState(AssetReference sceneReference, LoadSceneMode loadSceneMode) : this()
    {
        switch (loadSceneMode)
        {
            case LoadSceneMode.Single:
                _singleSceneReference = sceneReference;
                _sceneInstances = Array.Empty<SceneInstance>();
                break;
            case LoadSceneMode.Additive:
                _sceneReferences = new []{sceneReference};
                _sceneInstances = new SceneInstance[1];
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(loadSceneMode), loadSceneMode, null);
        }
        
    }
    
    

    protected enum InnerState
    {
        Inactive,
        Entering,
        Active,
        Exiting,
        Finished
    }
    
    protected async void SwitchState(AsyncState nState)
    {
        if (_current != null)
        {
            _current.State = InnerState.Exiting;
            _current.BaseExit();
            await _current.UnloadScenesAsync();
            _current.State = InnerState.Finished;
        }

        _current = nState;
        if (_current != null)
            await EnterState(_current);
        
        OnAnySwitchState?.Invoke(Root);
    }

    private async Task EnterState(AsyncState state)
    {
        state.State = InnerState.Entering;
        state._parent = this;
        state.Root = Root;
        await state.LoadScenesAsync();
        if(!(state is NullState))
            state.GoToNull();
        state.Enter();
        state.State = InnerState.Active;
    }

    protected async void InitializeAsRootAsync()
    {
        Root = this;
        await EnterState(this);
    }
    private void BaseExit()
    {
        if (_current != null)
        {
            _current.BaseExit();
            _current.Exit();
        }
    }

    
    /// <summary>
    /// DO NOT call outside of AsyncStateMachine class
    /// </summary>
    protected abstract void Enter();
    
    /// <summary>
    /// DO NOT call outside of AsyncStateMachine class
    /// </summary>
    protected abstract void Exit();

    private async Task LoadScenesAsync()
    {
        if (_singleSceneReference != null)
        {
            Debug.Log($"{this} Loading Single Scene Async");
            await Addressables.LoadSceneAsync(_singleSceneReference).Task;
        }
        
        if (_sceneReferences != null)
        {
            for (var index = 0; index < _sceneReferences.Length; index++)
            {
                Debug.Log($"{this} Loading SceneAsync {index+1}/{_sceneReferences.Length} ");
                _sceneInstances[index] = await Addressables.LoadSceneAsync(_sceneReferences[index], LoadSceneMode.Additive).Task;
            }
        }
    }

    private async Task UnloadScenesAsync()
    {
        if (_sceneReferences != null)
        {
            for (var index = 0; index < _sceneInstances.Length; index++)
            {
                Debug.Log($"{this} Unloading SceneAsync {index + 1}/{_sceneReferences.Length} ");
                
                await Addressables.UnloadSceneAsync(_sceneInstances[index], false).Task;
            }
        }
    }

    public Stack<AsyncState> GetStates()
    {
        Stack<AsyncState> asyncStates = new Stack<AsyncState>();
        GetStates(ref asyncStates);
        return asyncStates;
    }

    private void GetStates(ref Stack<AsyncState> stack)
    {
        stack.Push(this);
        _current?.GetStates(ref stack);
    }

    protected void GoToNull() => SwitchState(new NullState());
}