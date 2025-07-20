using Unity.VisualScripting;
using UnityEngine;

public class EnemyPhase1SOBase : ScriptableObject
{
    protected EnemyView Enemy;

    public virtual void Initialize(EnemyView enemyView)
    {
        Enemy = enemyView;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { }

    public virtual void DoReactionLogic(EnemyTurnGA enemyTurnGA) { }
    public virtual void ResetValues() { }
}
