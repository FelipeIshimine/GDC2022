using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DeckManager : RuntimeScriptableSingleton<DeckManager>
{
    public DeckPreset startingDeck;

    public DeckPreset defaultDeck;
    public static List<Coin> CreateDefaultDeck() => Instance.defaultDeck.coins.ConvertAll(x=>x.coin);
}


