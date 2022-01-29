using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DeckManager : RuntimeScriptableSingleton<DeckManager>
{
    public DeckPreset startingDeck;

    public static List<Coin> CreateDefaultDeck()
    {
        List<Coin> coins = new List<Coin>();
        foreach (CoinPresetPair coinPair in Instance.startingDeck.coinPairs)
            for (int i = 0; i < coinPair.amount; i++)
                coins.Add(coinPair.coinPreset.coin);
        return coins;
    }
}


