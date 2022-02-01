using UnityEngine;

public class CoinManager : RuntimeScriptableSingleton<CoinManager>
{
    public int[] tierValues = new int[] { 5, 10, 25, 50, 100 };
    public static int[] TierValues => Instance.tierValues;

    public static int GetValueForTier(int coinTier) => Instance.tierValues[coinTier];

    public Texture[] textures = new Texture[5];

    public static Texture GetCoinBaseTextureFromTier(int index) => Instance.textures[index];

}