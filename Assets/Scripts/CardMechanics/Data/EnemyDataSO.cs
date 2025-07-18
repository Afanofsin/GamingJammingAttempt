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

}
