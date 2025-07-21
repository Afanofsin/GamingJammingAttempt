using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
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
    public Image bossImage;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI bossDescription;
    public Sprite firstBossImage;
    public Sprite secondBossImage;
    public Sprite thirdBossImage;
    public Sprite fourthBossImage;
    public Sprite lockedSprite;
    public Sprite defeatedIcon;
    public List <EnemyDataSO> bossData;
    void OnEnable()
    {
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Defeated");
        foreach (var defeated in toDestroy)
        {
            Destroy(defeated);
        }
        bossData.Clear();
        firstBossButton.enabled = true;
        bossName.text = null;
        bossDescription.text = null;
        bossImage.enabled = false;
        startBattleButton.gameObject.SetActive(false);
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
        bossData = new List<EnemyDataSO>(){slothDataSO};
        bossName.text = slothDataSO.name;
        bossImage.sprite = slothDataSO.Image;
        bossImage.enabled = true;
        startBattleButton.gameObject.SetActive(true);
        bossDescription.text = slothDataSO.Description;

    }
    public void StartBattle()
    {
        GameManagerSystem.Instance.StartBattle( bossData );
    }
    public void ShowSecondBoss()
    {
        bossData = new List<EnemyDataSO>(){proctrastinationDataSO};
        bossName.text = proctrastinationDataSO.name;
        bossImage.sprite = proctrastinationDataSO.Image;
        bossImage.enabled = true;
        startBattleButton.gameObject.SetActive(true);
        bossDescription.text = proctrastinationDataSO.Description;
    }
    public void ShowThirdBoss()
    {
        bossData = new List<EnemyDataSO>(){imposterSyndromeDataSO};
        bossName.text = imposterSyndromeDataSO.name;
        bossImage.sprite = imposterSyndromeDataSO.Image;
        bossImage.enabled = true;
        startBattleButton.gameObject.SetActive(true);
        bossDescription.text = imposterSyndromeDataSO.Description;
    }
    public void ShowFourthBoss()
    {
        bossData = new List<EnemyDataSO>(){firstGameDataSO};
        bossName.text = firstGameDataSO.name;
        bossImage.sprite = firstGameDataSO.Image;
        bossImage.enabled = true;
        startBattleButton.gameObject.SetActive(true);
        bossName.text = firstGameDataSO.Description;
    }
    public void EvaluateSloth()
    {
        if (!progression.isFirstBossKilled)
        {
            firstBossButton.image.sprite = firstBossImage;
        }
        else
        {
            firstBossButton.enabled = true;
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
            secondBossButton.enabled = true;
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
            thirdBossButton.enabled = true;
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
            fourthBossButton.enabled = true;
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
