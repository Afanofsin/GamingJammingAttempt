using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardDataSO : ScriptableObject
{
    [field: SerializeField]
    public int ManaCost { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
}
