using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleApp : MonoBehaviour
{
    public EnemyDataSO slothDataSO;
    public EnemyDataSO proctrastinationDataSO;
    public EnemyDataSO imposterSyndromeDataSO;
    public EnemyDataSO firstGameDataSO;
    public Progression progression;
    public Button firstBossButton;
    public Button secondBossButton;
    public Button thirdBossButton;
    public Button fourthBossButton;
    public Button startBattleButton;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI bossDescription;
    public Sprite firstBossImage;
    public Sprite secondBossImage;
    public Sprite thirdBossImage;
    public Sprite fourthBossImage;
    public Sprite lockedSprite;
    public Sprite defeatedIcon;
    void OnEnable()
    {
        firstBossButton.enabled = true;
        EvaluateBosses();
    }

    public void EvaluateBosses()
    {
        EvaluateSloth();
        EvaluateProctastination();
        EvaluateImposter();
        EvaluateFirstGame();
    }
    public void ShowFirstBoss()
    {
        bossName.text = slothDataSO.name;

    }
    public void ShowSecondBoss()
    {
        bossName.text = proctrastinationDataSO.name;
    }
    public void ShowThirdBoss()
    {
        bossName.text = imposterSyndromeDataSO.name;
    }
    public void ShowFourthBoss()
    {
        bossName.text = firstGameDataSO.name;
    }
    public void EvaluateSloth()
    {
        if (!progression.isFirstBossKilled)
        {
            firstBossButton.image.sprite = firstBossImage;
        }
        else
        {
            firstBossButton.image.sprite = defeatedIcon;
            firstBossButton.enabled = false;
        }
    }
    public void EvaluateProctastination()
    {
        if (!progression.isFirstBossKilled)
        {
            secondBossButton.image.sprite = lockedSprite;
            secondBossButton.enabled = false;
        }
        else if (progression.isFirstBossKilled && !progression.isSecondBossKilled)
        {
            secondBossButton.image.sprite = secondBossImage;
            secondBossButton.enabled = true;
        }
        if (progression.isSecondBossKilled)
        {
            secondBossButton.image.sprite = defeatedIcon;
            secondBossButton.enabled = false;
        }
    }
    public void EvaluateImposter()
    {
        if (!progression.isSecondBossKilled)
        {
            thirdBossButton.image.sprite = lockedSprite;
            thirdBossButton.enabled = false;
        }
        else if (progression.isSecondBossKilled && !progression.isThirdBossKilled)
        {
            thirdBossButton.image.sprite = thirdBossImage;
            thirdBossButton.enabled = true;
        }
        if (progression.isThirdBossKilled)
        {
            thirdBossButton.image.sprite = defeatedIcon;
            thirdBossButton.enabled = false;
        }
    }
    public void EvaluateFirstGame()
    {
        if (!progression.isThirdBossKilled)
        {
            fourthBossButton.image.sprite = lockedSprite;
            fourthBossButton.enabled = false;
        }
        else if (progression.isThirdBossKilled && !progression.isFourthBossKilled)
        {
            fourthBossButton.image.sprite = fourthBossImage;
            fourthBossButton.enabled = true;
        }
        if (progression.isFourthBossKilled)
        {
            fourthBossButton.image.sprite = defeatedIcon;
            fourthBossButton.enabled = false;
        }
    }
    public void SwtichBattleApp()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
