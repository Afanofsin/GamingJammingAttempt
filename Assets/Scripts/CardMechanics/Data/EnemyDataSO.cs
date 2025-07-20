using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public int Health { get; private set; }
    [field: SerializeField]
    public int Morale { get; private set; }
    [field: SerializeField]
    public int AttackPower { get; private set; }
    [field: SerializeField]
    public RuntimeAnimatorController Controller { get; private set; }
    [field: SerializeField]
    public EnemyPhase1SOBase Phase1Base;
    [field: SerializeField]
    public EnemyPhase2SOBase Phase2Base;
    [field: SerializeField]
    public EnemyPhase3SOBase Phase3Base;

}
