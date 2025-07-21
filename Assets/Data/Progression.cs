using UnityEngine;
[CreateAssetMenu(menuName = "Data/Progression")]
public class Progression : ScriptableObject
{
    [field: SerializeField]
    public bool isFirstBossKilled { get; set; } = false;
    [field: SerializeField]
    public bool isSecondBossKilled { get; set; } = false;
    [field: SerializeField]
    public bool isThirdBossKilled { get; set; } = false;
    [field: SerializeField]
    public bool isFourthBossKilled { get; set; } = false;
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

    public void ResetProgress()
    {
        isFirstBossKilled = false;
        isSecondBossKilled = false;
        isThirdBossKilled = false;
        isFourthBossKilled = false;
        isBedCardCollected = false;
        isPlantCardCollected = false;
        isToiletCardCollected = false;
        isWardrobeCardCollected = false;
        isShoeCardCollected = false;
        isCompletedCatQuest = false;
    }
}
