using UnityEngine;

public class KillEnemyGA : GameAction
{
    public EnemyView enemyView {  get; private set; }

    public KillEnemyGA(EnemyView target)
    {
        enemyView = target;
    }
}
