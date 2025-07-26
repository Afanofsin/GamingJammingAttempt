using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Progression")]
public class Progression : ScriptableObject
{
    [field: SerializeField]
    public bool isBedCardCollected { get; set; } = false;
    [field: SerializeField]
    public bool isPlantCardCollected { get; set; } = false;
    [field: SerializeField]
    public bool isToiletCardCollected { get; set; } = false;
    [field: SerializeField]
    public bool isWardrobeCardCollected { get; set; } = false;
    [field: SerializeField]
    public bool isShoeCardCollected { get; set; } = false;
    [field: SerializeField]
    public bool isCompletedCatQuest { get; set; }   = false;
    [field: SerializeField]
    public bool isGuitarCardCollected { get; set; } = false;
    [field: SerializeField]
    public Dictionary<EnemyDataSO, bool> enemiesDefeated = new();

    public void ResetProgress()
    {
        enemiesDefeated = new();
        isBedCardCollected = false;
        isPlantCardCollected = false;
        isToiletCardCollected = false;
        isWardrobeCardCollected = false;
        isShoeCardCollected = false;
        isCompletedCatQuest = false;
        isGuitarCardCollected = false;
    }
}
