public class CoinManager : RuntimeScriptableSingleton<CoinManager>
{
    public int[] tierValues = new int[] { 5, 10, 25, 50, 100 };
    public static int[] TierValues => Instance.tierValues;
}