using UnityEngine;

public class KillEnemyGA : GameAction
{
    public EnemyView enemyView {  get; private set; }
    public int Reward {  get; private set; }
    public KillEnemyGA(EnemyView target, int reward)
    {
        enemyView = target;
        Reward = reward;
    }
}
