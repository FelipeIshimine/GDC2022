using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinInfoUI : MonoBehaviour
{
    public TextMeshProUGUI headDescription;
    public TextMeshProUGUI tailDescription;
    
    public TextMeshProUGUI headValue;
    public TextMeshProUGUI tailValue;

    public Image headIcon;
    public Image tailIcon;

    public void Initialize(Coin coin)
    {
        headDescription.text = coin.HeadDescription();
        tailDescription.text = coin.TailDescription();

        headValue.text = CoinManager.GetValueForTier(coin.tier).ToString();
        tailValue.text = CoinManager.GetValueForTier(coin.tier).ToString();

        headIcon.sprite = coin.HeadIcon;
        tailIcon.sprite = coin.TailIcon;
    }
}
