using UnityEngine;
[CreateAssetMenu(menuName = "Data/Progression")]
public class Progression : ScriptableObject
{
    public bool isFirstBossKilled = false;
    public bool isSecondBossKilled = false;
    public bool isThirdBossKilled = false;
    public bool isFourthBossKilled = false;
    public bool isBedCardCollected = false;
    public bool isPlantCardCollected = false;
    public bool isToiletCardCollected = false;
    public bool isWardrobeCardCollected = false;
    public bool isGuitarCardCollected = false;
    public bool isCompletedCatQuest = false;
}
