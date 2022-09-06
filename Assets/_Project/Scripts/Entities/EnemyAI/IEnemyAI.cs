public interface IEnemyAI
{
    public void Init(Enemy enemy);

    public void Tick(Enemy enemy);

    public void FixedTick(Enemy enemy);

    public void OnDeath(Enemy enemy);
}
