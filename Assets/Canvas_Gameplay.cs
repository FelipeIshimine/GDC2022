using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Gameplay : BaseMonoSingleton<Canvas_Gameplay>
{
    public static event Action OnNextRequest;
    public static event Action OnRetryRequest;

    public HandContainerUI handContainer;

    public TextMeshProUGUI actionPointsText;
    public TextMeshProUGUI life;
    public TextMeshProUGUI block;
    public AnimatedContainer blockAnimatedContainer;
    public Slider slider;
    public Image healthFill;
    public Color healthColor;
    public Color armorColor;
    
    
    public GameObject winScreen;
    public GameObject loseScreen;

    public static void Win() => Instance.winScreen.SetActive(true);
    public static void Lose() => Instance.loseScreen.SetActive(true);

    public static void Refresh(BattleUnit player, DeckBattleData deckBattleData)
    {
        Instance.actionPointsText.text = $"{player.ActionPoints}/{player.MaxActionPoints}";
        Instance.life.text = $"{player.Health}/{player.MaxHealth}";

        var blockValue = player.Defense;

        if (player.Defense > 0)
        {
            Instance.block.text = $"+{blockValue}";
            Instance.blockAnimatedContainer.Open();
            Instance.healthFill.color = Instance.armorColor;
        }
        else
        {
            Instance.healthFill.color = Instance.healthColor;
            Instance.blockAnimatedContainer.Close();
        }
        
        Instance.slider.value = (float)player.Health / player.MaxHealth;
    }

    public void Next()
    {
        OnNextRequest?.Invoke();
    }

    public void Retry()
    {
        OnRetryRequest?.Invoke();
    }
}
