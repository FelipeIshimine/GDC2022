using UnityEngine;

public class IconManager : RuntimeScriptableSingleton<IconManager>
{
    public Sprite attackIcon;
    public Sprite defenseIcon;
    public Sprite evadeIcon;

    public Texture attackCoinTexture;
    public Texture defenseCoinTexture;
    public Texture evadeCoinTexture;
}