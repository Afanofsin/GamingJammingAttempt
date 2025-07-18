using SerializeReferenceEditor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardDataSO : ScriptableObject
{
    [field: Header("Battle Data")]
    [field: SerializeField]
    public int ManaCost { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeReference, SR]
    public Effect ManualTargetEffect { get; private set; } = null;
    [field: SerializeField]
    public List<AutoTargetEffect> OtherEffects { get; private set; }

    [field: Space(10)]
    [field: Header("Game Progression Data")]
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public int DeckLimit { get; private set; }

    public void SetID(int id)
    {
        ID = id;
    }
}
