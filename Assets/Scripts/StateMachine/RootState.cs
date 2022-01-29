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

public class MainGameState : AsyncState
{
    private readonly Action _backToMenuCallback;
    private readonly Action _quitCallback;
    private PlayerData _playerData;

    public MainGameState(Action backToMenuCallback, Action quitCallback) : base(ScenesSettings.MainGame, LoadSceneMode.Single)
    {
        _backToMenuCallback = backToMenuCallback;
        _quitCallback = quitCallback;
    }

    protected override void Enter()
    {
        _playerData = new PlayerData()
        {
            BattleId = 0,
            Deck = DeckManager.CreateDefaultDeck(),
            Stats = StatsManager.CreateDefaultStats()
        };
    }

    protected override void Exit()
    {
    }

    [Button] private void GoToShop() => SwitchState(new ShopState(GoToNextBattle));

    [Button] private void GoToNextBattle()
    {
        SwitchState(new BattleState(_playerData, _backToMenuCallback, GoToShop));
    }
}

internal class BattleState : AsyncState
{
    private int _playerHealth;
    private int _enemyCount;

    private bool DidLose => _playerHealth <= 0;

    private bool DidWin => _enemyCount == 0;

    private readonly Action _backToMenuCallback;
    private readonly Action _continueCallback;
    private PlayerData _playerData;
    
    public BattleState(PlayerData playerData, Action backToMenuCallback, Action continueCallback) : base(ScenesSettings.Levels[playerData.BattleId], LoadSceneMode.Additive)
    {
        _playerData = playerData;
        _backToMenuCallback = backToMenuCallback;
        _continueCallback = continueCallback;
    }

    protected override void Enter()
    {
        GoToPlayerTurnState();
    }

    protected override void Exit()
    {
    }

    [Button] private void GoToPlayerTurnState()
    {
        if (TryEndBattle()) return;
        SwitchState(new PlayerTurnState(_playerData, GoToEnemyTurnState, _backToMenuCallback));
    }
    
    [Button] private void GoToEnemyTurnState()
    {
        if (TryEndBattle()) return;
        SwitchState(new EnemyTurnState(GoToPlayerTurnState));
    }
    
    private bool TryEndBattle()
    {
        if (DidWin)
        {
            GoToWin();
            return true;
        }
        if (DidLose)
        {
            GoToLose();
            return true;
        }
        return false;
    }
    
    [Button] private void GoToWin()
    {
        SwitchState(new WinState(_continueCallback));
    }
    
    [Button] private void GoToLose()
    {
        SwitchState(new LoseState(_backToMenuCallback));
    }

     //public void GoToPlaying() => SwitchState(new PlayingState(GoToPause));
     //public void GoToPause() => SwitchState(new PauseState(GoToPlaying));
}

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

internal class PlayerTurnState : AsyncState
    {
        private readonly Action _endTurnCallback;
        private readonly Action _backToMenuCallback;
        private readonly PlayerData _playerData;

        public PlayerTurnState(PlayerData playerData, Action endTurnCallback, Action backToMenuCallback)
        {
            _playerData = playerData;
            _endTurnCallback = endTurnCallback;
            _backToMenuCallback = backToMenuCallback;
        }

        protected override void Enter()
        {
            SwitchState(new CoinSelectionState(_playerData, CoinSelected));
        }

        protected override void Exit()
        {
        }

        private void CoinSelected(Coin coin) => SwitchState(new PlayCoinState(coin, CoinPlayed));

        void CoinPlayed()
        {
            if (_playerData.ActionPoints < 0)
                throw new Exception("Action points below 0");
            
            if (_playerData.ActionPoints == 0)
            {
                EndTurn();
            }
        }
        
        [Button] protected void EndTurn() => _endTurnCallback.Invoke();
        [Button] protected void BackToMenu() => _backToMenuCallback.Invoke();
    }

internal class PlayCoinState : AsyncState
{
    private readonly Coin _coin;
    private readonly Action _coinPlayedCallback;
    public PlayCoinState(Coin coin, Action coinPlayedCallback)
    {
        _coin = coin;
        _coinPlayedCallback = coinPlayedCallback;
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }

    [Button] private void Done() => _coinPlayedCallback?.Invoke();

}

internal class CoinSelectionState : AsyncState
{
    private readonly PlayerData _playerData;
    private readonly Action<Coin> _coinSelectedCallback;

    public CoinSelectionState(PlayerData playerData, Action<Coin> coinSelectedCallback)
    {
        _playerData = playerData;
        _coinSelectedCallback = coinSelectedCallback;
    }

    protected override void Enter()
    {
    }

    protected override void Exit()
    {
    }
}

internal class EnemyTurnState : AsyncState
    {
        private readonly Action _endTurnCallback;

        public EnemyTurnState(Action endTurnCallback)
        {
            _endTurnCallback = endTurnCallback;
        }

        protected override void Enter()
        {
        }

        protected override void Exit()
        {
        }
        [Button] protected void EndTurn() => _endTurnCallback.Invoke();
    }

/*
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
        private readonly Action _resumeCallback;

        public PauseState(Action callback, Action resumeCallback)
        {
            _resumeCallback = resumeCallback;
        }

        protected override void Enter()
        {
        }

        protected override void Exit()
        {
        }
    }

*/