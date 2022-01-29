using Sirenix.OdinInspector;

public class LevelsManager : RuntimeScriptableSingleton<LevelsManager>
{
    [InlineEditor]public BattleLevel[] battleLevels;
    public static BattleLevel[] BattleLevels => Instance.battleLevels;
}