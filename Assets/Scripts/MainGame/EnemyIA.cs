public class EnemyIA
{
    private readonly Enemy _enemy;

    public EnemyIA(Enemy enemy)
    {
        _enemy = enemy;
    }

    public BattleEffect NextAction() => _enemy.BattleEffects.GetRandom();
}